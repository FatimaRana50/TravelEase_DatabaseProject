namespace DB_module2
{
    partial class PlatformGrowthReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportViewer7 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewer7
            // 
            this.reportViewer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer7.LocalReport.ReportEmbeddedResource = "DB_module2.PlatformGrowthReport.rdlc";
            this.reportViewer7.Location = new System.Drawing.Point(0, 0);
            this.reportViewer7.Name = "reportViewer7";
            this.reportViewer7.ServerReport.BearerToken = null;
            this.reportViewer7.Size = new System.Drawing.Size(1178, 544);
            this.reportViewer7.TabIndex = 0;
            this.reportViewer7.Load += new System.EventHandler(this.reportViewer7_Load);
            // 
            // PlatformGrowthReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 544);
            this.Controls.Add(this.reportViewer7);
            this.Name = "PlatformGrowthReportForm";
            this.Text = "PlatformGrowthReportForm";
            this.Load += new System.EventHandler(this.PlatformGrowthReportForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer7;
    }
}