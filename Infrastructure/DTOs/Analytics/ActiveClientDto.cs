namespace Infrastructure.DTOs.Analytics;

public class ActiveClientDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
