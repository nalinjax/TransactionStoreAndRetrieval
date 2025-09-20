// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using NalinTransactionCommon;
using System.Text.Json;

namespace NalinTransactionPersistence
{
    /// <summary>
    /// Transaction data persistence - get/save
    /// </summary>
    public class TransactionPersistence : ITransactionPersistence
    {
        private Object locker = new Object();

        IDataPersistence _transactionPersistence;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transactionPersistence"></param>
        public TransactionPersistence(IDataPersistence transactionPersistence)
        {
            _transactionPersistence = transactionPersistence;

            //TODO: Add logging
        }


        /// <summary>
        /// Persist a transaction
        /// </summary>
        public bool PersistSingle(TransactionData transactionData)
        {
            try
            {
                var serializedData = JsonSerializer.Serialize(transactionData);

                lock (locker) // ensure consistency of result
                {

                    AddRecordsToData([serializedData]); // add single
                    return true;
                }
            }
            catch (Exception ex)
            {
                // TODO: log
            }

            return false;
        }

        /// <summary>
        /// Retrieve all transactions
        /// </summary>
        /// <returns></returns>
        public List<TransactionData> RetrieveAll()
        {
            try
            {
                var records = new List<TransactionData>();

                var existingRawRecords = GetAllData();
                foreach (var rawRecord in existingRawRecords)
                {
                    var record = JsonSerializer.Deserialize<TransactionData>(rawRecord);
                    records.Add(record);
                } // for

                return records;
            }
            catch (Exception ex)
            {
                // TODO: log
            }

            return null;
        }

        // -------------

        private string[] GetAllData()
        {
            return _transactionPersistence.GetData();
        }

        private bool AddRecordsToData(string[] records)
        {
            return _transactionPersistence.AddRecordData(records);
        }
    }
}
