using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : BaseController
{
    private readonly ITransactionService _transaction;

    public TransactionsController(ITransactionService transaction)
    {
        _transaction = transaction;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateTransferDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _transaction.CreateTransferAsync(dto);

        return result.IsSuccess
            ? Ok(result.Data)
            : HandleError(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _transaction.GetAllAsync();

        return result.IsSuccess
            ? Ok(result.Data)
            : HandleError(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _transaction.GetByIdAsync(id);

        return result.IsSuccess
            ? Ok(result.Data)
            : HandleError(result);
    }

    [HttpGet("account/{accountId}")]
    public async Task<IActionResult> GetByAccountIdAsync(Guid accountId)
    {
        var result = await _transaction.GetByAccountIdAsync(accountId);

        return result.IsSuccess
            ? Ok(result.Data)
            : HandleError(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateTransactionDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _transaction.UpdateAsync(id, dto);

        return result.IsSuccess
            ? Ok(result.Data)
            : HandleError(result);
    }
}