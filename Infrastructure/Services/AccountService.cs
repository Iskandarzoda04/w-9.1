using Domain.Entities;
using Infrastructure.DTOs.Accounts;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Infrastructure.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;
    private readonly ILogger<AccountService> _logger;

    public AccountService(AppDbContext context, ILogger<AccountService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<AccountDto>> CreateAsync(CreateAccountDto dto)
    {
        try
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(x => x.Id == dto.ClientId);

            if (client == null)
            {
                return Result<AccountDto>.Fail("Client not found", ErrorType.NotFound);
            }

            var account = new Account
            {
                ClientId = dto.ClientId,
                AccountNumber = Guid.NewGuid().ToString()[..12],
                Balance = 0,
                Currency = dto.Currency,
                Type = dto.Type,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            var result = new AccountDto
            {
                Id = account.Id,
                ClientId = account.ClientId,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                Currency = account.Currency,
                Type = account.Type,
                IsActive = account.IsActive,
                CreatedAt = account.CreatedAt
            };

            return Result<AccountDto>.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating account");
            return Result<AccountDto>.Fail("Error while creating account", ErrorType.Unknown);
        }
    }

    public async Task<Result<List<AccountDto>>> GetAllAsync()
    {
        try
        {
            var accounts = await _context.Accounts
                .Select(a => new AccountDto
                {
                    Id = a.Id,
                    ClientId = a.ClientId,
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance,
                    Currency = a.Currency,
                    Type = a.Type,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            return Result<List<AccountDto>>.Ok(accounts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting accounts");
            return Result<List<AccountDto>>.Fail("Error while getting accounts");
        }
    }

    public async Task<Result<AccountDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var account = await _context.Accounts
                .Where(a => a.Id == id)
                .Select(a => new AccountDto
                {
                    Id = a.Id,
                    ClientId = a.ClientId,
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance,
                    Currency = a.Currency,
                    Type = a.Type,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return Result<AccountDto>.Fail("Account not found", ErrorType.NotFound);
            }

            return Result<AccountDto>.Ok(account);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting account");
            return Result<AccountDto>.Fail("Error while getting account");
        }
    }

    public async Task<Result<List<AccountDto>>> GetByClientIdAsync(Guid clientId)
    {
        try
        {
            var accounts = await _context.Accounts
                .Where(x => x.ClientId == clientId)
                .Select(a => new AccountDto
                {
                    Id = a.Id,
                    ClientId = a.ClientId,
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance,
                    Currency = a.Currency,
                    Type = a.Type,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            return Result<List<AccountDto>>.Ok(accounts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting client accounts");
            return Result<List<AccountDto>>.Fail("Error while getting client accounts");
        }
    }

    public async Task<Result<AccountDto>> UpdateAsync(Guid id, UpdateAccountDto dto)
    {
        try
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(x => x.Id == id);

            if (account == null)
            {
                return Result<AccountDto>.Fail("Account not found", ErrorType.NotFound);
            }

            account.Currency = dto.Currency;
            account.Type = dto.Type; 
            account.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            var result = new AccountDto
            {
                Id = account.Id,
                ClientId = account.ClientId,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                Currency = account.Currency,
                Type = account.Type,
                IsActive = account.IsActive,
                CreatedAt = account.CreatedAt
            };

            return Result<AccountDto>.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating account");
            return Result<AccountDto>.Fail("Error while updating account", ErrorType.Unknown);
        }
    }
}