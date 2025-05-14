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
using System.Data.SqlClient;


namespace DB_module2
{
    public partial class TripBookingReportForm : Form
    {
        public TripBookingReportForm()
        {
            InitializeComponent();
            this.Size = new Size(767, 1150);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void TripBookingReportForm_Load(object sender, EventArgs e)
        {

            // Create an instance of the TableAdapter
            var adapter = new TripBookingReportDataSetTableAdapters.TripsTableAdapter();
            DataTable dt = adapter.GetData();
            // Fill the dataset with data from the Trips table
            var tripsData = adapter.GetData();  // This already returns TripsDataTable

            // Set the data source for the ReportViewer to the filled dataset
            // ReportDataSource rds = new ReportDataSource("TripBookingReportDataSet", tripsData);  // Use the exact name as in the report
            ReportDataSource rds = new ReportDataSource("TripBookingReportDataSet", (DataTable)dt);
            // Clear previous data sources and add the new one
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Refresh the ReportViewer to display the data
            reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
             
        }
    }
}
