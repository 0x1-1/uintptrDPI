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
                Log("Getting the link for the latest version ZIP file...");
                string latestZipUrl = await GetLatestReleaseZipUrl();
                if (string.IsNullOrEmpty(latestZipUrl))
                {
                    Log("Latest version ZIP link not found!", true);
                    return;
                }
                Log($"ZIP link found: {latestZipUrl}");
                string zipPath = Path.Combine(Path.GetTempPath(), "GoodbyeDPI-Turkey.zip");
                Log("Downloading the ZIP file...");
                await DownloadFileAsync(latestZipUrl, zipPath);
                Log("The ZIP file has been downloaded successfully.");
                Log($"Creating target folder: {TargetFolder}");
                if (!Directory.Exists(TargetFolder))
                    Directory.CreateDirectory(TargetFolder);
                Log("Target folder is ready.");
                Log("Extracting the ZIP file...");
                ZipFile.ExtractToDirectory(zipPath, TargetFolder, true);
                Log("The ZIP file has been extracted successfully.");
                string cmdFilePath = Path.Combine(TargetFolder, CmdFileName);
                if (!File.Exists(cmdFilePath))
                {
                    Log("CMD file could not be found!", true);
                    return;
                }
                Log($"CMD file found: {cmdFilePath}");

                Log("The CMD file is being run as administrator...");
                bool cmdResult = RunAsAdmin(cmdFilePath, "Y");
                Log(cmdResult ? "Service installed successfully." : "Service installation failed!", !cmdResult);

                if (cmdResult)
                {
                    Log("Checking the service startup type...");
                    EnsureServiceStartupType("GoodByeDPI");
                }
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}", true);
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
                        throw new Exception("Unable to access the GitHub API.");

                    string json = await response.Content.ReadAsStringAsync();

                    var regex = new Regex("\"browser_download_url\":\\s*\"(https://.*?\\.zip)\"");
                    Match match = regex.Match(json);
                    if (match.Success)
                    {
                        return match.Groups[1].Value;
                    }
                    else
                    {
                        throw new Exception("The ZIP link could not be retrieved from the API.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}", true);
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
                        Log($"Service '{serviceName}' stopped. Starting...");
                        sc.Start();
                        sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                        Log($"Service '{serviceName}' started successfully.");
                    }
                    else
                    {
                        Log($"Service '{serviceName}' is running.");
                    }
                }

                using (ManagementObject service = new ManagementObject($"Win32_Service.Name='{serviceName}'"))
                {
                    service.Get();

                    string startMode = service["StartMode"]?.ToString();
                    if (startMode != "Auto")
                    {
                        Log($"Service '{serviceName}' startup type is set to '{startMode}'. Changing to 'Automatic'...");
                        service["StartMode"] = "Auto";
                        service.Put();
                        Log($"Service '{serviceName}' startup type successfully set to 'Automatic'.");
                    }
                    else
                    {
                        Log($"Service '{serviceName}' startup type is already 'Automatic'.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Error occurred while checking the startup type of service '{serviceName}': {ex.Message}", true);
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
                        Log($"CMD error: {error}", true);
                        return false;
                    }

                    Log($"CMD output: {output}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}", true);
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
                "Deleting files in the C:\\uintptrDPI directory may cause the program to not function correctly.",
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
