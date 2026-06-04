using Infrastructure.DTOs.Clients;
using Infrastructure.Result;
using Infrastructure.Results;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Infrastructure.Interfaces;

public interface IClientService
{
     Task<Result<CreateClientDto>> CreateAsync(CreateClientDto dto);
  Task<PagedResult<CreateClientDto>> GetAllAsync(GetClientFilterDto dto);
      Task<Result<CreateClientDto>> GetByIdAsync(Guid id);
    Task<Result<CreateClientDto>> UpdateAsync(Guid id, UpdateClientDto dto);
    Task<Result<bool>> DeleteAsync(Guid id);
}