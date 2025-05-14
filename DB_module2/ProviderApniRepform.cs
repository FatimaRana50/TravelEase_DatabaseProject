using DB_module2.ProviderApnaDataTableAdapters;
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
    public partial class ProviderApniRepform : Form
    {
        public ProviderApniRepform()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1200, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ProviderApniRepform_Load(object sender, EventArgs e)
        {
            int providerId = Global.ProviderID; // You can make this dynamic via dropdown/parameter later

            // Create adapter and pass parameter to get data
            var adapter = new sp_GetServicePerformanceByProviderTableAdapter();
            DataTable dt = adapter.GetData(providerId); // assumes method signature is GetData(int ProviderID)

            // Set up ReportDataSource
            ReportDataSource rds = new ReportDataSource("ProviderApnaData", dt); // Match RDLC dataset name

            // Load report data
            reportViewer9.LocalReport.DataSources.Clear();
            reportViewer9.LocalReport.DataSources.Add(rds);


            this.reportViewer9.RefreshReport();
        }

        private void reportViewer9_Load(object sender, EventArgs e)
        {

        }
    }
}
