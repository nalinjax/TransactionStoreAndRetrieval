// Author: Nalin Jayasuriya
// Sep/19/2025 - Jacksonville FL

namespace NalinLogging
{
    /// <summary>
    /// Basic logger
    /// </summary>
    public interface INalinLogger
    {
        void Log(string level, string message);
    }
}
