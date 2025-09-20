// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using Microsoft.AspNetCore.Mvc.RazorPages;
using NalinTransactionPersistence;

namespace NalinTransactionStoreAndRetrieval.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ITransactionPersistence transactionPersistence)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
