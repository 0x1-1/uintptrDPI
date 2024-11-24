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
            listBoxLogs = new ListBox();
            btnInstallService = new Button();
            Close = new Button();
            btnCheckStatus = new Button();
            SuspendLayout();
            // 
            // listBoxLogs
            // 
            listBoxLogs.FormattingEnabled = true;
            listBoxLogs.ItemHeight = 15;
            listBoxLogs.Items.AddRange(new object[] { "Waiting input..." });
            listBoxLogs.Location = new Point(7, 8);
            listBoxLogs.Name = "listBoxLogs";
            listBoxLogs.Size = new Size(861, 184);
            listBoxLogs.TabIndex = 0;
            // 
            // btnInstallService
            // 
            btnInstallService.Location = new Point(7, 198);
            btnInstallService.Name = "btnInstallService";
            btnInstallService.Size = new Size(375, 40);
            btnInstallService.TabIndex = 1;
            btnInstallService.Text = "Install Service";
            btnInstallService.UseVisualStyleBackColor = true;
            btnInstallService.Click += btnInstallService_Click;
            // 
            // Close
            // 
            Close.Location = new Point(671, 198);
            Close.Name = "Close";
            Close.Size = new Size(197, 40);
            Close.TabIndex = 2;
            Close.Text = "Close";
            Close.UseVisualStyleBackColor = true;
            Close.Click += Close_Click;
            // 
            // btnCheckStatus
            // 
            btnCheckStatus.Location = new Point(388, 197);
            btnCheckStatus.Name = "btnCheckStatus";
            btnCheckStatus.Size = new Size(277, 41);
            btnCheckStatus.TabIndex = 3;
            btnCheckStatus.Text = "Check Status";
            btnCheckStatus.UseVisualStyleBackColor = true;
            btnCheckStatus.Click += btnCheckStatus_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(872, 246);
            Controls.Add(btnCheckStatus);
            Controls.Add(Close);
            Controls.Add(btnInstallService);
            Controls.Add(listBoxLogs);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "uintptrDPI";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxLogs;
        private Button btnInstallService;
        private Button Close;
        private Button btnCheckStatus;
    }
}
