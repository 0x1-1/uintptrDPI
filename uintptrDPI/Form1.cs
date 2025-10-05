using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using uintptrDPI.Properties;

namespace uintptrDPI
{
    public partial class Form1 : Form
    {
        private const string TargetFolder = @"C:\uintptrDPI";
        private const string InstallCmdFileName = "service_install_dnsredir_turkey.cmd";
        private const string UninstallCmdFileName = "service_uninstall.cmd"; // Assuming this is the name
        private const string ServiceName = "GoodByeDPI";

        private readonly FileDownloader _fileDownloader;
        private readonly ServiceManager _serviceManager;

        public Form1()
        {
            InitializeComponent();
            _fileDownloader = new FileDownloader(progressBarDownload, null);
            _serviceManager = new ServiceManager(ServiceName);
            this.Load += new System.EventHandler(this.Form1_Load);
            UpdateUIResources();
        }

        private void UpdateUIResources()
        {
            this.Text = Resources.TitleLabel;
            this.languageToolStripMenuItem.Text = Resources.LanguageLabel;
            this.englishToolStripMenuItem.Text = "English";
            this.turkishToolStripMenuItem.Text = "Türkçe";
            this.btnInstallService.Text = Resources.InstallServiceButton;
            this.btnStartService.Text = Resources.StartServiceButton;
            this.btnStopService.Text = Resources.StopServiceButton;
            this.btnUninstallService.Text = Resources.UninstallServiceButton;
            this.btnCheckStatus.Text = Resources.CheckStatusButton;
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            await CheckServiceStatus();
        }

        private async void ChangeLanguage(string lang)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            UpdateUIResources();
            await CheckServiceStatus(); // Re-check and update status label in the new language
        }

        private void englishToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            ChangeLanguage("en-US");
        }

        private void turkishToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            ChangeLanguage("tr-TR");
        }

        private async void btnInstallService_Click(object? sender, EventArgs e)
        {
            try
            {
                Log("Starting installation...");
                string? latestZipUrl = await GetLatestReleaseZipUrl();
                if (string.IsNullOrEmpty(latestZipUrl))
                {
                    Log("Could not find the latest release URL.", true);
                    return;
                }

                string zipPath = Path.Combine(Path.GetTempPath(), "GoodbyeDPI-Turkey.zip");
                await _fileDownloader.DownloadFileAsync(latestZipUrl, zipPath);
                
                if (!Directory.Exists(TargetFolder))
                    Directory.CreateDirectory(TargetFolder);

                System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, TargetFolder, true);

                string cmdFilePath = Path.Combine(TargetFolder, InstallCmdFileName);
                if (!File.Exists(cmdFilePath))
                {
                    Log($"{InstallCmdFileName} not found!", true);
                    return;
                }

                bool result = await _serviceManager.InstallService(cmdFilePath);
                Log(result ? "Service installed successfully." : "Service installation failed.", !result);
                await CheckServiceStatus();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, "An error occurred during service installation.");
            }
        }

        private async void btnStartService_Click(object? sender, EventArgs e)
        {
            bool result = await _serviceManager.StartService();
            Log(result ? "Service started successfully." : "Failed to start service.", !result);
            await CheckServiceStatus();
        }

        private async void btnStopService_Click(object? sender, EventArgs e)
        {
            bool result = await _serviceManager.StopService();
            Log(result ? "Service stopped successfully." : "Failed to stop service.", !result);
            await CheckServiceStatus();
        }

        private async void btnUninstallService_Click(object? sender, EventArgs e)
        {
            string cmdFilePath = Path.Combine(TargetFolder, UninstallCmdFileName);
            if (!File.Exists(cmdFilePath))
            {
                Log($"{UninstallCmdFileName} not found! Cannot uninstall.", true);
                return;
            }
            bool result = await _serviceManager.UninstallService(cmdFilePath);
            Log(result ? "Service uninstalled successfully." : "Service uninstallation failed.", !result);
            await CheckServiceStatus();
        }

        private async void btnCheckStatus_Click(object? sender, EventArgs e)
        {
            await CheckServiceStatus();
        }

        private async Task CheckServiceStatus()
        {
            var status = await _serviceManager.GetServiceStatus();
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(async () => await CheckServiceStatus()));
                return;
            }

            if (status != null)
            {
                lblStatus.Text = $"{Resources.StatusLabel}: {GetLocalizedStatusString(status.Status)}";
                bool isRunning = status.Status == System.ServiceProcess.ServiceControllerStatus.Running;
                btnStartService.Enabled = !isRunning;
                btnStopService.Enabled = isRunning;
                btnInstallService.Enabled = false; // Should only be enabled if service is not installed
                btnUninstallService.Enabled = true;
            }
            else
            {
                lblStatus.Text = $"{Resources.StatusLabel}: {Resources.StatusNotInstalled}";
                btnStartService.Enabled = false;
                btnStopService.Enabled = false;
                btnInstallService.Enabled = true;
                btnUninstallService.Enabled = false;
            }
        }

        private string GetLocalizedStatusString(System.ServiceProcess.ServiceControllerStatus status)
        {
            switch (status)
            {
                case System.ServiceProcess.ServiceControllerStatus.Running: return Resources.StatusRunning;
                case System.ServiceProcess.ServiceControllerStatus.Stopped: return Resources.StatusStopped;
                case System.ServiceProcess.ServiceControllerStatus.Paused: return Resources.StatusPaused;
                case System.ServiceProcess.ServiceControllerStatus.StopPending: return Resources.StatusStopPending;
                case System.ServiceProcess.ServiceControllerStatus.StartPending: return Resources.StatusStartPending;
                case System.ServiceProcess.ServiceControllerStatus.ContinuePending: return Resources.StatusContinuePending;
                case System.ServiceProcess.ServiceControllerStatus.PausePending: return Resources.StatusPausePending;
                default: return status.ToString();
            }
        }

        private async Task<string?> GetLatestReleaseZipUrl()
        {
            try
            {
                string apiUrl = "https://api.github.com/repos/cagritaskn/GoodbyeDPI-Turkey/releases/latest";
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "uintptrDPI-App");
                    var response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string json = await response.Content.ReadAsStringAsync();
                    var regex = new Regex(@"(https://.*?\.zip)");
                    var match = regex.Match(json);
                    return match.Success ? match.Groups[1].Value : null;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex, "Could not get latest release URL.");
                return null;
            }
        }

        private void Log(string message, bool isError = false)
        {
            if (richTextBoxLogs.InvokeRequired)
            {
                richTextBoxLogs.Invoke(new Action(() => Log(message, isError)));
                return;
            }
            richTextBoxLogs.SelectionColor = isError ? Color.Red : Color.White;
            richTextBoxLogs.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            richTextBoxLogs.ScrollToCaret();
        }
    }
}