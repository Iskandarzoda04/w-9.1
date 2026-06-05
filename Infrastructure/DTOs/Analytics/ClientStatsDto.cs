namespace Infrastructure.DTOs.Analytics;

public class ClientStatsDto
{
     public string FullName { get; set; } = null!;
    public int AccountsCount { get; set; }
    public decimal TotalBalance { get; set; }
}
