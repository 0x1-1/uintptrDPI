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
                Log("Fetching link for the latest ZIP file...");
                
                string latestZipUrl = await GetLatestReleaseZipUrl();
                if (string.IsNullOrEmpty(latestZipUrl))
                {
                    Log("Latest ZIP link not found!", true);
                    return;
                }

                Log($"ZIP link found: {latestZipUrl}");
                string zipPath = Path.Combine(Path.GetTempPath(), "GoodbyeDPI-Turkey.zip");

                Log("ZIP file is being downloaded...");
                await _fileDownloader.DownloadFileAsync(latestZipUrl, zipPath);
                Log("ZIP file successfully downloaded.");

                Log($"Target folder is being created: {TargetFolder}");
                if (!Directory.Exists(TargetFolder))
                    Directory.CreateDirectory(TargetFolder);
                Log("Target folder is ready.");

                Log("ZIP file is being extracted...");
                System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, TargetFolder, true);
                Log("ZIP file successfully extracted.");

                string cmdFilePath = Path.Combine(TargetFolder, CmdFileName);
                if (!File.Exists(cmdFilePath))
                {
                    Log("CMD file not found!", true);
                    return;
                }

                Log($"CMD file found: {cmdFilePath}");
                Log("CMD file is being run as administrator...");

                bool installationResult = await _serviceManager.InstallService(cmdFilePath);
                Log(installationResult ? "Service successfully installed." : "Service installation failed!", !installationResult);

                if (installationResult)
                {
                    var serviceStatus = await _serviceManager.GetServiceStatus();
                    if (serviceStatus != null)
                    {
                        Log($"Service status: {serviceStatus.Status}");
                        Log($"Startup type: {serviceStatus.StartType}");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, "An error occurred during service installation.");
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

                    throw new Exception("Failed to retrieve the ZIP link from the API.");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, "An error occurred while retrieving the latest release URL.");
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
                    Log($"Service name: {serviceStatus.Name}");
                    Log($"Status: {serviceStatus.Status}");
                    Log($"Startup type: {serviceStatus.StartType}");
                }
                else
                {
                    Log("Service status could not be retrieved!", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, "An error occurred while checking the service status.");
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Deleting files in the C:\\uintptrDPI directory may cause the program to malfunction.",
                "Warning",
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
