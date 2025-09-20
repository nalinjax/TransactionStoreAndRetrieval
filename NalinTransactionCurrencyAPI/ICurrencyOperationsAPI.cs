// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using static NalinTransactionCurrencyAPI.CurrencyOperationsAPI;

namespace NalinTransactionCurrencyAPI
{
    public interface ICurrencyOperationsAPI
    {

        /// <summary>
        /// Get all supported country-currencies
        /// </summary>

        public Task<CurrencyRecord[]> GetAllSupportCurrencies();

    }
}
