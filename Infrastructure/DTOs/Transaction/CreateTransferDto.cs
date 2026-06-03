public class CreateTransferDto
{
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public int Status  {get; set;}
    public string? Description { get; set; }
}