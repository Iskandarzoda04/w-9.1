namespace Infrastructure.Persistence.DataContext;

using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set;}
    public DbSet<Transaction> Transactions { get; set;}
    public DbSet<Account> Accounts { get; set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientConfiguration).Assembly);
    }

}
