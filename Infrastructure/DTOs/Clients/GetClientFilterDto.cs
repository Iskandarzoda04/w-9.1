using Infrastructure.Result;

namespace Infrastructure.DTOs.Clients;

public class GetClientFilterDto : PagedRequest

{
 public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}
