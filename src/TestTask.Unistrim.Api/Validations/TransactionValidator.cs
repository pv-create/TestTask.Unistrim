using FluentValidation;
using TestTask.Unistrim.Api.Dto;

namespace TestTask.Unistrim.Api.Validations;

public class TransactionValidator: AbstractValidator<Transaction>
{
    public TransactionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id транзакции обязателен");

        RuleFor(x => x.TransactionDate)
            .NotEmpty()
            .WithMessage("Дата транзакции обязательна")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Транзакция не может быть создана в будущем");

        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage("Величина транзакции обязательна")
            .PrecisionScale(18, 2, false)
            .WithMessage("Неверная точность транзакции")
            .GreaterThan(0)
            .WithMessage("Сумма транзакции должна быть больше нуля");
    } 
}