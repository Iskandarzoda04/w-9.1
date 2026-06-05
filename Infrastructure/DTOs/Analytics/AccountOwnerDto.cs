namespace Infrastructure.DTOs.Analytics;

public class AccountOwnerDto
{
     public string AccountNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public decimal Balance { get; set; }
}
