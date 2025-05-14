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
    public partial class ManageTripsForm : Form
    {
        public ManageTripsForm()
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
            CreateTrip createTripForm = new CreateTrip();
            createTripForm.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            TripUpdate updateTripForm = new TripUpdate();
            updateTripForm.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            tripDelete deltrip = new tripDelete();
           deltrip.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
           viewTrip viewtrip = new viewTrip();
            viewtrip.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ResourceCoordination res = new ResourceCoordination();
            res.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
           BookingMoverview book = new BookingMoverview();
            book.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            performanceAnalytics perf = new performanceAnalytics();
            perf.ShowDialog();
            this.Show();
        }

        private void ManageTripsForm_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProviderkiBookingMnagement mng = new ProviderkiBookingMnagement();
            mng.ShowDialog();
            this.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
            if (Global.HomeInstance != null)
            {
                Global.HomeInstance.Show();
                Global.HomeInstance.BringToFront();
            }
        }
    }
}
