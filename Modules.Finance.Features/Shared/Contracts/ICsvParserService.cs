using Modules.Finance.Domain.Entities;

namespace Modules.Finance.Features.Shared.Contracts;

public interface ICsvParserService
{
    Task<List<Transaction>> ParseTransactionsAsync(string filePath);
}