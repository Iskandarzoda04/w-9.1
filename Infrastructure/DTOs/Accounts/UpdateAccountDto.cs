using Domain.Entities;

public class UpdateAccountDto
{
    public string Currency { get; set; } = null!;
    public int Type { get; set; }
    public bool IsActive { get; set; }
}