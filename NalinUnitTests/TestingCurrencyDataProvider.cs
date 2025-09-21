// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

using NalinTransactionCurrencyAPI;
using System.Globalization;

namespace NalinUnitTests
{
    /// <summary>
    /// Testing currency data provider
    /// </summary>
    public class TestingCurrencyDataProvider : ICurrencyDataProvider
    {
        /// <summary>
        /// Get all supported country-currencies.
        /// </summary>
        public async Task<CurrencyRecord[]> GetAllSupportedCurrencies()
        {
            var canadaRecord = new CurrencyRecord
            {
                country_currency_desc = "Canada-Dollar"
            };
            var australiaRecord = new CurrencyRecord
            {
                country_currency_desc = "Australia-Dollar"
            };

            var testCurrencyRecords = new CurrencyRecord[] { canadaRecord, australiaRecord };

            return testCurrencyRecords;
        }

        /// <summary>
        /// Get currency data for date
        /// </summary>
        public async Task<CurrencyRateRecord[]> GetCurrencyRatesForDate(string countryCurrency, DateTime transactionDate, int lookBackMonths)
        {
            var marginDate = DateTime.ParseExact("2025-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            if (transactionDate >= marginDate)
            {
                var testCurrencyRateRecords1 = new CurrencyRateRecord[] {
                new CurrencyRateRecord()
                {
                   exchange_rate = "0.97",
                   record_date = "2025-09-01"
                }};

                return testCurrencyRateRecords1;
            }

            return null; // for not found
        }
    }
}
