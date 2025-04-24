using System;
using System.Windows.Forms;

namespace uintptrDPI
{
    public static class ErrorHandler
    {
        public static void HandleError(Exception ex, string context, bool showMessageBox = true)
        {
            string errorMessage = $"Error Details:\n" +
                                $"Context: {context}\n" +
                                $"Error Type: {ex.GetType().Name}\n" +
                                $"Message: {ex.Message}\n" +
                                $"Stack Trace: {ex.StackTrace}";

            // Log dosyasına kaydet
            LogToFile(errorMessage);

            // Kullanıcıya göster
            if (showMessageBox)
            {
                MessageBox.Show(
                    $"An error occurred:\n{ex.Message}\n\n" +
                    "Check the log file for more details.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private static void LogToFile(string message)
        {
            try
            {
                string logPath = Path.Combine(Application.StartupPath, "logs");
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                string logFile = Path.Combine(logPath, $"error_{DateTime.Now:yyyyMMdd}.log");
                File.AppendAllText(logFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n\n");
            }
            catch
            {
                // Continue silently in case of a log writing error
            }
        }
    }
}