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
    public partial class DestinationReportForm : Form
    {
        public DestinationReportForm()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void DestinationReportForm_Load(object sender, EventArgs e)
        {
            var adapter = new DestinationData1TableAdapters.DestinationsTableAdapter();

            // Fetch data using the adapter
            DataTable dt = adapter.GetData();  // This will return your DestinationDataTable with destination-related data

            // Set the data source for the ReportViewer to the filled DataTable
            ReportDataSource rds = new ReportDataSource("DestinationData1", dt); // Name must match RDLC dataset name

          

            var datase2=new DestinationData2();
            datase2.EnforceConstraints = false;
            var adapter3 = new DestinationData2TableAdapters.DestinationsTableAdapter();
            adapter3.Fill(datase2.Destinations); // Fill the strongly typed table in the dataset
            DataTable dt2 = datase2.Destinations;
            ReportDataSource rds2 = new ReportDataSource("DestinationData2", dt2); // Name must match RDLC dataset name



            // Clear existing data sources and add the new one
            reportViewer5.LocalReport.DataSources.Clear();
            reportViewer5.LocalReport.DataSources.Add(rds);
            reportViewer5.LocalReport.DataSources.Add(rds2);

            // Set the RDLC report path if needed
            // reportViewer5.LocalReport.ReportEmbeddedResource = "DB_module2.YourReportName.rdlc";  // Adjust this if necessary

            // Refresh the ReportViewer to render the report
            reportViewer5.RefreshReport();

        }

        private void reportViewer5_Load(object sender, EventArgs e)
        {

        }
    }
}
