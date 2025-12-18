using Microsoft.EntityFrameworkCore;
using Modules.Finance.Domain.Entities;

namespace Modules.Finance.Infrastructure.Data;

public class FinanceDbContext : DbContext
{
    public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
    : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Uncategorized", Description = "Default category for unmatched transactions" },
            new Category { Id = 2, Name = "Groceries", Description = "Grocery shopping" },
            new Category { Id = 3, Name = "Food & Dining", Description = "Restaurants and dining out" },
            new Category { Id = 4, Name = "Transportation", Description = "Public transport and travel" },
            new Category { Id = 5, Name = "Healthcare", Description = "Medical and pharmacy expenses" },
            new Category { Id = 6, Name = "Entertainment", Description = "Streaming, games, and leisure" },
            new Category { Id = 7, Name = "Utilities", Description = "Phone, internet, and electricity" },
            new Category { Id = 8, Name = "Insurance", Description = "Insurance and security services" },
            new Category { Id = 9, Name = "Shopping", Description = "General retail shopping" },
            new Category { Id = 10, Name = "Education", Description = "Student loans and education expenses" },
            new Category { Id = 11, Name = "Income", Description = "Salary, benefits, and other income" },
            new Category { Id = 12, Name = "Membership", Description = "Union and membership fees" },
            new Category { Id = 13, Name = "Financial Services", Description = "Payment services and financial fees" },
            new Category { Id = 14, Name = "Swish", Description = "Swish payments" },
            new Category { Id = 15, Name = "ATM", Description = "Cash withdrawals" },
            new Category { Id = 16, Name = "Travel", Description = "Hotels and travel accommodations" },
            new Category { Id = 17, Name = "Personal Care", Description = "Haircuts and personal grooming" }
        );
    }
}
