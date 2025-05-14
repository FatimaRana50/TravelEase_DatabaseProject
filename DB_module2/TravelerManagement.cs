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
    public partial class TravelerManagement : Form
    {
        public TravelerManagement()
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
            TravSearchandBooking travSearchandBooking = new TravSearchandBooking();
            travSearchandBooking.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            TripDashboard tripDashboard = new TripDashboard();
            tripDashboard.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Tpass travelpass = new Tpass();
            travelpass.ShowDialog(); 
          
            this.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            TravelerReview travelerReview = new TravelerReview();
            travelerReview.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProfileHistory profileHistory = new ProfileHistory();
            profileHistory.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Wishlist wishlist = new Wishlist();

            wishlist.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
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
