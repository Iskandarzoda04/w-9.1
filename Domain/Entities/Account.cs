using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Account
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid ClientId { get; set; }
    [Required, MaxLength(20)]
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; }
    [Required, StringLength(3)]
    public string Currency { get; set; } = "USD";
    [Required]
    public int Type { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Client Client { get; set; } = null!;

 public ICollection<Transaction> SentTransactions { get; set; } = new List<Transaction>();
public ICollection<Transaction> ReceivedTransactions { get; set; } = new List<Transaction>();
}
  