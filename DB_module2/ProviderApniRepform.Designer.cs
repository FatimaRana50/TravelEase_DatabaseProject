namespace DB_module2
{
    partial class ProviderApniRepform
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
            this.reportViewer9 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewer9
            // 
            this.reportViewer9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer9.LocalReport.ReportEmbeddedResource = "DB_module2.ProviderApniReport.rdlc";
            this.reportViewer9.Location = new System.Drawing.Point(0, 0);
            this.reportViewer9.Name = "reportViewer9";
            this.reportViewer9.ServerReport.BearerToken = null;
            this.reportViewer9.Size = new System.Drawing.Size(1178, 544);
            this.reportViewer9.TabIndex = 0;
            this.reportViewer9.Load += new System.EventHandler(this.reportViewer9_Load);
            // 
            // ProviderApniRepform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 544);
            this.Controls.Add(this.reportViewer9);
            this.Name = "ProviderApniRepform";
            this.Text = "ProviderApniRepform";
            this.Load += new System.EventHandler(this.ProviderApniRepform_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer9;
    }
}