// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using Microsoft.AspNetCore.Mvc.RazorPages;
using NalinTransactionPersistence;

namespace NalinTransactionStoreAndRetrieval.Pages
{
    /// <summary>
    /// View transactions UI
    /// </summary>
    public class ViewTransactionsModel : PageModel
    {
        ITransactionPersistence _transactionPersistence;

        /// <summary>
        /// Input fields with validation
        /// </summary>
        public class TransactionDisplay
        {
            public string ID { get; set; }

            public string Description { get; set; }

            public DateTime DateTime { get; set; }

            public Decimal Amount { get; set; }
        }

        public List<TransactionDisplay> DisplayData { get; set; } // bound to UI

        public ViewTransactionsModel(ITransactionPersistence transactionPersistence)
        {
            _transactionPersistence = transactionPersistence;
        }

        public void OnGet()
        {
            // get records
            var rawRransactionList = _transactionPersistence.RetrieveAll();

            // prepare data to display in UI
            DisplayData = new List<TransactionDisplay>();

            foreach (var transaction in rawRransactionList)
            {
                var displayRecord = new TransactionDisplay()
                {
                    ID = transaction.ID,
                    Description = transaction.Description,
                    DateTime = transaction.Date,
                    //TODO: translate
                    Amount = transaction.Amount,
                };

                DisplayData.Add(displayRecord);
            }
        }
    }
}
