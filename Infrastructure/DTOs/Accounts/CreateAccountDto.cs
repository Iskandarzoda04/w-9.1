using Domain.Entities;

namespace Infrastructure.DTOs.Accounts;

public class CreateAccountDto
{
    public Guid ClientId { get; set; }
    public string Currency { get; set; } =null!;
    public int Type { get; set; }
}