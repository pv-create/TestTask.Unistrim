using TestTask.Unistrim.Api.Dto;
using TestTask.Unistrim.Api.Models;

namespace TestTask.Unistrim.Api.Interfaces;

/// <summary>
/// Интерфейс для работы с базой данных
/// </summary>
public interface ITransactionRepository
{
    Task<Transaction> GetByIdAsync(Guid id);
    Task<TransactionModel> CreateAsync(Transaction transaction);

    Task<List<TransactionModel>> GetTransactions();
}