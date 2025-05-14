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
    public partial class ServiceProviderReportForm : Form
    {
        public ServiceProviderReportForm()
        {
            InitializeComponent();
            this.Size = new Size(767, 1150);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;


        }

        private void ServiceProviderReportForm_Load(object sender, EventArgs e)
        {
            var adapter = new ServiceProviderTableAdapters.ServiceProviderTableAdapter();

            // Fetch data using the adapter
            DataTable dt = adapter.GetData(); // This assumes you've written the query in the TableAdapter

            // Set the data source for the ReportViewer
            ReportDataSource rds = new ReportDataSource("ServiceProvider", dt); // Match the dataset name exactly

            // Clear existing sources and add the new one
            reportViewer4.LocalReport.DataSources.Clear();
            reportViewer4.LocalReport.DataSources.Add(rds);

            // Refresh the viewer to render the report
            reportViewer4.RefreshReport();



            this.reportViewer4.RefreshReport();
        }

        private void reportViewer4_Load(object sender, EventArgs e)
        {

        }
    }
}
