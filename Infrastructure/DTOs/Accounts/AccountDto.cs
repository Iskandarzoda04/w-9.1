using Domain.Entities;

namespace Infrastructure.DTOs.Accounts;

public class AccountDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; }
    public string Currency { get; set; } = null!;
    public int Type { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}