using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Modules.Finance.Domain.Entities;
using Modules.Finance.Features.Shared.Contracts;
using Modules.Finance.Features.Shared.Mappings;
using Modules.Finance.Infrastructure.Data;

namespace Modules.Finance.Features.Shared.Services;

public class CsvParserService : ICsvParserService
{
    private readonly FinanceDbContext _dbContext;

    public CsvParserService(FinanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Transaction>> ParseTransactionsAsync(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
            TrimOptions = TrimOptions.Trim,
            BadDataFound = null,
            MissingFieldFound = null
        };

        var categories = await _dbContext.Categories
            .AsNoTracking()
            .ToListAsync();

        var categoryCache = categories.ToDictionary(c => c.Name, c => c.Id, StringComparer.OrdinalIgnoreCase);

        using var reader = new StreamReader(filePath, Encoding.GetEncoding("Windows-1252"));
        using var csv = new CsvReader(reader, config);

        csv.Context.RegisterClassMap<TransactionCsvMap>();

        // Skip the first line (comment line)
        await reader.ReadLineAsync();

        var records = csv.GetRecordsAsync<TransactionCsvRecord>();
        var transactions = new List<Transaction>();

        await foreach (var record in records)
        {
            var categoryName = TransactionCategoryMatcher.DetermineCategoryName(
                record.Description,
                record.Reference);

            // TODO Fixa category id igenom listan
            var categoryId = 1;

            transactions.Add(new Transaction
            {
                Name = record.Reference,
                Description = record.Description,
                Amount = Math.Abs(record.Amount),
                Date = record.TransactionDate,
                Type = DetermineTransactionType(record),
                CategoryId = categoryId
            });
        }

        return transactions;
    }

    private static TransactionType DetermineTransactionType(TransactionCsvRecord record)
    {
        if (record.Description.Contains("Överföring via internet", StringComparison.OrdinalIgnoreCase)
            || record.Description.Contains("+46706031322"))
        {
            return TransactionType.Transfer;
        }

        return record.Amount > 0 ? TransactionType.Income : TransactionType.Expense;
    }
}
