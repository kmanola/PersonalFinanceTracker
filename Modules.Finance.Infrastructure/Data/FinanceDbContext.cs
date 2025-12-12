using Microsoft.EntityFrameworkCore;
using Modules.Finance.Domain.Entities;

namespace Modules.Finance.Infrastructure.Data;

public class FinanceDbContext : DbContext
{
    public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
    : base(options)
    { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}
