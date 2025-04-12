using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uintptrDPI
{
    public partial class Form1 : Form
    {
        private const string ReleasesUrl = "https://github.com/cagritaskn/GoodbyeDPI-Turkey/releases";
        private const string TargetFolder = @"C:\uintptrDPI";
        private const string CmdFileName = "service_install_dnsredir_turkey.cmd";
        private const string ServiceName = "GoodByeDPI";

        private readonly FileDownloader _fileDownloader;
        private readonly ServiceManager _serviceManager;

        public Form1()
        {
            InitializeComponent();
            _fileDownloader = new FileDownloader(progressBarDownload, labelStatus);
            _serviceManager = new ServiceManager(ServiceName);
        }

        private async void btnInstallService_Click(object sender, EventArgs e)
        {
            try
            {
                listBoxLogs.Items.Clear();
                Log("En son sürüm ZIP dosyası için bağlantı alınıyor...");
                
                string latestZipUrl = await GetLatestReleaseZipUrl();
                if (string.IsNullOrEmpty(latestZipUrl))
                {
                    Log("En son sürüm ZIP bağlantısı bulunamadı!", true);
                    return;
                }

                Log($"ZIP bağlantısı bulundu: {latestZipUrl}");
                string zipPath = Path.Combine(Path.GetTempPath(), "GoodbyeDPI-Turkey.zip");

                Log("ZIP dosyası indiriliyor...");
                await _fileDownloader.DownloadFileAsync(latestZipUrl, zipPath);
                Log("ZIP dosyası başarıyla indirildi.");

                Log($"Hedef klasör oluşturuluyor: {TargetFolder}");
                if (!Directory.Exists(TargetFolder))
                    Directory.CreateDirectory(TargetFolder);
                Log("Hedef klasör hazır.");

                Log("ZIP dosyası açılıyor...");
                System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, TargetFolder, true);
                Log("ZIP dosyası başarıyla açıldı.");

                string cmdFilePath = Path.Combine(TargetFolder, CmdFileName);
                if (!File.Exists(cmdFilePath))
                {
                    Log("CMD dosyası bulunamadı!", true);
                    return;
                }

                Log($"CMD dosyası bulundu: {cmdFilePath}");
                Log("CMD dosyası yönetici olarak çalıştırılıyor...");

                bool installationResult = await _serviceManager.InstallService(cmdFilePath);
                Log(installationResult ? "Servis başarıyla kuruldu." : "Servis kurulumu başarısız oldu!", !installationResult);

                if (installationResult)
                {
                    var serviceStatus = await _serviceManager.GetServiceStatus();
                    if (serviceStatus != null)
                    {
                        Log($"Servis durumu: {serviceStatus.Status}");
                        Log($"Başlangıç tipi: {serviceStatus.StartType}");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, "Servis kurulumu sırasında");
            }
        }

        private async Task<string> GetLatestReleaseZipUrl()
        {
            try
            {
                string apiUrl = "https://api.github.com/repos/cagritaskn/GoodbyeDPI-Turkey/releases/latest";

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "ServiceManagerApp");
                    var response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();
                    var regex = new Regex("\"browser_download_url\":\\s*\"(https://.*?\\.zip)\"");
                    var match = regex.Match(json);

                    if (match.Success)
                    {
                        return match.Groups[1].Value;
                    }

                    throw new Exception("API'den ZIP bağlantısı alınamadı.");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, "En son sürüm URL'si alınırken");
                return null;
            }
        }

        private void Log(string message, bool isError = false)
        {
            listBoxLogs.Items.Add($"{DateTime.Now:HH:mm:ss} - {message}");
            if (isError)
            {
                listBoxLogs.ForeColor = System.Drawing.Color.Red;
            }
        }

        private async void btnCheckStatus_Click(object sender, EventArgs e)
        {
            try
            {
                listBoxLogs.Items.Clear();
                var serviceStatus = await _serviceManager.GetServiceStatus();

                if (serviceStatus != null)
                {
                    Log($"Servis adı: {serviceStatus.Name}");
                    Log($"Durum: {serviceStatus.Status}");
                    Log($"Başlangıç tipi: {serviceStatus.StartType}");
                }
                else
                {
                    Log("Servis durumu alınamadı!", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, "Servis durumu kontrol edilirken");
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "C:\\uintptrDPI dizinindeki dosyaları silmek programın düzgün çalışmamasına neden olabilir.",
                "Uyarı",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}
