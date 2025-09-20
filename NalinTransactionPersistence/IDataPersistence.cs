namespace NalinTransactionPersistence
{
    // Author: Nalin Jayasuriya
    // Sep/20/2025 - Jacksonville FL

    public interface IDataPersistence
    {
        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns>records or null if failed</returns>
        public string[] GetData();

        /// <summary>
        /// Add a set of data records 
        /// </summary>
        /// <returns>true if successful</returns>
        public bool AddRecordData(string[] records);
    }
}
