using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Transaction
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid FromAccountId { get; set; }
    [Required]
    public Guid ToAccountId { get; set; }
    [Required]
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
    [Required]
    public int Status { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public Account FromAccount { get; set; } = null!;
    public Account ToAccount { get; set; } = null!;
}