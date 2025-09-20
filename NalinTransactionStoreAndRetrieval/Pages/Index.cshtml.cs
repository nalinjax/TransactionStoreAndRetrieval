using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NalinTransactionCommon;
using NalinTransactionPersistence;

namespace NalinTransactionStoreAndRetrieval.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ITransactionPersistence transactionPersistence)
        {
            _logger = logger;

            // for Testing

            var data = new TransactionData()
            {
                Amount = 10,
                Date = DateTime.UtcNow,
                Description = "adsd",
                ID = new Guid().ToString(),
            };

            transactionPersistence.PersistSingle(data);

            var records = transactionPersistence.RetrieveAll();

        }

        public void OnGet()
        {

        }
    }
}
