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
    public partial class PlatformGrowthReportForm : Form
    {
        public PlatformGrowthReportForm()
        {
            InitializeComponent();
            this.Size = new Size(767, 1150);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void PlatformGrowthReportForm_Load(object sender, EventArgs e)
        {

            var adapter = new PlatformData1TableAdapters.sp_GetMonthlyUserCountsTableAdapter();

            // Fill the data table
            DataTable dt = adapter.GetData();

            // Create report data source — match the dataset name in your RDLC
            ReportDataSource rds = new ReportDataSource("PlatformData1", dt);
            var adapter2 = new PlatformData2TableAdapters.sp_GetMonthlyActiveUsersTableAdapter();
            DataTable dt2 = adapter2.GetData();
            ReportDataSource rds2 = new ReportDataSource("PlatformData2", dt2); // RDLC Dataset name must match

            var adapter3 = new PlatformData3TableAdapters.sp_GetMonthlyPartnerGrowthTableAdapter();
            DataTable dt3 = adapter3.GetData();
            ReportDataSource rds3 = new ReportDataSource("PlatformData3", dt3);



            var adapter4 = new PlatformData4TableAdapters.DataTable1TableAdapter();
            DataTable dt4 = adapter4.GetData();
            ReportDataSource rds4 = new ReportDataSource("PlatformData4", dt4);
            // Clear existing data sources and add new one
            reportViewer7.LocalReport.DataSources.Clear();
            reportViewer7.LocalReport.DataSources.Add(rds);
            reportViewer7.LocalReport.DataSources.Add(rds2);
            reportViewer7.LocalReport.DataSources.Add(rds3);
            reportViewer7.LocalReport.DataSources.Add(rds4);




            // OPTIONAL: set the RDLC file path if not already bound in designer
            // reportViewer7.LocalReport.ReportEmbeddedResource = "DB_module2.YourReportName.rdlc";

            // Refresh the viewer


            this.reportViewer7.RefreshReport();
        }

        private void reportViewer7_Load(object sender, EventArgs e)
        {

        }
    }
}
