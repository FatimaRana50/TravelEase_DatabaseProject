using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_module2
{
    public partial class Prefrences : Form
    {
        int travelerId;
        string connectionString = @"Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
        public Prefrences(int id)
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            travelerId = id;
            LoadTravelerInfo();
            LoadTravelHistory();
        }
        void LoadTravelerInfo()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Email, Preferences FROM Traveler WHERE TravelerID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", travelerId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtEmail.Text = dr["Email"].ToString();
                    rtbPreferences.Text = dr["Preferences"].ToString();
                }
            }
        }

        void LoadTravelHistory()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT T.Title, B.BookingDate, B.Status, B.Amount
                    FROM Booking B
                    JOIN Trips T ON B.TripID = T.TripID
                    WHERE B.TravelerID = @id
                    ORDER BY B.BookingDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@id", travelerId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTravelHistory.DataSource = dt;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string update = "UPDATE Traveler SET Preferences = @pref WHERE TravelerID = @id";
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.Parameters.AddWithValue("@pref", rtbPreferences.Text);
                cmd.Parameters.AddWithValue("@id", travelerId);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Preferences updated successfully.");
            }
        }

        private void Prefrences_Load(object sender, EventArgs e)
        {

        }
    }
}
