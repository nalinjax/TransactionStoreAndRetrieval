// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

namespace NalinLogging
{
    /// <summary>
    /// Basic logger
    /// </summary>
    public class NalinLogger : INalinLogger
    {
        /// <summary>
        /// Write to log
        /// </summary>
        public void Log(string level, string message)
        {
            Console.WriteLine($"Logged message: {level}, {message}");
        }
    }
}
