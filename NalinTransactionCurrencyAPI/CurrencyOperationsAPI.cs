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
        const string AllCurrenciesAPIUrl = "https://api.fiscaldata.treasury.gov/services/api/fiscal_service/v1/accounting/od/rates_of_exchange?fields=country_currency_desc&page[size]=10000";

        /// <summary>
        /// Wrapper for all country currencies
        /// </summary>
        public class currenciesRecordWrapper
        {
            public CurrencyRecord[] data { get; set; } // all the currency records
        }

        /// <summary>
        /// For each country currency
        /// </summary>
        public class CurrencyRecord
        {
            public string country_currency_desc { get; set; } // e.g. Australia-Dollar
        }

        /// <summary>
        /// Get all supported country-currencies
        /// </summary>
        public async Task<CurrencyRecord[]> GetAllSupportCurrencies()
        {
            HttpClient client = new HttpClient();
            var data = await client.GetAsync(AllCurrenciesAPIUrl);

            if (data.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var recordData = await data.Content.ReadAsStringAsync();

            var  wrapperData =   JsonSerializer.Deserialize<currenciesRecordWrapper>(recordData);

            return wrapperData.data;
        }
    }
}
