// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using NalinTransactionCommon;

namespace NalinTransactionPersistence
{
    public interface ITransactionPersistence
    {
        /// <summary>
        /// Persist a single transaction
        /// </summary>
        /// <returns>True if successful</returns>
        bool PersistSingle(TransactionData transactionData); // persist data - adds

        /// <summary>
        /// Retrieve all persisted transactions
        /// </summary>
        List<TransactionData> RetrieveAll(); // retrieve given the ID
    }
}
