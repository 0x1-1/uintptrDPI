using System;
using System.Windows.Forms;

namespace uintptrDPI
{
    public static class ErrorHandler
    {
        public static void HandleError(Exception ex, string context, bool showMessageBox = true)
        {
            string errorMessage = $"Hata Detayları:\n" +
                                $"Bağlam: {context}\n" +
                                $"Hata Türü: {ex.GetType().Name}\n" +
                                $"Mesaj: {ex.Message}\n" +
                                $"Stack Trace: {ex.StackTrace}";

            // Log dosyasına kaydet
            LogToFile(errorMessage);

            // Kullanıcıya göster
            if (showMessageBox)
            {
                MessageBox.Show(
                    $"Bir hata oluştu:\n{ex.Message}\n\n" +
                    "Daha fazla detay için log dosyasını kontrol edin.",
                    "Hata",
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
                // Log yazma hatası durumunda sessizce devam et
            }
        }
    }
} 