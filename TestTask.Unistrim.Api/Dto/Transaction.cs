namespace TestTask.Unistrim.Api.Dto;

public record Transaction
{
    public required Guid Id { get; init; }
    public required DateTime TransactionDate { get; init; }
    public required decimal Amount { get; init; }
}