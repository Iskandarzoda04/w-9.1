using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _service;

    public AnalyticsController(IAnalyticsService service)
    {
        _service = service;
    }


    [HttpGet("active-clients")]
    public async Task<IActionResult> GetActiveClients()
    {
        var result = await _service.GetActiveClientsAsync();
        return Ok(result);
    }

    // 2
    [HttpGet("client-accounts")]
    public async Task<IActionResult> GetClientAccounts([FromQuery] string email)
    {
        var result = await _service.GetClientAccountsByEmailAsync(email);
        return Ok(result);
    }

  
    [HttpGet("account-transfers/{accountId}")]
    public async Task<IActionResult> GetAccountTransfers(Guid accountId)
    {
        var result = await _service.GetAccountTransfersAsync(accountId);
        return Ok(result);
    }

  
    [HttpGet("client-balance")]
    public async Task<IActionResult> GetClientBalance([FromQuery] string email)
    {
        var result = await _service.GetClientBalanceAsync(email);
        return Ok(result);
    }

    
    [HttpGet("low-balance-accounts")]
    public async Task<IActionResult> GetLowBalanceAccounts([FromQuery] decimal threshold)
    {
        var result = await _service.GetDepositOnlyAccountsAsync(); // later replace if needed
        return Ok(result);
    }

    
    [HttpGet("accounts-with-owners")]
    public async Task<IActionResult> GetAccountsWithOwners()
    {
        var result = await _service.GetAccountsWithOwnersAsync();
        return Ok(result);
    }

    
    [HttpGet("richest-client")]
    public async Task<IActionResult> GetRichestClient()
    {
        var result = await _service.GetRichestClientAsync();
        return Ok(result);
    }

    
    [HttpGet("client-stats")]
    public async Task<IActionResult> GetClientStats()
    {
        var result = await _service.GetClientStatsAsync();
        return Ok(result);
    }

    
    [HttpGet("average-transfer")]
    public async Task<IActionResult> GetAverageTransfer()
    {
        var result = await _service.GetAverageTransferAsync();
        return Ok(result);
    }

    
    [HttpGet("busiest-day")]
    public async Task<IActionResult> GetBusiestDay()
    {
        var result = await _service.GetBusiestDayAsync();
        return Ok(result);
    }

 
    [HttpGet("total-fees")]
    public async Task<IActionResult> GetTotalFees()
    {
        var result = await _service.GetTotalFeesAsync();
        return Ok(result);
    }
}