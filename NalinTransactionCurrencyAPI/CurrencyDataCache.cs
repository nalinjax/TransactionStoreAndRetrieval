// Author: Nalin Jayasuriya
// Sep/20/2025 - Jacksonville FL

namespace NalinTransactionCurrencyAPI
{
    /// <summary>
    /// Caching for better performance
    /// </summary>
    internal class CurrencyDataCache
    {
        internal CurrencyRecord[] CountryCurrencies { get; set; }

        internal Dictionary<string, string> CurrencyRates { get; set; }
    }
}
