// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

namespace NalinTransactionCurrencyAPI
{
    /// <summary>
    /// Currency data provider
    /// </summary>
    public interface ICurrencyDataProvider
    {
        public Task<CurrencyRecord[]> GetAllSupportedCurrencies();

        public Task<CurrencyRateRecord[]> GetCurrencyRatesForDate(string countryCurrency, DateTime transactionDate, int lookBackMonths);
    }
}
