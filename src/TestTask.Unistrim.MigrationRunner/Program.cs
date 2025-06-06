using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestTask.Unistrim.Api.Infrustructure;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        connectionString = "Host=84.252.143.70;Port=5432;Database=MyDatabase;Username=postgres;Password=postgres";

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Строка подключения не найдена в переменных окружения.");
        }

        Console.WriteLine($"Using connection string: {connectionString}");

        services.AddDbContext<TransactionDbContext>(options =>
            options.UseNpgsql(connectionString));
    })
    .Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    Console.WriteLine("Applying migrations...");
    var dbContext = services.GetRequiredService<TransactionDbContext>();
    dbContext.Database.Migrate();
    Console.WriteLine("Migrations applied successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
    throw;
}