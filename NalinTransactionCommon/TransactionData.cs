// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

namespace NalinTransactionCommon
{
    /// <summary>
    /// Transaction data
    /// </summary>
    public class TransactionData
    {
        public string ID { get; set; }

        public string Description {  get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }   // source is USD
    }
}
