using System;

namespace EmployeeManagement.Utilities
{
    /// <summary>
    /// This class handler logging in the application.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Singleton instance of the logger to log the debugging or error messages.
        /// </summary>
        public static Logger Instance { get; private set; } = new Logger();
        private Logger()
        { }

        /// <summary>
        /// This method logs the method.
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            // Can be logged onto device or database.
            // for now showing in on to console.
            Console.WriteLine(message);
        }
    }
}