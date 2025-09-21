// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

using System.Text.Json;

namespace NalinTransactionCurrencyAPI
{
    /// <summary>
    /// Treasury currency data provider
    /// </summary>
    public class TreasuryCurrencyDataProvider : ICurrencyDataProvider
    {
        const string baseUri = "https://api.fiscaldata.treasury.gov/services/api/fiscal_service/v1/accounting/od/rates_of_exchange";

        // for fetching all country-currencies
        const string AllCurrenciesAPIUrl = $"{baseUri}?fields=country_currency_desc&page[size]=10000";

        // for fetching currency rates by record data for a specific country-currency and specific data-range
        const string CurrencyRatesAPIUrl = $"{baseUri}?fields=exchange_rate,record_date&filter=country_currency_desc:in:([currency]),record_date:gte:[fromDate],record_date:lte:[toDate]&page[size]=1000";

        /// <summary>
        /// Wrapper for all country currencies - source is JSON
        /// </summary>
        public class CurrenciesRecordWrapper
        {
            public CurrencyRecord[] data { get; set; } // all the currency records
        }

        /// <summary>
        /// Wrapper for currencies rates by date - source is JSON
        /// </summary>
        public class RatesRecordWrapper
        {
            public CurrencyRateRecord[] data { get; set; }
        }

        /// <summary>
        /// Get all supported country-currencies.
        /// </summary>
        public async Task<CurrencyRecord[]> GetAllSupportedCurrencies()
        {
            using var client = new HttpClient();
            var data = await client.GetAsync(AllCurrenciesAPIUrl);

            if (data.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var recordData = await data.Content.ReadAsStringAsync();
            var wrapperData = JsonSerializer.Deserialize<CurrenciesRecordWrapper>(recordData);

            return wrapperData.data;
        }

        /// <summary>
        /// Get currency data for date
        /// </summary>
        public async Task<CurrencyRateRecord[]> GetCurrencyRatesForDate(string countryCurrency, DateTime transactionDate, int lookBackMonths)
        {
            var transactionDateFormatted = transactionDate.ToString("yyyy-MM-dd");

            var fromDate = transactionDate.AddMonths(-lookBackMonths).ToString("yyyy-MM-dd");
            var toDate = transactionDate.ToString("yyyy-MM-dd");

            var apiUrl = CurrencyRatesAPIUrl.Replace("[currency]", countryCurrency);
            apiUrl = apiUrl.Replace("[fromDate]", fromDate);
            apiUrl = apiUrl.Replace("[toDate]", toDate);

            using var client = new HttpClient();
            var data = await client.GetAsync(apiUrl);

            if (data.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var rateData = await data.Content.ReadAsStringAsync();
            var wrapperData = JsonSerializer.Deserialize<RatesRecordWrapper>(rateData);

            if (wrapperData.data.Length == 0)
            {
                return null; // no data
            }

            return wrapperData.data;
        }
    }
}
