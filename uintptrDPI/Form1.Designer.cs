namespace uintptrDPI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutHeader = new System.Windows.Forms.TableLayoutPanel();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelLanguage = new System.Windows.Forms.TableLayoutPanel();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.comboLanguage = new System.Windows.Forms.ComboBox();
            this.tableLayoutContent = new System.Windows.Forms.TableLayoutPanel();
            this.groupActions = new System.Windows.Forms.GroupBox();
            this.tableLayoutActions = new System.Windows.Forms.TableLayoutPanel();
            this.btnInstallService = new System.Windows.Forms.Button();
            this.btnUninstallService = new System.Windows.Forms.Button();
            this.btnStartService = new System.Windows.Forms.Button();
            this.btnStopService = new System.Windows.Forms.Button();
            this.btnCheckStatus = new System.Windows.Forms.Button();
            this.groupLogs = new System.Windows.Forms.GroupBox();
            this.richTextBoxLogs = new System.Windows.Forms.RichTextBox();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.tableLayoutFooter = new System.Windows.Forms.TableLayoutPanel();
            this.lblDownloadStatus = new System.Windows.Forms.Label();
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();

            // Form
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Text = "uintptrDPI";
            this.BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            this.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // Root layout
            this.tableLayoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutRoot.ColumnCount = 1;
            this.tableLayoutRoot.RowCount = 3;
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Padding = new System.Windows.Forms.Padding(12);
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.Controls.Add(this.tableLayoutRoot);

            // Header layout
            this.tableLayoutHeader = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutHeader.ColumnCount = 3;
            this.tableLayoutHeader.RowCount = 1;
            this.tableLayoutHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutHeader.BackColor = System.Drawing.Color.White;
            this.tableLayoutHeader.Padding = new System.Windows.Forms.Padding(12);
            this.tableLayoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutRoot.Controls.Add(this.tableLayoutHeader, 0, 0);

            // Title panel
            this.panelTitle = new System.Windows.Forms.Panel();
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutHeader.Controls.Add(this.panelTitle, 0, 0);

            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(20, 24, 28);
            this.lblTitle.Location = new System.Drawing.Point(0, 2);
            this.panelTitle.Controls.Add(this.lblTitle);

            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(92, 98, 104);
            this.lblSubtitle.Location = new System.Drawing.Point(2, 40);
            this.panelTitle.Controls.Add(this.lblSubtitle);

            // Status label
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(230, 235, 242);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(80, 90, 102);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Margin = new System.Windows.Forms.Padding(10, 18, 10, 18);
            this.tableLayoutHeader.Controls.Add(this.lblStatus, 1, 0);

            // Language panel
            this.panelLanguage = new System.Windows.Forms.TableLayoutPanel();
            this.panelLanguage.ColumnCount = 1;
            this.panelLanguage.RowCount = 2;
            this.panelLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLanguage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.panelLanguage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutHeader.Controls.Add(this.panelLanguage, 2, 0);

            this.lblLanguage = new System.Windows.Forms.Label();
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLanguage.ForeColor = System.Drawing.Color.FromArgb(90, 96, 102);
            this.lblLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLanguage.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelLanguage.Controls.Add(this.lblLanguage, 0, 0);

            this.comboLanguage = new System.Windows.Forms.ComboBox();
            this.comboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLanguage.Size = new System.Drawing.Size(140, 23);
            this.comboLanguage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.comboLanguage.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.comboLanguage.SelectedIndexChanged += new System.EventHandler(this.comboLanguage_SelectedIndexChanged);
            this.panelLanguage.Controls.Add(this.comboLanguage, 0, 1);

            // Content layout
            this.tableLayoutContent = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutContent.ColumnCount = 2;
            this.tableLayoutContent.RowCount = 1;
            this.tableLayoutContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutContent.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.tableLayoutContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.Controls.Add(this.tableLayoutContent, 0, 1);

            // Actions group
            this.groupActions = new System.Windows.Forms.GroupBox();
            this.groupActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupActions.BackColor = System.Drawing.Color.White;
            this.groupActions.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.groupActions.Padding = new System.Windows.Forms.Padding(12, 22, 12, 12);
            this.tableLayoutContent.Controls.Add(this.groupActions, 0, 0);

            this.tableLayoutActions = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutActions.ColumnCount = 2;
            this.tableLayoutActions.RowCount = 3;
            this.tableLayoutActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutActions.Padding = new System.Windows.Forms.Padding(4);
            this.tableLayoutActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.tableLayoutActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.tableLayoutActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.334F));
            this.groupActions.Controls.Add(this.tableLayoutActions);

            this.btnInstallService = new System.Windows.Forms.Button();
            this.btnInstallService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInstallService.BackColor = System.Drawing.Color.FromArgb(32, 129, 233);
            this.btnInstallService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstallService.FlatAppearance.BorderSize = 0;
            this.btnInstallService.ForeColor = System.Drawing.Color.White;
            this.btnInstallService.Margin = new System.Windows.Forms.Padding(6);
            this.btnInstallService.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnInstallService.Click += new System.EventHandler(this.btnInstallService_Click);
            this.tableLayoutActions.Controls.Add(this.btnInstallService, 0, 0);

            this.btnUninstallService = new System.Windows.Forms.Button();
            this.btnUninstallService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUninstallService.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnUninstallService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUninstallService.FlatAppearance.BorderSize = 0;
            this.btnUninstallService.ForeColor = System.Drawing.Color.White;
            this.btnUninstallService.Margin = new System.Windows.Forms.Padding(6);
            this.btnUninstallService.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUninstallService.Click += new System.EventHandler(this.btnUninstallService_Click);
            this.tableLayoutActions.Controls.Add(this.btnUninstallService, 1, 0);

            this.btnStartService = new System.Windows.Forms.Button();
            this.btnStartService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartService.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnStartService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartService.FlatAppearance.BorderSize = 0;
            this.btnStartService.ForeColor = System.Drawing.Color.White;
            this.btnStartService.Margin = new System.Windows.Forms.Padding(6);
            this.btnStartService.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            this.tableLayoutActions.Controls.Add(this.btnStartService, 0, 1);

            this.btnStopService = new System.Windows.Forms.Button();
            this.btnStopService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStopService.BackColor = System.Drawing.Color.FromArgb(255, 159, 67);
            this.btnStopService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopService.FlatAppearance.BorderSize = 0;
            this.btnStopService.ForeColor = System.Drawing.Color.White;
            this.btnStopService.Margin = new System.Windows.Forms.Padding(6);
            this.btnStopService.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStopService.Click += new System.EventHandler(this.btnStopService_Click);
            this.tableLayoutActions.Controls.Add(this.btnStopService, 1, 1);

            this.btnCheckStatus = new System.Windows.Forms.Button();
            this.btnCheckStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCheckStatus.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnCheckStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckStatus.FlatAppearance.BorderSize = 0;
            this.btnCheckStatus.ForeColor = System.Drawing.Color.White;
            this.btnCheckStatus.Margin = new System.Windows.Forms.Padding(6);
            this.btnCheckStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCheckStatus.Click += new System.EventHandler(this.btnCheckStatus_Click);
            this.tableLayoutActions.Controls.Add(this.btnCheckStatus, 0, 2);
            this.tableLayoutActions.SetColumnSpan(this.btnCheckStatus, 2);

            // Logs group
            this.groupLogs = new System.Windows.Forms.GroupBox();
            this.groupLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupLogs.BackColor = System.Drawing.Color.White;
            this.groupLogs.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.groupLogs.Padding = new System.Windows.Forms.Padding(12, 22, 12, 12);
            this.tableLayoutContent.Controls.Add(this.groupLogs, 1, 0);

            this.richTextBoxLogs = new System.Windows.Forms.RichTextBox();
            this.richTextBoxLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLogs.BackColor = System.Drawing.Color.White;
            this.richTextBoxLogs.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.richTextBoxLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxLogs.ReadOnly = true;
            this.richTextBoxLogs.Font = new System.Drawing.Font("Consolas", 9F);
            this.groupLogs.Controls.Add(this.richTextBoxLogs);

            // Footer
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
            this.panelFooter.Padding = new System.Windows.Forms.Padding(6, 6, 6, 10);
            this.tableLayoutRoot.Controls.Add(this.panelFooter, 0, 2);

            this.tableLayoutFooter = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutFooter.ColumnCount = 1;
            this.tableLayoutFooter.RowCount = 2;
            this.tableLayoutFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelFooter.Controls.Add(this.tableLayoutFooter);

            this.lblDownloadStatus = new System.Windows.Forms.Label();
            this.lblDownloadStatus.AutoSize = true;
            this.lblDownloadStatus.ForeColor = System.Drawing.Color.FromArgb(90, 96, 102);
            this.tableLayoutFooter.Controls.Add(this.lblDownloadStatus, 0, 0);

            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.progressBarDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarDownload.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.tableLayoutFooter.Controls.Add(this.progressBarDownload, 0, 1);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutHeader;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TableLayoutPanel panelLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox comboLanguage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutContent;
        private System.Windows.Forms.GroupBox groupActions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutActions;
        private System.Windows.Forms.Button btnInstallService;
        private System.Windows.Forms.Button btnUninstallService;
        private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.Button btnStopService;
        private System.Windows.Forms.Button btnCheckStatus;
        private System.Windows.Forms.GroupBox groupLogs;
        private System.Windows.Forms.RichTextBox richTextBoxLogs;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutFooter;
        private System.Windows.Forms.Label lblDownloadStatus;
        private System.Windows.Forms.ProgressBar progressBarDownload;
    }
}
