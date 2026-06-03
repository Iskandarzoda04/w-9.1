using Infrastructure.DTOs.Accounts;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController : BaseController
{
    private readonly IAccountService _account;

    public AccountsController(IAccountService account)
    {
        _account = account;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateAccountDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _account.CreateAsync(dto);

        return result.IsSuccess? Ok(result.Data): HandleError(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _account.GetAllAsync();

        return result.IsSuccess ? Ok(result.Data) : HandleError(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _account.GetByIdAsync(id);

        return result.IsSuccess ? Ok(result.Data): HandleError(result);
    }

    [HttpGet("client/{clientId}")]
    public async Task<IActionResult> GetByClientIdAsync(Guid clientId)
    {
        var result = await _account.GetByClientIdAsync(clientId);

        return result.IsSuccess? Ok(result.Data): HandleError(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateAccountDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _account.UpdateAsync(id, dto);

        return result.IsSuccess? Ok(result.Data): HandleError(result);
    }
}