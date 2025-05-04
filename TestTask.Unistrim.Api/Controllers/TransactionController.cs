using Microsoft.AspNetCore.Mvc;
using TestTask.Unistrim.Api.Dto;
using TestTask.Unistrim.Api.Interfaces;

namespace TestTask.Unistrim.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ILogger<TransactionController> _logger;
    private readonly ITransactionService _transactionService;

    public TransactionController(
        ITransactionService transactionService,
        ILogger<TransactionController> logger)
    {
        _transactionService = transactionService;
        _logger = logger;
    }
    
    [HttpPost]
    [Route("Transaction")]
    public async Task<ActionResult<Transaction>> CreateTransaction(Transaction transaction)
    {
        try
        {
            _logger.LogInformation("Создание транзакции с id: {id}", transaction.Id);
            var result = await _transactionService.CreateTransaction(transaction);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Произошла ошибка при добавлении новой транзакции: {ex}", ex.Message);

            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("Transaction")]
    public async Task<ActionResult<Transaction>> GetTransaction([FromQuery] Guid id)
    {
        try
        {
            var result = await _transactionService.GetTransactionById(id);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Произошла ошибка при получении с {id} транзакции: {ex}", id, ex.Message);
            
            return BadRequest(ex.Message);
        }
    }
}

