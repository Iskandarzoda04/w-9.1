using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Infrastructure.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly AppDbContext _context;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(
        AppDbContext context,
        ILogger<TransactionService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<TransactionDto>> CreateTransferAsync(CreateTransferDto dto)
    {
        try
        {
            if (dto.FromAccountId == Guid.Empty)
            {
                _logger.LogWarning("FromAccountId is required");
                return Result<TransactionDto>.Fail("FromAccountId is required", ErrorType.Validation);
            }

            if (dto.ToAccountId == Guid.Empty)
            {
                _logger.LogWarning("ToAccountId is required");
                return Result<TransactionDto>.Fail("ToAccountId is required",ErrorType.Validation);
            }

            if (dto.Amount <= 0)
            {
                _logger.LogWarning("Amount must be greater than zero");
                return Result<TransactionDto>.Fail( "Amount must be greater than zero",ErrorType.Validation);
            }


            var transaction = new Transaction
            {
                FromAccountId = dto.FromAccountId,
                ToAccountId = dto.ToAccountId,
                Amount = dto.Amount,
                Fee = 0,
                Status = dto.Status,
                Description = dto.Description,
                Timestamp = DateTime.UtcNow
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return Result<TransactionDto>.Ok(
                new TransactionDto
                {
                    Id = transaction.Id,
                    FromAccountId = transaction.FromAccountId,
                    ToAccountId = transaction.ToAccountId,
                    Amount = transaction.Amount,
                    Fee = transaction.Fee,
                    Status = transaction.Status,
                    Description = transaction.Description,
                    Timestamp = transaction.Timestamp
                });
        }
        catch (Exception)
        {
            _logger.LogError("Error while creating transaction");

            return Result<TransactionDto>.Fail(
                "Error while creating transaction",
                ErrorType.Unknown);
        }
    }

    public async Task<Result<List<TransactionDto>>> GetAllAsync()
    {
        try
        {
            var transactions = await _context.Transactions
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    FromAccountId = t.FromAccountId,
                    ToAccountId = t.ToAccountId,
                    Amount = t.Amount,
                    Fee = t.Fee,
                    Status = t.Status,
                    Description = t.Description,
                    Timestamp = t.Timestamp
                })
                .ToListAsync();

            return Result<List<TransactionDto>>.Ok(transactions);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting transactions");

            return Result<List<TransactionDto>>.Fail(
                "Error while getting transactions");
        }
    }

    public async Task<Result<TransactionDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var transaction = await _context.Transactions
                .Where(t => t.Id == id)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    FromAccountId = t.FromAccountId,
                    ToAccountId = t.ToAccountId,
                    Amount = t.Amount,
                    Fee = t.Fee,
                    Status = t.Status,
                    Description = t.Description,
                    Timestamp = t.Timestamp
                })
                .FirstOrDefaultAsync();

            if (transaction == null)
            {
                _logger.LogWarning("Transaction not found");

                return Result<TransactionDto>.Fail(
                    "Transaction not found",
                    ErrorType.NotFound);
            }

            return Result<TransactionDto>.Ok(transaction);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting transaction");

            return Result<TransactionDto>.Fail(
                "Error while getting transaction");
        }
    }

    public async Task<Result<List<TransactionDto>>> GetByAccountIdAsync(Guid accountId)
    {
        try
        {
            var transactions = await _context.Transactions
                .Where(tr =>
                    tr.FromAccountId == accountId ||
                    tr.ToAccountId == accountId)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    FromAccountId = t.FromAccountId,
                    ToAccountId = t.ToAccountId,
                    Amount = t.Amount,
                    Fee = t.Fee,
                    Status = t.Status,
                    Description = t.Description,
                    Timestamp = t.Timestamp
                })
                .ToListAsync();

            return Result<List<TransactionDto>>.Ok(transactions);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting account transactions");

            return Result<List<TransactionDto>>.Fail(
                "Error while getting account transactions");
        }
    }

    public async Task<Result<TransactionDto>> UpdateAsync( Guid id, UpdateTransactionDto dto)
    {
        try
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (transaction == null)
            {
                return Result<TransactionDto>.Fail(
                    "Transaction not found",
                    ErrorType.NotFound);
            }

            transaction.Status = dto.Status;
            transaction.Description = dto.Description;

            await _context.SaveChangesAsync();

            return Result<TransactionDto>.Ok(
                new TransactionDto
                {
                    Id = transaction.Id,
                    FromAccountId = transaction.FromAccountId,
                    ToAccountId = transaction.ToAccountId,
                    Amount = transaction.Amount,
                    Fee = transaction.Fee,
                    Description = transaction.Description,
                    Timestamp = transaction.Timestamp
                });
        }
        catch (Exception)
        {
            _logger.LogError("Error while updating transaction");

            return Result<TransactionDto>.Fail(
                "Error while updating transaction",
                ErrorType.Unknown);
        }
    }

  

}