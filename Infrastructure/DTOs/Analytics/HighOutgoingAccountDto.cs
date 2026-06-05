namespace Infrastructure.DTOs.Analytics;

public class HighOutgoingAccountDto
{
     public string AccountNumber { get; set; } = null!;
    public decimal TotalOutgoing { get; set; }
}
