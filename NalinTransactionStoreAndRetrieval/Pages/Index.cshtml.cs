using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NalinTransactionPersistence;

namespace NalinTransactionStoreAndRetrieval.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IDataPersistance dataPersistance)
        {
            _logger = logger;

            // for Testing

            dataPersistance.AddRecordData(["hello"]);

        }

        public void OnGet()
        {

        }
    }
}
