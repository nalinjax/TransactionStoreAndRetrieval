// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using NalinTransactionCommon;

namespace NalinTransactionPersistence
{
    public interface ITransactionPersistance
    {
        /// <summary>
        /// Persist transaction
        /// </summary>
        /// <returns>True if successful</returns>
        bool Persist(TransactionData transactionData); // persist data - adds

        /// <summary>
        /// Retrieve all persisted transactions
        /// </summary>
        List<TransactionData> Retrieve(); // retrieve given the ID
    }
}
