namespace DB_module2
{
    partial class PaymentReportForm
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
            this.reportViewer8 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewer8
            // 
            this.reportViewer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer8.LocalReport.ReportEmbeddedResource = "DB_module2.PaymentReport.rdlc";
            this.reportViewer8.Location = new System.Drawing.Point(0, 0);
            this.reportViewer8.Name = "reportViewer8";
            this.reportViewer8.ServerReport.BearerToken = null;
            this.reportViewer8.Size = new System.Drawing.Size(1178, 544);
            this.reportViewer8.TabIndex = 0;
            this.reportViewer8.Load += new System.EventHandler(this.reportViewer8_Load);
            // 
            // PaymentReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 544);
            this.Controls.Add(this.reportViewer8);
            this.Name = "PaymentReportForm";
            this.Text = "PaymentReportForm";
            this.Load += new System.EventHandler(this.PaymentReportForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer8;
    }
}