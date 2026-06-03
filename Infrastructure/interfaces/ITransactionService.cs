using Infrastructure.Results;

namespace Infrastructure.Interfaces;

public interface ITransactionService
{
   Task<Result<TransactionDto>> CreateTransferAsync(CreateTransferDto dto);
   Task<Result<List<TransactionDto>>> GetAllAsync();
  Task<Result<TransactionDto>> GetByIdAsync(Guid id);
  Task<Result<List<TransactionDto>>> GetByAccountIdAsync(Guid accountId);
    Task<Result<TransactionDto>> UpdateAsync( Guid id, UpdateTransactionDto dto);
}