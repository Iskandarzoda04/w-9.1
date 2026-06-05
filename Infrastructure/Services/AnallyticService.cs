using Domain.Entities;
using Infrastructure.DTOs.Accounts;
using Infrastructure.DTOs.Analytics;
using Infrastructure.Persistence.DataContext;
using Infrastructure.Results;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
namespace Infrastructure.Services;

public class AnallyticService : IAnalyticsService
{

     private readonly AppDbContext _context;
    private readonly ILogger<AnallyticService> _logger;

    public AnallyticService(AppDbContext context, ILogger<AnallyticService> logger)
    {
        _context = context;
        _logger = logger;
    }

  public async Task<Result<List<AccountOwnerDto>>> GetAccountsWithOwnersAsync()
{
    try
    {
        var accounts = await _context.Accounts
            .Select(a => new AccountOwnerDto
            {
                AccountNumber = a.AccountNumber,
                FirstName = a.Client.FirstName,
                LastName = a.Client.LastName,
                Balance = a.Balance
            })
            .ToListAsync();

        if (!accounts.Any())
        {
            return Result<List<AccountOwnerDto>>.Fail("Accounts not found", ErrorType.NotFound);
        }

        return Result<List<AccountOwnerDto>>.Ok(accounts);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting accounts with owners");

        return Result<List<AccountOwnerDto>>.Fail("Error while getting accounts with owners", ErrorType.Unknown);
    }
}

    public Task<Result<List<TransactionDto>>> GetAccountTransfersAsync(Guid accountId)
    {
        throw new NotImplementedException();
    }

public async Task<Result<List<ActiveClientDto>>> GetActiveClientsAsync()
{
    try
    {
        var clients = await _context.Clients
            .Where(c => c.IsActive)
            .Select(c => new ActiveClientDto
            {
                Id = c.Id,
                FullName = $"{c.FirstName} {c.LastName}",
                Email = c.Email
            }).ToListAsync();

        if (!clients.Any())
        {
            return Result<List<ActiveClientDto>>.Fail("Active clients not found", ErrorType.NotFound);
        }

        return Result<List<ActiveClientDto>>.Ok(clients);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting active clients");

        return Result<List<ActiveClientDto>>.Fail("Error while getting active clients", ErrorType.Unknown);
    }
}

 public async Task<Result<decimal>> GetAverageTransferAsync()
{
    try
    {
        var average = await _context.Transactions
            .Where(x => x.Status == 1)
            .AverageAsync(x => x.Amount);

        return Result<decimal>.Ok(average);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting average transfer");

        return Result<decimal>.Fail("Error while getting average transfer", ErrorType.Unknown);
    }
}

public async Task<Result<BusiestDayDto>> GetBusiestDayAsync()
{
    try
    {
        var busiestDay = await _context.Transactions
            .Where(x => x.Status == 1)
            .GroupBy(x => x.Timestamp.Date)
            .Select(g => new BusiestDayDto
            {
                
                TotalAmount = g.Sum(x => x.Amount)
            })
            .OrderByDescending(x => x.TotalAmount)
            .FirstOrDefaultAsync();

        if (busiestDay == null)
        {
            return Result<BusiestDayDto>.Fail("No transactions found", ErrorType.NotFound);
        }

        return Result<BusiestDayDto>
            .Ok(busiestDay);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting busiest day");

        return Result<BusiestDayDto>.Fail("Error while getting busiest day", ErrorType.Unknown);
    }
}

 public async Task<Result<List<ClientAccountDto>>> GetClientAccountsByEmailAsync(string email)
{
    try
    {
        var client = await _context.Clients
        .FirstOrDefaultAsync(x => x.Email == email);

        if (client == null)
        {
            return Result<List<ClientAccountDto>>.Fail("Client not found", ErrorType.NotFound);
        }

        var accounts = await _context.Accounts
            .Where(x => x.ClientId == client.Id)
            .Select(x => new ClientAccountDto
            {
                AccountNumber = x.AccountNumber,
                Balance = x.Balance,
                Currency = x.Currency
            })
            .ToListAsync();

        if (!accounts.Any())
        {
            return Result<List<ClientAccountDto>>.Fail("No accounts found", ErrorType.NotFound);
        }

        return Result<List<ClientAccountDto>>.Ok(accounts);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting client accounts");

        return Result<List<ClientAccountDto>>
            .Fail("Error while getting client accounts", ErrorType.Unknown);
    }
}

public async Task<Result<ClientBalanceDto>> GetClientBalanceAsync(string email)
{
    try
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(x => x.Email == email);

        if (client == null)
        {
            return Result<ClientBalanceDto>
                .Fail("Client not found", ErrorType.NotFound);
        }

        var totalBalance = await _context.Accounts
            .Where(x => x.ClientId == client.Id)
            .SumAsync(x => x.Balance);

        var result = new ClientBalanceDto
        {
            FullName = $"{client.FirstName} {client.LastName}",
            TotalBalance = totalBalance
        };

        return Result<ClientBalanceDto>.Ok(result);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting client balance");

        return Result<ClientBalanceDto>
            .Fail("Error while getting client balance", ErrorType.Unknown);
    }
}
public async Task<Result<List<ClientStatsDto>>> GetClientStatsAsync()
{
    try
    {
        var stats = await _context.Clients
            .Select(c => new ClientStatsDto
            {
                FullName = $"{c.FirstName} {c.LastName}",
                AccountsCount = c.Accounts.Count,
                TotalBalance = c.Accounts.Sum(a => a.Balance)
            })
            .ToListAsync();

        if (!stats.Any())
        {
            return Result<List<ClientStatsDto>>
                .Fail("No clients found", ErrorType.NotFound);
        }

        return Result<List<ClientStatsDto>>
            .Ok(stats);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting client stats");

        return Result<List<ClientStatsDto>>
            .Fail("Error while getting client stats", ErrorType.Unknown);
    }
}
    public Task<Result<List<DepositOnlyAccountDto>>> GetDepositOnlyAccountsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<HighOutgoingAccountDto>>> GetHighOutgoingAccountsAsync(decimal amount)
    {
        throw new NotImplementedException();
    }


 public async Task<Result<RichestClientDto>> GetRichestClientAsync()
{
    try
    {
        var client = await _context.Clients
            .Select(c => new
            {
                Client = c,
                TotalBalance = c.Accounts.Sum(a => a.Balance)
            })
            .OrderByDescending(x => x.TotalBalance)
            .FirstOrDefaultAsync();

        if (client == null)
        {
            return Result<RichestClientDto>
                .Fail("No clients found", ErrorType.NotFound);
        }

        var result = new RichestClientDto
        {
            FullName = $"{client.Client.FirstName} {client.Client.LastName}",
            TotalBalance = client.TotalBalance
        };

        return Result<RichestClientDto>.Ok(result);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting richest client");

        return Result<RichestClientDto>
            .Fail("Error while getting richest client", ErrorType.Unknown);
    }
}

    public Task<Result<List<SelfTransferClientDto>>> GetSelfTransferClientsAsync()
    {
        throw new NotImplementedException();
    }

 

  public async Task<Result<decimal>> GetTotalFeesAsync()
{
    try
    {
        var totalFees = await _context.Transactions
            .Where(x => x.Status == 1)
            .SumAsync(x => x.Fee);

        return Result<decimal>.Ok(totalFees);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting total fees");

        return Result<decimal>
            .Fail("Error while getting total fees", ErrorType.Unknown);
    }
}

}
