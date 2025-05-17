using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestTask.Unistrim.Api.Dto;
using TestTask.Unistrim.Api.Interfaces;
using TestTask.Unistrim.Api.Options;

namespace TestTask.Unistrim.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ILogger<TransactionController> _logger;
    private readonly ITransactionService _transactionService;
    private readonly TransactionSettings _transactionSettings;

    public TransactionController(
        ITransactionService transactionService,
        ILogger<TransactionController> logger,
        IOptions<TransactionSettings> options)
    {
        _transactionService = transactionService;
        _logger = logger;
        _transactionSettings = options.Value;
    }

    [HttpGet]
    [Route("GetTransactionOptions")]
    public async Task<ActionResult> GetTransactionOptions()
    {
        return Ok("test");
    }

    [HttpGet]
    [Route("GetTransactions")]
    public async Task<ActionResult<IReadOnlyCollection<Transaction>>> GetTransactions()
    {
        var result = await _transactionService.GetTransactions();

        return Ok(result);
    }
    /// <summary>
    /// Метод создания новой транзакции
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Метод получения транзакции по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

