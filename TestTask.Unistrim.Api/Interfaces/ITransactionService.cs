using TestTask.Unistrim.Api.Dto;

namespace TestTask.Unistrim.Api.Interfaces;

public interface ITransactionService
{
    Task<Transaction> CreateTransaction(Transaction transaction);

    Task<Transaction> GetTransactionById(Guid id);
}