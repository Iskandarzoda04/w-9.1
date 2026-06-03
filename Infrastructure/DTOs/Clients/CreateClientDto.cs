namespace Infrastructure.DTOs.Clients;

public class CreateClientDto
{
    internal Guid id;


    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public bool IsActive { get; internal set; }
    public DateTime CreatedAt { get; internal set; }
}