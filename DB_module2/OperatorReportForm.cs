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
    public partial class OperatorReportForm : Form
    {
        public OperatorReportForm()
        {
            InitializeComponent();
            this.Size = new Size(767, 1150);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void OperatorReportForm_Load(object sender, EventArgs e)
        {



            var adapter = new OperatorPerfomanceDataTableAdapters.TourOperatorTableAdapter();

            // Fetch data using the adapter
            DataTable dt = adapter.GetData();  // This returns your DataTable with operator performance

            // Set the data source for the ReportViewer to the filled DataTable
            ReportDataSource rds = new ReportDataSource("OperatorPerformanceData", (DataTable)dt); // Name must match RDLC dataset

            // Clear existing sources and add the new one
            reportViewer3.LocalReport.DataSources.Clear();
            reportViewer3.LocalReport.DataSources.Add(rds);

            // Set the RDLC report path
            //reportViewer3.LocalReport.ReportEmbeddedResource = "DB_module2.OperatorPerformanceReport.rdlc"; // adjust path if needed

            // Refresh the viewer to render the report
            //reportViewer3.LocalReport.ReportPath = "OperatorReport.rdlc";
            reportViewer3.RefreshReport();

        }

        private void reportViewer3_Load(object sender, EventArgs e)
        {

        }

        private void reportViewer3_Load_1(object sender, EventArgs e)
        {

        }
    }
}
