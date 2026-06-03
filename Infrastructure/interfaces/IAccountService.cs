using Infrastructure.DTOs.Accounts;
using Infrastructure.Results;

namespace Infrastructure.Interfaces;

public interface IAccountService
{
  Task<Result<AccountDto>> CreateAsync(CreateAccountDto dto);
    Task<Result<List<AccountDto>>> GetAllAsync();
    Task<Result<AccountDto>> GetByIdAsync(Guid id);
    Task<Result<List<AccountDto>>> GetByClientIdAsync(Guid clientId);
   Task<Result<AccountDto>> UpdateAsync(Guid id, UpdateAccountDto dto);
}