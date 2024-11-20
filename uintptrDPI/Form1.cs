using System.Diagnostics;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Management;
using System.ServiceProcess;

namespace uintptrDPI
{
    public partial class Form1 : Form
    {
        private const string ReleasesUrl = "https://github.com/cagritaskn/GoodbyeDPI-Turkey/releases";
        private const string TargetFolder = @"C:\uintptrDPI";
        private const string CmdFileName = "service_install_dnsredir_turkey.cmd";

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnInstallService_Click(object sender, EventArgs e)
        {
            listBoxLogs.Items.Clear();
            try
            {
                Log("En son sürüm ZIP dosyasının bağlantısı alınıyor...");
                string latestZipUrl = await GetLatestReleaseZipUrl();
                if (string.IsNullOrEmpty(latestZipUrl))
                {
                    Log("En son sürüm ZIP bağlantısı bulunamadı!", true);
                    return;
                }
                Log($"ZIP bağlantısı bulundu: {latestZipUrl}");
                string zipPath = Path.Combine(Path.GetTempPath(), "GoodbyeDPI-Turkey.zip");
                Log("ZIP dosyası indiriliyor...");
                await DownloadFileAsync(latestZipUrl, zipPath);
                Log("ZIP dosyası başarıyla indirildi.");
                Log($"Hedef klasör oluşturuluyor: {TargetFolder}");
                if (!Directory.Exists(TargetFolder))
                    Directory.CreateDirectory(TargetFolder);
                Log("Hedef klasör hazır.");
                Log("ZIP dosyası çıkarılıyor...");
                ZipFile.ExtractToDirectory(zipPath, TargetFolder, true);
                Log("ZIP dosyası başarıyla çıkarıldı.");
                string cmdFilePath = Path.Combine(TargetFolder, CmdFileName);
                if (!File.Exists(cmdFilePath))
                {
                    Log("CMD dosyası bulunamadı!", true);
                    return;
                }
                Log($"CMD dosyası bulundu: {cmdFilePath}");

                Log("CMD dosyası yönetici olarak çalıştırılıyor...");
                bool cmdResult = RunAsAdmin(cmdFilePath, "Y");
                Log(cmdResult ? "Servis başarıyla kuruldu." : "Servis kurulamadı!", !cmdResult);

                if (cmdResult)
                {
                    Log("Servisin başlangıç türü kontrol ediliyor...");
                    EnsureServiceStartupType("GoodByeDPI");
                }
            }
            catch (Exception ex)
            {
                Log($"Hata: {ex.Message}", true);
            }
        }

        private async Task<string> GetLatestReleaseZipUrl()
        {
            try
            {
                string apiUrl = "https://api.github.com/repos/cagritaskn/GoodbyeDPI-Turkey/releases/latest";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "ServiceManagerApp");

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (!response.IsSuccessStatusCode)
                        throw new Exception("GitHub API'ye erişilemedi.");

                    string json = await response.Content.ReadAsStringAsync();

                    var regex = new Regex("\"browser_download_url\":\\s*\"(https://.*?\\.zip)\"");
                    Match match = regex.Match(json);
                    if (match.Success)
                    {
                        return match.Groups[1].Value;
                    }
                    else
                    {
                        throw new Exception("ZIP bağlantısı API'den alınamadı.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Hata: {ex.Message}", true);
                return null;
            }
        }

        private void EnsureServiceStartupType(string serviceName)
        {
            try
            {
                using (ServiceController sc = new ServiceController(serviceName))
                {
                    if (sc.Status == ServiceControllerStatus.Stopped)
                    {
                        Log($"Servis '{serviceName}' durdurulmuş. Başlatılıyor...");
                        sc.Start();
                        sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                        Log($"Servis '{serviceName}' başarıyla başlatıldı.");
                    }
                    else
                    {
                        Log($"Servis '{serviceName}' çalışıyor.");
                    }
                }

                using (ManagementObject service = new ManagementObject($"Win32_Service.Name='{serviceName}'"))
                {
                    service.Get();

                    string startMode = service["StartMode"]?.ToString();
                    if (startMode != "Auto")
                    {
                        Log($"Servis '{serviceName}' başlangıç türü '{startMode}' olarak ayarlanmış. 'Otomatik' olarak değiştiriliyor...");
                        service["StartMode"] = "Auto";
                        service.Put();
                        Log($"Servis '{serviceName}' başlangıç türü başarıyla 'Otomatik' olarak ayarlandı.");
                    }
                    else
                    {
                        Log($"Servis '{serviceName}' başlangıç türü zaten 'Otomatik'.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Servis '{serviceName}' başlangıç türü kontrolü sırasında hata: {ex.Message}", true);
            }
        }

        private async Task DownloadFileAsync(string url, string filePath)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        await response.Content.CopyToAsync(fs);
                    }
                }
            }
        }

        private bool RunAsAdmin(string filePath, string arguments)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c \"{filePath}\" {arguments}",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();

                    using (StreamWriter writer = process.StandardInput)
                    {
                        if (writer.BaseStream.CanWrite)
                        {
                            writer.WriteLine("Y");
                        }
                    }

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        Log($"CMD hatası: {error}", true);
                        return false;
                    }

                    Log($"CMD çıktısı: {output}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log($"Hata: {ex.Message}", true);
                return false;
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

        private void Close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "C:\\uintptrDPI dizinindeki dosyaları silmeniz halinde program düzgün çalışmayacaktır.",
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
