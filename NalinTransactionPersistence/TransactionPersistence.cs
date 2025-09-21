// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using NalinLogging;
using NalinTransactionCommon;
using System.Text.Json;

namespace NalinTransactionPersistence
{
    /// <summary>
    /// Transaction data persistence - get/save
    /// Has logging.
    /// </summary>
    public class TransactionPersistence : ITransactionPersistence
    {
        private Object locker = new Object();

        IDataPersistence _transactionPersistence;

        INalinLogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transactionPersistence"></param>
        public TransactionPersistence(IDataPersistence transactionPersistence, INalinLogger logger)
        {
            _transactionPersistence = transactionPersistence;
            _logger = logger;
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
                _logger.Log("Exception", ex.ToString());
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
                _logger.Log("Exception", ex.ToString());
            }

            return null;
        }

        // ------------- private

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
