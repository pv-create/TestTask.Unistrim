namespace TestTask.Unistrim.Api.Models;

public sealed class Transaction
{
    public Guid Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
}