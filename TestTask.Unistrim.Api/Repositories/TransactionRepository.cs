using Microsoft.EntityFrameworkCore;
using TestTask.Unistrim.Api.Dto;
using TestTask.Unistrim.Api.Infrustructure;
using TestTask.Unistrim.Api.Interfaces;
using TestTask.Unistrim.Api.Models;

namespace TestTask.Unistrim.Api.Repositories;

public class TransactionRepository: ITransactionRepository
{
    private readonly TransactionDbContext _context;
    private const int MaxTransactions = 100;

    public TransactionRepository(TransactionDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction> GetByIdAsync(Guid id)
    {
        TransactionModel? transactionModel = await _context.TransactionModels.FindAsync(id);

        if (transactionModel is null)
        {
            throw new Exception($"Не удалось найти транзакцию c id: {id}");
        }
        
        Transaction transaction = new()
        {
            Id = transactionModel.Id,
            TransactionDate = transactionModel.TransactionDate,
            Amount = transactionModel.Amount
        };
            
        return transaction;
    }

    public async Task<TransactionModel> CreateAsync(Transaction newTransaction)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingTransaction = await _context.TransactionModels.FindAsync(newTransaction.Id);

            if (existingTransaction is not null)
            {
                return existingTransaction;
            }
            // Получаем текущее количество транзакций
            var currentCount = await _context.TransactionModels.CountAsync();

            if (currentCount >= MaxTransactions)
            {
                // Удаляем самую старую транзакцию
                TransactionModel oldestTransaction = await _context.TransactionModels
                    .OrderBy(t => t.TransactionDate)
                    .FirstAsync();

                _context.TransactionModels.Remove(oldestTransaction);
            }

            // Добавляем новую транзакцию
            TransactionModel newTransactionModel = new()
            {
                TransactionDate = newTransaction.TransactionDate,
                Amount = newTransaction.Amount
            };
                
            await _context.TransactionModels.AddAsync(newTransactionModel);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return newTransactionModel;
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Произошла ошибка при добавлении новой транзакции {ex.Message}");
        }
    }

    public async Task<IReadOnlyCollection<Transaction>> GetTransactions()
    {
        return await  _context.TransactionModels
            .Select(x =>
            new Transaction()
            {
                TransactionDate = x.TransactionDate,
                Amount = x.Amount,
                Id = x.Id
                    
            })
            .ToListAsync();
    }
}