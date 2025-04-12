namespace uintptrDPI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Text = "uintptrDPI";

            // Yeni kontroller
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.labelStatus = new System.Windows.Forms.Label();
            this.listBoxLogs = new System.Windows.Forms.ListBox();
            this.btnInstallService = new System.Windows.Forms.Button();
            this.btnCheckStatus = new System.Windows.Forms.Button();

            // ProgressBar ayarları
            this.progressBarDownload.Location = new System.Drawing.Point(12, 12);
            this.progressBarDownload.Size = new System.Drawing.Size(776, 23);
            this.progressBarDownload.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

            // Label ayarları
            this.labelStatus.Location = new System.Drawing.Point(12, 38);
            this.labelStatus.Size = new System.Drawing.Size(776, 23);
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ListBox ayarları
            this.listBoxLogs.Location = new System.Drawing.Point(12, 64);
            this.listBoxLogs.Size = new System.Drawing.Size(776, 400);
            this.listBoxLogs.HorizontalScrollbar = true;

            // Buton ayarları
            this.btnInstallService.Location = new System.Drawing.Point(12, 470);
            this.btnInstallService.Size = new System.Drawing.Size(380, 40);
            this.btnInstallService.Text = "Servisi Kur";
            this.btnInstallService.Click += new System.EventHandler(this.btnInstallService_Click);

            this.btnCheckStatus.Location = new System.Drawing.Point(408, 470);
            this.btnCheckStatus.Size = new System.Drawing.Size(380, 40);
            this.btnCheckStatus.Text = "Durumu Kontrol Et";
            this.btnCheckStatus.Click += new System.EventHandler(this.btnCheckStatus_Click);

            // Form ayarları
            this.Controls.Add(this.progressBarDownload);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.listBoxLogs);
            this.Controls.Add(this.btnInstallService);
            this.Controls.Add(this.btnCheckStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ListBox listBoxLogs;
        private System.Windows.Forms.Button btnInstallService;
        private System.Windows.Forms.Button btnCheckStatus;
    }
}
