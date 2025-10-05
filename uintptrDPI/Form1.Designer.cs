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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Text = "uintptrDPI - GoodbyeDPI Installer";
            this.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // MenuStrip
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turkishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.languageToolStripMenuItem });
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.englishToolStripMenuItem, this.turkishToolStripMenuItem });
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            this.turkishToolStripMenuItem.Click += new System.EventHandler(this.turkishToolStripMenuItem_Click);
            this.MainMenuStrip = this.menuStrip1;
            this.Controls.Add(this.menuStrip1);
            
            // Service Status Label
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatus.Location = new System.Drawing.Point(20, 40);
            this.lblStatus.Size = new System.Drawing.Size(460, 23);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Controls.Add(this.lblStatus);

            // Buttons
            this.btnInstallService = new System.Windows.Forms.Button();
            this.btnStartService = new System.Windows.Forms.Button();
            this.btnStopService = new System.Windows.Forms.Button();
            this.btnUninstallService = new System.Windows.Forms.Button();
            this.btnCheckStatus = new System.Windows.Forms.Button();

            var buttons = new[] { btnInstallService, btnStartService, btnStopService, btnUninstallService, btnCheckStatus };
            int buttonWidth = 90;
            int buttonHeight = 40;
            int startX = 20;
            int spacing = 5;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Size = new System.Drawing.Size(buttonWidth, buttonHeight);
                buttons[i].Location = new System.Drawing.Point(startX + i * (buttonWidth + spacing), 80);
                buttons[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                buttons[i].FlatAppearance.BorderSize = 1;
                buttons[i].FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 80, 80);
                buttons[i].BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
                buttons[i].ForeColor = System.Drawing.Color.White;
                this.Controls.Add(buttons[i]);
            }
            
            this.btnInstallService.Click += new System.EventHandler(this.btnInstallService_Click);
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            this.btnStopService.Click += new System.EventHandler(this.btnStopService_Click);
            this.btnUninstallService.Click += new System.EventHandler(this.btnUninstallService_Click);
            this.btnCheckStatus.Click += new System.EventHandler(this.btnCheckStatus_Click);

            // Log TextBox
            this.richTextBoxLogs = new System.Windows.Forms.RichTextBox();
            this.richTextBoxLogs.Location = new System.Drawing.Point(20, 140);
            this.richTextBoxLogs.Size = new System.Drawing.Size(460, 150);
            this.richTextBoxLogs.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.richTextBoxLogs.ForeColor = System.Drawing.Color.White;
            this.richTextBoxLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxLogs.ReadOnly = true;
            this.Controls.Add(this.richTextBoxLogs);

            // ProgressBar
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.progressBarDownload.Location = new System.Drawing.Point(20, 305);
            this.progressBarDownload.Size = new System.Drawing.Size(460, 23);
            this.Controls.Add(this.progressBarDownload);
            
            // Apply initial localization
            UpdateUIResources();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turkishToolStripMenuItem;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnInstallService;
        private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.Button btnStopService;
        private System.Windows.Forms.Button btnUninstallService;
        private System.Windows.Forms.Button btnCheckStatus;
        private System.Windows.Forms.RichTextBox richTextBoxLogs;
        private System.Windows.Forms.ProgressBar progressBarDownload;
    }
}