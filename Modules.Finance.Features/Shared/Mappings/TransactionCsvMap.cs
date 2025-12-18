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
    public int RowNumber { get; set; }
    public string ClearingNumber { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string Product { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime CurrencyDate { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal BookedBalance { get; set; }
}