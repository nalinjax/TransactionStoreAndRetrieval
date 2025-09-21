// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NalinTransactionCurrencyAPI;
using NalinTransactionPersistence;


namespace NalinTransactionStoreAndRetrieval.Pages
{
    /// <summary>
    /// View transactions UI.
    /// Performs currency conversions for transactions to selected currency.
    /// </summary>
    public class ViewTransactionsModel : PageModel
    {
        const int RatesLookBackMonths = 6;

        ITransactionPersistence _transactionPersistence;

        ICurrencyOperationsAPI _currencyOperationsAPI;

        /// <summary>
        /// Input fields with validation
        /// </summary>
        public class TransactionDisplay
        {
            public string ID { get; set; }

            public string Message { get; set; }

            public string Description { get; set; }

            public DateTime DateTime { get; set; }

            public string OriginalAmount { get; set; } // original USD amount

            public string CurrencyRate { get; set; } // conversion rate

            public string ConvertedAmount { get; set; } // amount following conversion
        }

        public List<TransactionDisplay> DisplayData { get; set; } // bound to UI

        public List<SelectListItem> CountryCurrencyList { get; set; } // bound to UI

        public string SelectedCountryCurrency { get; set; } // bound to UI

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewTransactionsModel(ITransactionPersistence transactionPersistence, ICurrencyOperationsAPI currencyOperationsAPI)
        {
            _transactionPersistence = transactionPersistence;
            _currencyOperationsAPI = currencyOperationsAPI;
        }

        /// <summary>
        /// When page is requested
        /// </summary>
        public void OnGet()
        {
            SetupCountryCurrencies();

            // get all transactions
            var rawRransactionList = _transactionPersistence.RetrieveAll();

            // prepare data to display in UI
            DisplayData = new List<TransactionDisplay>();

            foreach (var transaction in rawRransactionList)
            {
                var rateValue = _currencyOperationsAPI.GetCurrencyRatesForDate(SelectedCountryCurrency, transaction.Date, RatesLookBackMonths).Result;

                var displayRecord = new TransactionDisplay()
                {
                    ID = transaction.ID,
                    Description = transaction.Description,
                    DateTime = transaction.Date,
                    OriginalAmount = transaction.Amount.ToString(),
                };

                if (rateValue != null )
                {
                    var rate = decimal.Parse(rateValue);
                    displayRecord.CurrencyRate = rate.ToString();
                    var convertedAmount = Math.Round(rate * transaction.Amount, 2); // limit to two decimals
                    displayRecord.ConvertedAmount = convertedAmount.ToString();
                }
                else
                {
                    displayRecord.Message = "ERROR - No rate available";
                }

                DisplayData.Add(displayRecord);
            }
        }

        /// <summary>
        /// When user clicks Submit on form
        /// </summary>
        public IActionResult OnPost()
        {
            SetupCountryCurrencies();

            if (!ModelState.IsValid)
            {
                // Handle validation errors, e.g., re-display the form with error messages
                return Page();
            }

            // look at SelectedCountryCurrency

            return Page();
        }

        private void SetupCountryCurrencies()
        {
            CountryCurrencyList = new List<SelectListItem>();

            // for displying in list
            var allCountryCurrencies = _currencyOperationsAPI.GetAllSupportedCurrencies().Result;
            foreach (var currency in allCountryCurrencies)
            {
                CountryCurrencyList.Add(new SelectListItem(currency.country_currency_desc, currency.country_currency_desc));
            }
            if (SelectedCountryCurrency == null)
            {
                SelectedCountryCurrency = CountryCurrencyList.First().Value; // default
            }
        }
    }
}
