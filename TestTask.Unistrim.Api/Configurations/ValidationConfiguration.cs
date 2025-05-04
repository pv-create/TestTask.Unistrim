using FluentValidation;
using FluentValidation.AspNetCore;
using TestTask.Unistrim.Api.Validations;

namespace TestTask.Unistrim.Api.Configurations;

public static  class ValidationConfiguration
{
    public static IServiceCollection ConfigureValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<TransactionValidator>();
        services.AddFluentValidationAutoValidation();

        return services;
    }
}