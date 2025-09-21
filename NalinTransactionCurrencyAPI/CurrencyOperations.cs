// Author: Nalin Jayasuriya
// Sep/20/2025 - Jacksonville FL

namespace NalinTransactionCurrencyAPI
{
    /// <summary>
    /// Country-currency operations via a provider
    /// </summary>
    public class CurrencyOperations : ICurrencyOperations
    {
        private CurrencyDataCache _currencyDataCache ;

        ICurrencyDataProvider _currencyDataProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        public CurrencyOperations(ICurrencyDataProvider currencyDataProvider)
        {
            _currencyDataProvider = currencyDataProvider;

            // setup caching
            _currencyDataCache = new CurrencyDataCache();
            _currencyDataCache.CurrencyRates = new Dictionary<string, string>();
        }

        /// <summary>
        /// Get all supported country-currencies.
        /// With caching for better performance.
        /// </summary>
        public async Task<CurrencyRecord[]> GetAllSupportedCurrencies()
        {
            // if in cache, send for better performance
            if (_currencyDataCache.CountryCurrencies != null)
            {
                return _currencyDataCache.CountryCurrencies;
            }

            // get from provider

            var allCurrencies = await _currencyDataProvider.GetAllSupportedCurrencies();

            if (allCurrencies == null) // no data?
            {
                return null;
            }
            
            return allCurrencies;
        }

        /// <summary>
        /// Get conversion rate for specified currency by date.
        /// With caching
        /// </summary>
        public async Task<string> GetCurrencyRatesForDate(string countryCurrency, DateTime transactionDate, int lookBackMonths)
        {
            var transactionDateFormatted = transactionDate.ToString("yyyy-MM-dd");
            var cacheKey = $"{countryCurrency}#{transactionDateFormatted}";

            // if in cache, send for better performance
            if (_currencyDataCache.CurrencyRates.TryGetValue(cacheKey, out string cachedRate))
            {
                return cachedRate;
            }

            // get from provider

            var currencyRates = await _currencyDataProvider.GetCurrencyRatesForDate(countryCurrency, transactionDate, lookBackMonths);
            if (currencyRates == null)
            {
                return null; // unable to retrieve
            }

            // find an exact match first by date
            var exactMatchDate = transactionDate.ToString("yyyy-MM-dd");
            var exactRateRecord = currencyRates.FirstOrDefault(d => d.record_date == exactMatchDate);
            if (exactRateRecord != null) 
            {
                _currencyDataCache.CurrencyRates.Add(cacheKey, exactRateRecord.exchange_rate); // cache
                return exactRateRecord.exchange_rate; // yey!
            }

            // any record is fine, because record are already filtered by acceptable date range (e.g. 6 months)
            var result = currencyRates.First().exchange_rate;
            _currencyDataCache.CurrencyRates.Add(cacheKey, result); // cache
            return result;
        }
    }
}
