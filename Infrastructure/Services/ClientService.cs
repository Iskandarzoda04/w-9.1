using Infrastructure.Results;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Infrastructure.Persistence.DataContext;
using Infrastructure.DTOs.Clients;
using Infrastructure.Interfaces;
using Infrastructure.Result;


namespace Infrastructure.Services;

public class ClientService : IClientService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ClientService> _logger;
    public ClientService(AppDbContext context, ILogger<ClientService> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Result<CreateClientDto>> CreateAsync(CreateClientDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName))
            {
                _logger.LogWarning("First name is required.");
                return Result<CreateClientDto>.Fail("First name is required", ErrorType.Validation);
            }
            if (string.IsNullOrWhiteSpace(dto.LastName))
            {
                _logger.LogWarning("Last name is required.");
                return Result<CreateClientDto>.Fail("Last name is required", ErrorType.Validation);
            }
            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                _logger.LogWarning("Email is required.");
                return Result<CreateClientDto>.Fail("Email is required", ErrorType.Validation);
            }
            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                _logger.LogWarning("Phone number is required.");
                return Result<CreateClientDto>.Fail("Phone number is required", ErrorType.Validation);
            }

            var emailCheck = await _context.Clients
                .FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (emailCheck != null)
            {
                _logger.LogWarning("Client with this Email already Exists");
                return Result<CreateClientDto>.Fail("Client with this Email already Exists", ErrorType.Conflict);
            }
            var phoneCheck = await _context.Clients
                .FirstOrDefaultAsync(x => x.PhoneNumber == dto.PhoneNumber);
            if (phoneCheck != null)
            {
                _logger.LogWarning("Client with this phonenumber already Exists");
                return Result<CreateClientDto>.Fail("Client with this phonenumber already Exists", ErrorType.Conflict);
            }

            var client = new Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                BirthDate = dto.BirthDate
            };

            var result = _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return Result<CreateClientDto>.Ok(new CreateClientDto
            {
                id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                BirthDate = client.BirthDate,
                IsActive = client.IsActive,
                CreatedAt = client.CreatedAt
            });

        }
        catch (System.Exception)
        {
            _logger.LogError("An error while creating Client");
            return Result<CreateClientDto>.Fail("An error while creating Client", ErrorType.Unknown);
        }
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        try
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                _logger.LogWarning("Client with this id doesn't found");
                return Result<bool>.Fail("Client with this id doesn't found");
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }
        catch (System.Exception)
        {
            _logger.LogError("An error while");
            return Result<bool>.Fail("An error while");
        }
    }

public async Task<PagedResult<CreateClientDto>> GetAllAsync(GetClientFilterDto dto)
{
    var query = _context.Clients.AsQueryable();

  
    if (!string.IsNullOrWhiteSpace(dto.FirstName))
        query = query.Where(c => c.FirstName.Contains(dto.FirstName));

    if (!string.IsNullOrWhiteSpace(dto.LastName))
        query = query.Where(c => c.LastName.Contains(dto.LastName));

    if (!string.IsNullOrWhiteSpace(dto.Email))
        query = query.Where(c => c.Email.Contains(dto.Email));


    var totalCount = await query.CountAsync();

    
    var clients = await query
        .Skip((dto.Page - 1) * dto.PageSize)
        .Take(dto.PageSize)
        .Select(c => new CreateClientDto
        {
            id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            BirthDate = c.BirthDate,
            CreatedAt = c.CreatedAt,
            IsActive = c.IsActive
        })
        .ToListAsync();

    return PagedResult<CreateClientDto>.Ok(
        clients,
        totalCount,
        dto.Page,
        dto.PageSize
    );
}

 public async Task<Result<CreateClientDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                _logger.LogWarning("Client with this id doesn't found");
                return Result<CreateClientDto>.Fail("Client with this id doesn't found");
            }
            var client2 = await _context.Clients
                .Where(c => c.Id == id)
                .Select(c => new CreateClientDto
                {
                    id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    IsActive = c.IsActive,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    CreatedAt = c.CreatedAt,
                    BirthDate = c.BirthDate
                }).FirstOrDefaultAsync();

            return Result<CreateClientDto>.Ok(client2);
        }
        catch (System.Exception)
        {
            _logger.LogError("An error while getting by id");
            return Result<CreateClientDto>.Fail("An error while getting by id");
        }
    }


    public async Task<Result<CreateClientDto>> UpdateAsync(Guid id, UpdateClientDto dto)
    {
        try
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
            {
                _logger.LogWarning("Client not found.");
                return Result<CreateClientDto>.Fail("Client not found", ErrorType.NotFound);
            }

            if (string.IsNullOrWhiteSpace(dto.FirstName))
            {
                _logger.LogWarning("First name is required.");
                return Result<CreateClientDto>.Fail("First name is required", ErrorType.Validation);
            }
            if (string.IsNullOrWhiteSpace(dto.LastName))
            {
                _logger.LogWarning("Last name is required.");
                return Result<CreateClientDto>.Fail("Last name is required", ErrorType.Validation);
            }
            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                _logger.LogWarning("Phone number is required.");
                return Result<CreateClientDto>.Fail("Phone number is required", ErrorType.Validation);
            }

            client.FirstName = dto.FirstName;
            client.LastName = dto.LastName;
            client.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();


            return Result<CreateClientDto>.Ok(new CreateClientDto
            {
                id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                BirthDate = client.BirthDate,
                IsActive = client.IsActive,
                CreatedAt = client.CreatedAt
            });
        }
        catch (Exception)
        {
            _logger.LogError("An error while updating Client");
            return Result<CreateClientDto>.Fail("An error while updating Client", ErrorType.Unknown);
        }
    }

}