namespace Modules.Finance.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public TransactionType Type { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public int? BudgetId { get; set; }
    public Budget? Budget { get; set; }
}

public enum TransactionType
{
    Income,
    Expense,
    Transfer
}