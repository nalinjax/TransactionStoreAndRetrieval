// Author: Nalin Jayasuriya
// Sep/20/2025 - Jacksonville FL

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NalinTransactionCommon;
using NalinTransactionPersistence;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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
            [Required, MaxLength(50, ErrorMessage = "Cannot exceed 50 chars")]
            public string Description { get; set; }

            [Required, MinLength(10, ErrorMessage = "Must be in mm-dd-yyyy format")]
            public string Date { get; set; }

            [Required]
            [Range(0.01, 9999999999)]
            public Decimal Amount { get; set; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AddTransactionModel(ITransactionPersistence transactionPersistence)
        {
            _transactionPersistence = transactionPersistence;
        }

        public void OnGet()
        {
            InputModel = new TransactionInput();
            InputModel.Date = DateTime.Now.ToString("MM-dd-yyyy");
        }

        /// <summary>
        /// When user clicks Submit on form
        /// </summary>
        public IActionResult OnPost()
        {
            // input validation first

            if (!ModelState.IsValid)
            {
                // Handle validation errors, e.g., re-display the form with error messages
                return Page();
            }

            if (InputModel.Description?.Length > 50)
            {
                ModelState.AddModelError("Description", "Description length must be <= 50 characters.");
                return Page();
            }

            if (InputModel.Amount < 0)
            {
                ModelState.AddModelError("Amount", "Amount must be positive.");
                return Page();
            }

            DateTime transactionDate;
            if (!DateTime.TryParseExact(InputModel.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out transactionDate))
            {
                ModelState.AddModelError("Date", "Date must be in yyyy/MM/dd format.");
                return Page();
            }

            // persist transaction

            var usdAmount = Math.Round(InputModel.Amount, 2); // limit to 2 decimals

            var dataToPersist = new TransactionData()
            {
                Description = InputModel.Description,
                Amount = usdAmount,
                Date = transactionDate,
                ID = Guid.NewGuid().ToString()  // associate with a unique ID
            };

            var successful = _transactionPersistence.PersistSingle(dataToPersist);

            if (!successful)
            {
                ModelState.AddModelError("", "ERROR: Failed to save transaction! Please inform support team");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
