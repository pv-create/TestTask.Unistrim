using TestTask.Unistrim.Api.Dto;
using TestTask.Unistrim.Api.Interfaces;
using TestTask.Unistrim.Api.Models;

namespace TestTask.Unistrim.Api.Services;

public class TransactionService: ITransactionService
{
    private readonly ILogger<TransactionService> _logger;
    private readonly ITransactionRepository _repository;

    public TransactionService(
        ILogger<TransactionService> logger,
        ITransactionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    
    public async Task<Transaction> CreateTransaction(Transaction transaction)
    {
        try
        {
            TransactionModel newTransactionModel = await _repository.CreateAsync(transaction);

            return new()
            {
                Id = newTransactionModel.Id,
                TransactionDate = newTransactionModel.TransactionDate,
                Amount = newTransactionModel.Amount
            };

        }
        catch (Exception ex)
        {
            throw new Exception($"Произошла ошибка при добавлении новой транзакции {ex.Message}");
        }
    }

    public async Task<Transaction> GetTransactionById(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
}