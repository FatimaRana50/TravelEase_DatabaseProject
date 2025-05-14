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
    public partial class PaymentReportForm : Form
    {
        public PaymentReportForm()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1200, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void PaymentReportForm_Load(object sender, EventArgs e)
        {

            var adapter = new PaymentDataTableAdapters.DataTable1TableAdapter();
            DataTable dt = adapter.GetData();

            // Bind it to the report
            ReportDataSource rds = new ReportDataSource("PaymentData", dt); // Match name used in .rdlc

            reportViewer8.LocalReport.DataSources.Clear();
            reportViewer8.LocalReport.DataSources.Add(rds);


            this.reportViewer8.RefreshReport();
        }

        private void reportViewer8_Load(object sender, EventArgs e)
        {

        }
    }
}
