using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uintptrDPI
{
    public class FileDownloader
    {
        private readonly HttpClient _httpClient;
        private readonly ProgressBar _progressBar;
        private readonly Label _statusLabel;

        public FileDownloader(ProgressBar progressBar = null, Label statusLabel = null)
        {
            _httpClient = new HttpClient();
            _progressBar = progressBar;
            _statusLabel = statusLabel;
        }

        public async Task<string> DownloadFileAsync(string url, string destinationPath, string expectedHash = null)
        {
            try
            {
                using (var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                    var canReportProgress = totalBytes != -1 && _progressBar != null;

                    if (canReportProgress)
                    {
                        _progressBar.Maximum = (int)totalBytes;
                        _progressBar.Value = 0;
                    }

                    using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        var buffer = new byte[8192];
                        var totalRead = 0L;
                        var bytesRead = 0;

                        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            totalRead += bytesRead;

                            if (canReportProgress)
                            {
                                _progressBar.Value = (int)totalRead;
                                _statusLabel?.Invoke((MethodInvoker)(() => 
                                    _statusLabel.Text = $"İndiriliyor: {totalRead * 100 / totalBytes}%"
                                ));
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(expectedHash))
                    {
                        var actualHash = await CalculateFileHash(destinationPath);
                        if (actualHash != expectedHash)
                        {
                            File.Delete(destinationPath);
                            throw new Exception("Dosya hash değeri doğrulaması başarısız oldu.");
                        }
                    }

                    return destinationPath;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, "Dosya indirme işlemi sırasında");
                throw;
            }
        }

        private async Task<string> CalculateFileHash(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                var hash = await Task.Run(() => md5.ComputeHash(stream));
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
} 