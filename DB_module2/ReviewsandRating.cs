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
    public partial class ReviewsandRating : Form
    {
        private int tripId;
        private int travelerId;
        private string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
        public ReviewsandRating(int tripId, int travelerId)
        {
           
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.tripId = tripId;
            this.travelerId = travelerId;
            LoadReviews();
        }
        private void LoadReviews()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Rating, Comments, ReviewDate FROM Review WHERE TripID = @TripID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@TripID", tripId);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvReviews.DataSource = dt;
            }
        }

        private void ReviewsandRating_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void nudRating_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSubmitReview_Click(object sender, EventArgs e)
        {
            int rating = (int)nudRating.Value;
            string comments = txtComments.Text;
            DateTime reviewDate = DateTime.Now;

            if (rating < 1 || rating > 5)
            {
                MessageBox.Show("Rating must be between 1 and 5.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string insertQuery = "INSERT INTO Review (TripID, TravelerID, Rating, Comments, ReviewDate) VALUES (@TripID, @TravelerID, @Rating, @Comments, @ReviewDate)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@TripID", tripId);
                cmd.Parameters.AddWithValue("@TravelerID", travelerId);
                cmd.Parameters.AddWithValue("@Rating", rating);
                cmd.Parameters.AddWithValue("@Comments", comments);
                cmd.Parameters.AddWithValue("@ReviewDate", reviewDate);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Review submitted successfully!");
                LoadReviews();
                txtComments.Clear();
                nudRating.Value = 1;
            }
        }
    }
}
