using Microsoft.EntityFrameworkCore;
using Npgsql;
using Polly;
using Serilog;
using TestTask.Unistrim.Api.Infrustructure;

namespace TestTask.Unistrim.Api.Configurations;

public static class DbContextConfiguration
{
    public static void ConfigureDbContext(this WebApplicationBuilder builder)
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Строка подключения 'DefaultConnection' не найдена.");
    }

    // Определяем политики повторных попыток
    var retryPolicy = Policy
        .Handle<NpgsqlException>()
        .Or<TimeoutException>()
        .WaitAndRetry(
            retryCount: 3,
            sleepDurationProvider: retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            onRetry: (exception, timeSpan, retryCount, context) =>
            {
                Log.Warning(
                    exception,
                    "Ошибка подключения к базе данных. Повторная попытка {RetryCount} через {TimeSpan} секунд",
                    retryCount,
                    timeSpan.TotalSeconds);
            });

    // Политика Circuit Breaker
    var circuitBreakerPolicy = Policy
        .Handle<NpgsqlException>()
        .Or<TimeoutException>()
        .CircuitBreaker(
            exceptionsAllowedBeforeBreaking: 5, // количество ошибок до размыкания цепи
            durationOfBreak: TimeSpan.FromSeconds(30), // время, на которое размыкается цепь
            onBreak: (exception, duration) =>
            {
                Log.Error(
                    exception,
                    "Circuit Breaker размкнут на {DurationSeconds} секунд",
                    duration.TotalSeconds);
            },
            onReset: () =>
            {
                Log.Information("Circuit Breaker сброшен");
            });

    // Комбинируем политики
    var resilientPolicy = Policy.Wrap(retryPolicy, circuitBreakerPolicy);

    builder.Services.AddDbContext<TransactionDbContext>((serviceProvider, options) =>
    {
        options.UseNpgsql(connectionString, npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorCodesToAdd: null);

            npgsqlOptions.CommandTimeout(20);
            npgsqlOptions.EnableRetryOnFailure(0);
        });

        if (builder.Environment.IsDevelopment())
        {
            options.EnableSensitiveDataLogging()
                  .EnableDetailedErrors()
                  .LogTo(Console.WriteLine);
        }
    });
}
}