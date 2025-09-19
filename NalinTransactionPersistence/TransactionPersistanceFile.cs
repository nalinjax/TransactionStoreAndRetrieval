// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

using NalinTransactionCommon;
using System.Text.Json;

namespace NalinTransactionPersistence
{
    /// <summary>
    /// Persistance to a file on disk
    /// </summary>
    internal class TransactionPersistanceFile : ITransactionPersistance
    {
        private Object locker = new Object();

        //TODO: Add logging

        public bool Persist(TransactionData transactionData)
        {
            try
            {
                var serializedData = JsonSerializer.Serialize(transactionData);

                lock (locker)
                {

                    AddRecordToFileData(new string[] { serializedData });
                    return true;
                }
            }
            catch (Exception ex)
            {
                // TODO: log
            }

            return false;
        }

        public List<TransactionData> Retrieve()
        {
            try
            {
                var records = new List<TransactionData>();

                var existingRawRecords = GetFileData();
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

        private string[] GetFileData()
        {
            var records = File.ReadAllLines("asds");
            return records;
        }

        private bool AddRecordToFileData(string[] records)
        {
            File.AppendAllLines("asds", records);
            return true;
        }
    }
}
