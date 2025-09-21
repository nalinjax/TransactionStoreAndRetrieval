// Author: Nalin Jayasuriya
// Sep/20/2025 - Jacksonville FL

using System.Text.Json;

namespace NalinTransactionCurrencyAPI
{
    /// <summary>
    /// Country-currency operations API
    /// </summary>
    public class CurrencyOperationsAPI : ICurrencyOperationsAPI
    {
        const string baseUri = "https://api.fiscaldata.treasury.gov/services/api/fiscal_service/v1/accounting/od/rates_of_exchange";

        // for fetching all country-currencies
        const string AllCurrenciesAPIUrl = $"{baseUri}?fields=country_currency_desc&page[size]=10000";

        // for fetching currency rates by record data for a specific country-currency and specific data-range
        const string CurrencyRatesAPIUrl = $"{baseUri}?fields=exchange_rate,record_date&filter=country_currency_desc:in:([currency]),record_date:gte:[fromDate],record_date:lte:[toDate]&page[size]=1000";

        private CurrencyDataCache _currencyDataCache ;

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
        /// With caching
        /// </summary>
        public async Task<CurrencyRecord[]> GetAllSupportedCurrencies()
        {
            // if in cache, send for better performance
            if (_currencyDataCache != null)
            {
                return _currencyDataCache.CountryCurrencies;
            }

            using var client = new HttpClient();
            var data = await client.GetAsync(AllCurrenciesAPIUrl);

            if (data.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var recordData = await data.Content.ReadAsStringAsync();
            var wrapperData = JsonSerializer.Deserialize<CurrenciesRecordWrapper>(recordData);

            _currencyDataCache = new CurrencyDataCache()
            {
                CountryCurrencies = wrapperData.data
            };

            return wrapperData.data;
        }

        /// <summary>
        /// Get conversion rate for specified currency by date.
        /// </summary>
        public async Task<string> GetCurrencyRatesForDate(string countryCurrency, DateTime transactionDate, int lookBackMonths)
        {
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

            // find an exact match first by date
            var exactMatchDate = transactionDate.ToString("yyyy-MM-dd");
            var exactRateRecord = wrapperData.data.FirstOrDefault(d => d.record_date == exactMatchDate);
            if (exactRateRecord != null) 
            {
                return exactRateRecord.exchange_rate; // yey!
            }

            // any record is fine, because record are already filtered by acceptable date range (e.g. 6 months)
            return wrapperData.data.First().exchange_rate;
        }
    }
}
