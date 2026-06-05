namespace Infrastructure.DTOs.Analytics;

public class ClientAccountDto
{
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; }
    public string Currency { get; set; } = null!;
}
