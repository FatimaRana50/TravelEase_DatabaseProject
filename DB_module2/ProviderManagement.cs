using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace DB_module2
{
    public partial class ProviderManagement : Form
    {
        public ProviderManagement()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ServiceIntegration serviceIntegration = new ServiceIntegration();
            serviceIntegration.ShowDialog();
            this.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ServiceListing serviceListing = new ServiceListing();
            serviceListing.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            // ProviderkiBookingMnagement providerkiBookingMnagement = new ProviderkiBookingMnagement();
            // providerkiBookingMnagement.ShowDialog();
            TrackPayment trackPayment = new TrackPayment();
            trackPayment.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            if (Global.HomeInstance != null)
            {
                Global.HomeInstance.Show();
                Global.HomeInstance.BringToFront();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProviderApniRepform providerApniRepform = new ProviderApniRepform();
            providerApniRepform.ShowDialog();
            this.Show();
        }
    }
}
