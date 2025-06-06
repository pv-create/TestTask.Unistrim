using Npgsql;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using Serilog;
using TestTask.Unistrim.Api.Configurations;
using TestTask.Unistrim.Api.Interfaces;
using TestTask.Unistrim.Api.Repositories;
using TestTask.Unistrim.Api.Services;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Read from appsettings.json
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddMetrics();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(cfg => cfg
        .SetResourceBuilder(ResourceBuilder.CreateDefault()
                .AddService("TransactionService"))
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddNpgsql()
        .AddSource("TransactionService")
        .AddOtlpExporter(opts => opts.Endpoint = new Uri("http://84.252.143.70:4317"))
        .AddConsoleExporter()
        );

builder.ConfigureDbContext();
builder.Services.AddSwaggerConfiguration(builder.Configuration);
builder.Services.ConfigureValidation();
builder.Services.AddControllers();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.MapMetrics();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSwaggerConfiguration();
app.Run();

public partial class Program { }