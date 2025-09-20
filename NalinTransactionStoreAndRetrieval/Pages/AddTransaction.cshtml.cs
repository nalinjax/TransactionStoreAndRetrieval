// Author: Nalin Jayasuriya
// Sep/20/2025 - Jacksonville FL

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NalinTransactionCommon;
using NalinTransactionPersistence;
using System.ComponentModel.DataAnnotations;

namespace NalinTransactionStoreAndRetrieval.Pages
{
    /// <summary>
    /// Add transaction UI
    /// </summary>
    public class AddTransactionModel : PageModel
    {
        ITransactionPersistence _transactionPersistence;

        [BindProperty]
        public TransactionInput? InputModel { get; set; }

        /// <summary>
        /// Input fields with validation
        /// </summary>
        public class TransactionInput
        {
            [Required, MaxLength(50, ErrorMessage = "Description cannot exceed 50 chars")]
            public string Description { get; set; }

            [Required]
            public DateTime DateTime { get; set; }

            [Required]
            public Decimal Amount { get; set; }
        }

        public AddTransactionModel(ITransactionPersistence transactionPersistence)
        {
            _transactionPersistence = transactionPersistence;
        }

        public void OnGet()
        {
            InputModel = new TransactionInput();
            InputModel.DateTime = DateTime.Now;
        }

        /// <summary>
        /// When user clicks Submit on form
        /// </summary>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors, e.g., re-display the form with error messages
                return Page();
            }

            // save transaction

            var dataToPersist = new TransactionData()
            {
                Description = InputModel.Description,
                Amount = InputModel.Amount,
                Date = InputModel.DateTime,
                ID = Guid.NewGuid().ToString()  // associate with a unique ID
            };

            var successful = _transactionPersistence.PersistSingle(dataToPersist);

            if (!successful)
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
