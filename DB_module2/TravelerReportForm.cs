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
    public partial class TravelerReportForm : Form
    {
        public TravelerReportForm()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void TravelerReportForm_Load(object sender, EventArgs e)
        {

            // Create the dataset and disable constraints
            var dataset = new TravelerPreferencesDataSe();
            dataset.EnforceConstraints = false;

            var adapter = new TravelerPreferencesDataSeTableAdapters.TravTableTableAdapter();
            adapter.Fill(dataset.TravTable); // Fill the strongly typed table in the dataset

            // Now cast that typed table to a DataTable
            DataTable dt = dataset.TravTable;

            // Use your same report setup
            ReportDataSource rds = new ReportDataSource("TravelerPreferencesDataSe", dt);
            reportViewer2.LocalReport.DataSources.Clear();
            reportViewer2.LocalReport.DataSources.Add(rds);

            reportViewer2.RefreshReport();
        }

        private void reportViewer2_Load(object sender, EventArgs e)
        {

        }
    }
}
