using CsvHelper.Configuration;
using Modules.Finance.Domain.Entities;

namespace Modules.Finance.Features.Shared.Mappings;

public sealed class TransactionCsvMap : ClassMap<TransactionCsvRecord>
{
    public TransactionCsvMap()
    {
        Map(m => m.RowNumber).Name("Radnummer");
        Map(m => m.ClearingNumber).Name("Clearingnummer");
        Map(m => m.AccountNumber).Name("Kontonummer");
        Map(m => m.Product).Name("Produkt");
        Map(m => m.Currency).Name("Valuta");
        Map(m => m.BookingDate).Name("Bokföringsdag");
        Map(m => m.TransactionDate).Name("Transaktionsdag");
        Map(m => m.CurrencyDate).Name("Valutadag");
        Map(m => m.Reference).Name("Referens");
        Map(m => m.Description).Name("Beskrivning");
        Map(m => m.Amount).Name("Belopp");
        Map(m => m.BookedBalance).Name("Bokfört saldo");
    }
}

public record TransactionCsvRecord
{
    public int RowNumber { get; init; }
    public string ClearingNumber { get; init; } = string.Empty;
    public string AccountNumber { get; init; } = string.Empty;
    public string Product { get; init; } = string.Empty;
    public string Currency { get; init; } = string.Empty;
    public DateTime BookingDate { get; init; }
    public DateTime TransactionDate { get; init; }
    public DateTime CurrencyDate { get; init; }
    public string Reference { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public decimal BookedBalance { get; init; }
}