using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_module2
{
    public partial class AbandonmentReportForm : Form
    {
        public AbandonmentReportForm()
        {
            InitializeComponent();
            this.Size = new Size(767, 1150);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void AbandonmentReportForm_Load(object sender, EventArgs e)
        {

            var adapter = new AbandonTableAdapters.AbandonedBookingTableAdapter();

            // Fill the data table
            DataTable dt = adapter.GetData();

            // Create report data source — must match RDLC dataset name
            ReportDataSource rds = new ReportDataSource("Abandon", dt);
            var adapter2 = new Abandon2TableAdapters.DataTable1TableAdapter();
            DataTable dt2 = adapter2.GetData();
            ReportDataSource rds2 = new ReportDataSource("Abandon2", dt2);

            // Clear and add data sources
            reportViewer6.LocalReport.DataSources.Clear();
            reportViewer6.LocalReport.DataSources.Add(rds);
            reportViewer6.LocalReport.DataSources.Add(rds2);

            this.reportViewer6.RefreshReport();
        }

        private void reportViewer6_Load(object sender, EventArgs e)
        {

        }
    }
}
