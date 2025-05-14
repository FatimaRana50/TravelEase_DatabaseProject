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
    public partial class TravelerReview : Form
    {
        private int travelerID = Global.TravelerID; // Fetching TravelerID from Global context
        private string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
        public TravelerReview()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadReviewableTrips();
        }
        private void LoadReviewableTrips()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT DISTINCT t.TripID, t.Title, t.EndDate
                    FROM Trips t
                    INNER JOIN Booking b ON t.TripID = b.TripID
                    WHERE b.TravelerID = @TravelerID
                      AND b.Status = 'Confirmed'
                      AND t.EndDate < GETDATE()
                      AND NOT EXISTS (
                          SELECT 1 FROM Review r
                          WHERE r.TripID = t.TripID AND r.TravelerID = @TravelerID
                      )";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@TravelerID", travelerID);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

               dataGridView1.DataSource = dt;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a trip to review.");
                return;
            }

            int tripID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TripID"].Value);
            int rating = (int)numericUpDown1.Value;
            string comments = richTextBox1.Text;
            DateTime reviewDate = DateTime.Now;

            // Insert the review into the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Review (TripID, TravelerID, Rating, Comments, ReviewDate)
                    VALUES (@TripID, @TravelerID, @Rating, @Comments, @ReviewDate)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TripID", tripID);
                cmd.Parameters.AddWithValue("@TravelerID", travelerID);
                cmd.Parameters.AddWithValue("@Rating", rating);
                cmd.Parameters.AddWithValue("@Comments", comments);
                cmd.Parameters.AddWithValue("@ReviewDate", reviewDate);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Review submitted successfully.");
                    LoadReviewableTrips();  // Refresh the grid after submitting
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error submitting the review: " + ex.Message);
                }
            }
        }

        private void TravelerReview_Load(object sender, EventArgs e)
        {
            LoadReviewableTrips();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                int tripID = Convert.ToInt32(selectedRow.Cells["TripID"].Value);
                string tripTitle = selectedRow.Cells["Title"].Value?.ToString();
                DateTime tripEndDate = Convert.ToDateTime(selectedRow.Cells["EndDate"].Value);

                label4.Text = $"Selected Trip: {tripTitle} (Ends: {tripEndDate.ToShortDateString()})";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ensure click is not on header
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];
                int tripID = Convert.ToInt32(selectedRow.Cells["TripID"].Value);
                string tripTitle = selectedRow.Cells["Title"].Value?.ToString();
                DateTime tripEndDate = Convert.ToDateTime(selectedRow.Cells["EndDate"].Value);

                label4.Text = $"Selected Trip: {tripTitle} (Ends: {tripEndDate.ToShortDateString()})";
            }
        }
    }
}
