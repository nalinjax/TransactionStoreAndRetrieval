// Author: Nalin Jayasuriya
// Sep/20/2025 - Jacksonville FL

namespace NalinTransactionPersistence
{
    /// <summary>
    /// Data persitance - disk file
    /// </summary>
    public class FileDataPersistence : IDataPersistence
    {
        string _fileName = null;

        public FileDataPersistence(string folderName)
        {
            _fileName = Path.Combine(folderName, "transactions.txt");
        }

        /// <summary>
        /// Add a set of data records 
        /// </summary>
        /// <returns>true if successful</returns>
        public bool AddRecordData(string[] records)
        {
            File.AppendAllLines(_fileName, records);

            return true;
        }

        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns>records or null if failed</returns>
        public string[] GetData()
        {
            var records = File.ReadAllLines(_fileName);
            return records;
        }
    }
}
