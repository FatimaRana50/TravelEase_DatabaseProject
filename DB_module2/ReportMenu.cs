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
    public partial class ReportMenu : Form
    {
        public ReportMenu()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1200, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ReportMenu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TripBookingReportForm tripBookingReportForm = new TripBookingReportForm();
            tripBookingReportForm.ShowDialog();
            this.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            TravelerReportForm travelerReportForm = new TravelerReportForm();
            travelerReportForm.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            OperatorReportForm operatorReportForm = new OperatorReportForm();
            operatorReportForm.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ServiceProviderReportForm serviceProviderReportForm = new ServiceProviderReportForm();
            serviceProviderReportForm.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            DestinationReportForm destinationReportForm = new DestinationReportForm();
            destinationReportForm.ShowDialog();
            this.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            AbandonmentReportForm abandonmentReportForm = new AbandonmentReportForm();
            abandonmentReportForm.ShowDialog();
            this.Show();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlatformGrowthReportForm platformGrowthReportForm = new PlatformGrowthReportForm();
            platformGrowthReportForm.ShowDialog();
            this.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            PaymentReportForm paymentReportForm = new PaymentReportForm();
            paymentReportForm.ShowDialog();
            this.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
