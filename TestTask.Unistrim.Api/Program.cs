using Microsoft.EntityFrameworkCore;
using TestTask.Unistrim.Api.Configurations;
using TestTask.Unistrim.Api.Infrustructure;
using TestTask.Unistrim.Api.Interfaces;
using TestTask.Unistrim.Api.Repositories;
using TestTask.Unistrim.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbContext();
builder.Services.ConfigureValidation();
builder.Services.AddControllers();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();