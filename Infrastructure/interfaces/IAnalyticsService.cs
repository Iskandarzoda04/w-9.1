using Domain.Entities;
using Infrastructure.DTOs.Accounts;
using Infrastructure.DTOs.Analytics;
using Infrastructure.Results;

public interface IAnalyticsService
{
    Task<Result<List<ActiveClientDto>>> GetActiveClientsAsync();
    Task<Result<List<ClientAccountDto>>> GetClientAccountsByEmailAsync(string email);
    Task<Result<List<TransactionDto>>> GetAccountTransfersAsync(Guid accountId);
    Task<Result<ClientBalanceDto>> GetClientBalanceAsync(string email);
    Task<Result<List<AccountOwnerDto>>> GetAccountsWithOwnersAsync();
    Task<Result<RichestClientDto>> GetRichestClientAsync();
  
    Task<Result<List<ClientStatsDto>>> GetClientStatsAsync();
    Task<Result<List<HighOutgoingAccountDto>>> GetHighOutgoingAccountsAsync(decimal amount);
    Task<Result<decimal>> GetTotalFeesAsync();
    Task<Result<decimal>> GetAverageTransferAsync();
    Task<Result<List<SelfTransferClientDto>>> GetSelfTransferClientsAsync();
    Task<Result<BusiestDayDto>> GetBusiestDayAsync();
    Task<Result<List<DepositOnlyAccountDto>>> GetDepositOnlyAccountsAsync();
}