// Author: Nalin Jayasuriya
// Sep/21/2025 - Jacksonville FL

namespace NalinTransactionCurrencyAPI
{
    /// <summary>
    /// Currency rate record - source is JSON
    /// </summary>
    public class CurrencyRateRecord
    {
        public string exchange_rate { get; set; } // e.g. 1.352

        public string record_date { get; set; } // e.g. 2024-09-30
    }
}
