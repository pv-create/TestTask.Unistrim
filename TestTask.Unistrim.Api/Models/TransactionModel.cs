namespace TestTask.Unistrim.Api.Models;

/// <summary>
/// Модель банковской транзакции
/// </summary>
public sealed class TransactionModel
{
    /// <summary>
    /// Id транзакции
    /// </summary>
    public Guid Id { get; init; }
    /// <summary>
    /// Дата  транзакции
    /// </summary>
    public DateTime TransactionDate { get; set; }
    /// <summary>
    /// Размер транзакции
    /// </summary>
    public decimal Amount { get; set; }
}