namespace TestTask.Unistrim.Api.Models;

public sealed class TransactionModel
{
    public Guid Id { get; init; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
}