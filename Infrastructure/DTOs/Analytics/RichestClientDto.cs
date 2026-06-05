namespace Infrastructure.DTOs.Analytics;

public class RichestClientDto
{
     public Guid ClientId { get; set; }
    public string FullName { get; set; } = null!;
    public decimal TotalBalance { get; set; }
}
