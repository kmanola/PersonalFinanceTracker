namespace Modules.Finance.Features.Shared.Services;

public class TransactionCategoryMatcher
{
    private static readonly Dictionary<string, string[]> CategoryKeywords = new()
    {
        ["Groceries"] = ["ICA", "MAXI ICA", "COOP", "HEMKOP", "CITY GROSS"],
        ["Food & Dining"] = ["McDONALDS", "BURGER KING", "PIZZERIA", "SPAZIO"],
        ["Transportation"] = ["SKÅNETRAFIKEN"],
        ["Healthcare"] = ["FOLKTANDVÅRD", "APOTEK"],
        ["Entertainment"] = ["HBOMAX", "PLAYSTATION", "NETFLIX", "SPOTIFY", "PERINA SPELBUTIK", "SVENSKA SPEL"],
        ["Utilities"] = ["TELIA", "HALEBOP", "ÖRESUNDSKRAFT", "ONE.COM"],
        ["Insurance"] = ["IF SKADEFÖRSÄKRI", "SECTOR ALARM"],
        ["Shopping"] = ["IKEA", "DOLLARSTORE", "LAGER 157", "POLARN O. PYRET", "BLOMMOR", "VAELA", "CIRCLE K", "KIOSK", "PRESSBYRAN"],
        ["Education"] = ["CSN"],
        ["Income"] = ["DAGERSÄTTNING", "BARNBIDRAG", "I00000371775"],
        ["Membership"] = ["UNIONEN"],
        ["Financial Services"] = ["QLIRO"],
        ["Swish"] = ["SWISH"],
        ["ATM"] = ["ATM KNUTPUNKTEN"]
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