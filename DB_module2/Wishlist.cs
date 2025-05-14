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
    public partial class Wishlist : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");

        public Wishlist()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Wishlist_Load(object sender, EventArgs e)
        {
            LoadAllTrips();
            LoadWishlist();
        }
        private void LoadAllTrips()
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT t.TripID, t.Title, d.Name AS Destination, t.StartDate, t.Priceperperson
                    FROM Trips t
                    JOIN Destinations d ON t.DestinationID = d.DestinationID", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            finally { conn.Close(); }
        }
        private void LoadWishlist()
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT w.TripID, t.Title, d.Name AS Destination, t.StartDate, t.Priceperperson
                    FROM Wishlist w
                    JOIN Trips t ON w.TripID = t.TripID
                    JOIN Destinations d ON t.DestinationID = d.DestinationID
                    WHERE w.TravelerID = @travelerID", conn);

                da.SelectCommand.Parameters.AddWithValue("@travelerID", Global.TravelerID);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            finally { conn.Close(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a trip to add.");
                return;
            }

            int tripID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TripID"].Value);

            try
            {
                conn.Open();

                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Wishlist WHERE TravelerID = @travelerID AND TripID = @tripID", conn);
                checkCmd.Parameters.AddWithValue("@travelerID", Global.TravelerID);
                checkCmd.Parameters.AddWithValue("@tripID", tripID);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Trip already in wishlist.");
                    return;
                }

                SqlCommand insertCmd = new SqlCommand("INSERT INTO Wishlist (TravelerID, TripID) VALUES (@travelerID, @tripID)", conn);
                insertCmd.Parameters.AddWithValue("@travelerID", Global.TravelerID);
                insertCmd.Parameters.AddWithValue("@tripID", tripID);
                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Added to wishlist.");
               
            }
            finally { conn.Close(); }
            LoadWishlist();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a wishlist trip to remove.");
                return;
            }

            int tripID = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["TripID"].Value);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Wishlist WHERE TravelerID = @travelerID AND TripID = @tripID", conn);
                cmd.Parameters.AddWithValue("@travelerID", Global.TravelerID);
                cmd.Parameters.AddWithValue("@tripID", tripID);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Removed from wishlist.");
               
            }
            finally { conn.Close(); }
            LoadWishlist();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
