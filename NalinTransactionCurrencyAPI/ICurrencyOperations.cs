// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using static NalinTransactionCurrencyAPI.CurrencyOperations;

namespace NalinTransactionCurrencyAPI
{
    /// <summary>
    /// Country-currency operations API
    /// </summary>
    public interface ICurrencyOperations
    {
        /// <summary>
        /// Get all supported country-currencies
        /// </summary>

        public Task<CurrencyRecord[]> GetAllSupportedCurrencies();

        /// <summary>
        /// Get conversion rate for specified currency by date.
        /// </summary>
        public Task<string> GetCurrencyRatesForDate(string countryCurrency, DateTime transactionDate, int lookBackMonths);
    }
}
