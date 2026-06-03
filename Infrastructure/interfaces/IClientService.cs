using Infrastructure.DTOs.Clients;
using Infrastructure.Results;

namespace Infrastructure.Interfaces;

public interface IClientService
{
     Task<Result<CreateClientDto>> CreateAsync(CreateClientDto dto);
  Task<Result<List<CreateClientDto>>> GetAllAsync();
    Task<Result<CreateClientDto>> GetByIdAsync(Guid id);
    Task<Result<CreateClientDto>> UpdateAsync(Guid id, UpdateClientDto dto);
    Task<Result<bool>> DeleteAsync(Guid id);
}