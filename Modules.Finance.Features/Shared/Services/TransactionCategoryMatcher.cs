namespace Modules.Finance.Features.Shared.Services;

public class TransactionCategoryMatcher
{
    private static readonly Dictionary<string, string[]> CategoryKeywords = new()
    {
        ["Groceries"] = ["ICA", "MAXI ICA", "COOP", "HEMKOP", "CITY GROSS", "7-ELEVEN", "HEMMAKV"],
        ["Food & Dining"] = ["McDONALDS", "BURGER KING", "PIZZERIA", "SPAZIO", "OLEARYS", "PUB NESSIE", "LOCALI", "LEMON BAR"],
        ["Transportation"] = ["SKÅNETRAFIKEN", "SKANETRAFIKEN", "SJ AB", "EASYPARK"],
        ["Healthcare"] = ["FOLKTANDVÅRD", "APOTEK"],
        ["Entertainment"] = ["HBOMAX", "HBO NORDIC", "HELP.HBOMAX", "PLAYSTATION", "NETFLIX", "SPOTIFY", "PERINA SPELBUTIK", "SVENSKA SPEL", "BLIZZARD", "VR STUDION", "APPLE.COM/BILL"],
        ["Utilities"] = ["TELIA", "HALEBOP", "ÖRESUNDSKRAFT", "ONE.COM"],
        ["Insurance"] = ["IF SKADEFÖRSÄKRI", "SECTOR ALARM"],
        ["Shopping"] = ["IKEA", "DOLLARSTORE", "LAGER 157", "POLARN O. PYRET", "BLOMMOR", "VAELA", "CIRCLE K", "KIOSK", "PRESSBYRAN", "NORMAL", "HM SE", "H&M", "VOLT", "CUTTERS"],
        ["Education"] = ["CSN"],
        ["Income"] = ["DAGERSÄTTNING", "BARNBIDRAG", "I00000371775", "I00000368235", "LÖN", "CAPGEMINI", "INSÄTTNING"],
        ["Membership"] = ["UNIONEN"],
        ["Financial Services"] = ["QLIRO", "KLARNA", "KREDITKORT"],
        ["Swish"] = ["SWISH"],
        ["ATM"] = ["ATM"],
        ["Travel"] = ["COURTYARD", "MARRIOTT"],
        ["Personal Care"] = ["CUTTERS"]
    };

    public static string DetermineCategoryName(string description, string reference)
    {
        var searchText = $"{description} {reference}".ToUpperInvariant();

        foreach (var (category, keywords) in CategoryKeywords)
        {
            if (keywords.Any(keyword => searchText.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
            {
                return category;
            }
        }

        return "Uncategorized";
    }
}