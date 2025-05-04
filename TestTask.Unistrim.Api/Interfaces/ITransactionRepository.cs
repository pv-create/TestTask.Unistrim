using TestTask.Unistrim.Api.Dto;
using TestTask.Unistrim.Api.Models;

namespace TestTask.Unistrim.Api.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> GetByIdAsync(Guid id);
    Task<TransactionModel> CreateAsync(Transaction transaction);
}