// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using NalinTransactionCommon;

namespace NalinTransactionPersistence
{
    /// <summary>
    /// Persistance via a file on disk
    /// </summary>
    internal class TransactionPersistanceFile : ITransactionPersistance
    {
        public bool Persist(TransactionData transactionData)
        {
            //TODO:
            throw new NotImplementedException();
        }

        public List<TransactionData> Retrieve()
        {
            //TODO:
            throw new NotImplementedException();
        }
    }
}
