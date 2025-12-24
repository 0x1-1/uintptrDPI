using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceProcess;
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
        private bool _isUpdatingLanguage;

        public Form1()
        {
            InitializeComponent();
            _fileDownloader = new FileDownloader(progressBarDownload, lblDownloadStatus);
            _serviceManager = new ServiceManager(ServiceName);
            InitializeLanguageOptions();
            this.Load += new System.EventHandler(this.Form1_Load);
            UpdateUIResources();
            Log("Application starting...");
        }

        private void InitializeLanguageOptions()
        {
            _isUpdatingLanguage = true;
            comboLanguage.Items.Clear();
            comboLanguage.Items.Add("English");
            comboLanguage.Items.Add("Türkçe");
            comboLanguage.SelectedIndex = Thread.CurrentThread.CurrentUICulture.Name.StartsWith("tr", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
            _isUpdatingLanguage = false;
        }

        private void UpdateUIResources()
        {
            this.Text = Resources.TitleLabel;
            this.btnInstallService.Text = Resources.InstallServiceButton;
            this.btnStartService.Text = Resources.StartServiceButton;
            this.btnStopService.Text = Resources.StopServiceButton;
            this.btnUninstallService.Text = Resources.UninstallServiceButton;
            this.btnCheckStatus.Text = Resources.CheckStatusButton;
            this.groupActions.Text = Resources.ActionsGroupLabel;
            this.groupLogs.Text = Resources.LogsGroupLabel;
            this.lblTitle.Text = Resources.TitleLabel;
            this.lblSubtitle.Text = Resources.SubtitleLabel;
            this.lblDownloadStatus.Text = Resources.DownloadStatusReady;
            _fileDownloader.ProgressPrefix = Resources.DownloadStatusInProgress;
            this.lblLanguage.Text = Resources.LanguageLabel;
            _isUpdatingLanguage = true;
            comboLanguage.SelectedIndex = Thread.CurrentThread.CurrentUICulture.Name.StartsWith("tr", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
            _isUpdatingLanguage = false;
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            await CheckServiceStatus();
        }

        private async void ChangeLanguage(string lang)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            UpdateUIResources();
            await CheckServiceStatus();
        }

        private void comboLanguage_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isUpdatingLanguage)
                return;

            if (comboLanguage.SelectedIndex == 1)
            {
                ChangeLanguage("tr-TR");
                return;
            }

            ChangeLanguage("en-US");
        }

        private async void btnInstallService_Click(object? sender, EventArgs e)
        {
            try
            {
                Log("Starting installation...");
                lblDownloadStatus.Text = Resources.DownloadStatusStarting;
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
            finally
            {
                lblDownloadStatus.Text = Resources.DownloadStatusReady;
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
                var statusText = GetLocalizedStatusString(status.Status);
                var statusColors = GetStatusColors(status.Status);
                UpdateStatusUI(statusText, statusColors.backColor, statusColors.foreColor);
                Log($"Service status: {statusText}");
                bool isRunning = status.Status == ServiceControllerStatus.Running;
                btnStartService.Enabled = !isRunning;
                btnStopService.Enabled = isRunning;
                btnInstallService.Enabled = false;
                btnUninstallService.Enabled = true;
            }
            else
            {
                var statusColors = GetStatusColors(null);
                UpdateStatusUI(Resources.StatusNotInstalled, statusColors.backColor, statusColors.foreColor);
                Log("Service is not installed.");
                btnStartService.Enabled = false;
                btnStopService.Enabled = false;
                btnInstallService.Enabled = true;
                btnUninstallService.Enabled = false;
            }
        }

        private void UpdateStatusUI(string statusText, Color backColor, Color foreColor)
        {
            lblStatus.Text = $"{Resources.StatusLabel}: {statusText}";
            lblStatus.BackColor = backColor;
            lblStatus.ForeColor = foreColor;
        }

        private (Color backColor, Color foreColor) GetStatusColors(ServiceControllerStatus? status)
        {
            if (status == null)
            {
                return (Color.FromArgb(230, 235, 242), Color.FromArgb(90, 96, 102));
            }

            switch (status.Value)
            {
                case ServiceControllerStatus.Running:
                    return (Color.FromArgb(220, 245, 228), Color.FromArgb(29, 122, 64));
                case ServiceControllerStatus.Stopped:
                    return (Color.FromArgb(255, 242, 214), Color.FromArgb(170, 92, 0));
                case ServiceControllerStatus.Paused:
                    return (Color.FromArgb(255, 236, 240), Color.FromArgb(176, 49, 77));
                case ServiceControllerStatus.StopPending:
                case ServiceControllerStatus.StartPending:
                case ServiceControllerStatus.ContinuePending:
                case ServiceControllerStatus.PausePending:
                    return (Color.FromArgb(224, 232, 255), Color.FromArgb(60, 88, 160));
                default:
                    return (Color.FromArgb(230, 235, 242), Color.FromArgb(90, 96, 102));
            }
        }

        private string GetLocalizedStatusString(ServiceControllerStatus status)
        {
            switch (status)
            {
                case ServiceControllerStatus.Running: return Resources.StatusRunning;
                case ServiceControllerStatus.Stopped: return Resources.StatusStopped;
                case ServiceControllerStatus.Paused: return Resources.StatusPaused;
                case ServiceControllerStatus.StopPending: return Resources.StatusStopPending;
                case ServiceControllerStatus.StartPending: return Resources.StatusStartPending;
                case ServiceControllerStatus.ContinuePending: return Resources.StatusContinuePending;
                case ServiceControllerStatus.PausePending: return Resources.StatusPausePending;
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
            richTextBoxLogs.SelectionColor = isError ? Color.FromArgb(176, 49, 77) : Color.FromArgb(33, 37, 41);
            richTextBoxLogs.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            richTextBoxLogs.ScrollToCaret();
        }
    }
}
