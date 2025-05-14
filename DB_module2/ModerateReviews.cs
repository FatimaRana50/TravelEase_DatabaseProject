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
    public partial class ModerateReviews : Form
    {

        private SqlConnection con = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");
        private DataTable reviewTable;
        public ModerateReviews()
        {
            InitializeComponent();
            this.Size = new Size(1100, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
          
        }

        private void ModerateReviews_Load(object sender, EventArgs e)
        {
            try
            {
                string query = @"
                    SELECT R.ReviewID, R.TripID, R.TravelerID, R.Rating, 
                           R.Comments, R.ReviewDate, AM.AdminID AS AssignedAdmin
                    FROM Review R
                    LEFT JOIN AdminModeratesReview AM ON R.ReviewID = AM.ReviewID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                reviewTable = new DataTable();
                da.Fill(reviewTable);
                dataGridView1.DataSource = reviewTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reviews: " + ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (reviewTable == null) return;

            string text = textBox1.Text.Trim().Replace("'", "''");

            reviewTable.DefaultView.RowFilter = $@"
                Convert(ReviewID, 'System.String') LIKE '%{text}%' OR
                Convert(TripID, 'System.String') LIKE '%{text}%' OR
                Convert(TravelerID, 'System.String') LIKE '%{text}%' OR
                Comments LIKE '%{text}%'";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a review to assign.");
                return;
            }

            int reviewId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ReviewID"].Value);

            try
            {
                con.Open();

                // Check if the review is already assigned
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM AdminModeratesReview WHERE ReviewID = @ReviewID", con);
                checkCmd.Parameters.AddWithValue("@ReviewID", reviewId);
                int exists = (int)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    MessageBox.Show("Already assigned.");
                    return;
                }

                // Assign the review to the current admin using Global.AdminID
                SqlCommand insertCmd = new SqlCommand("INSERT INTO AdminModeratesReview (AdminID, ReviewID) VALUES (@AdminID, @ReviewID)", con);
                insertCmd.Parameters.AddWithValue("@AdminID", Global.AdminID);
                insertCmd.Parameters.AddWithValue("@ReviewID", reviewId);
                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Assigned successfully.");
                ModerateReviews_Load(sender, e);  // Reload the reviews after assignment
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error assigning review: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a review to delete.");
                return;
            }

            int reviewId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ReviewID"].Value);

            DialogResult dr = MessageBox.Show("Delete this review?", "Confirm", MessageBoxButtons.YesNo);
            if (dr != DialogResult.Yes) return;

            try
            {
                con.Open();

                // Delete the review from the database (CASCADE will remove from AdminModeratesReview)
                SqlCommand delCmd = new SqlCommand("DELETE FROM Review WHERE ReviewID = @ReviewID", con);
                delCmd.Parameters.AddWithValue("@ReviewID", reviewId);
                delCmd.ExecuteNonQuery();

                MessageBox.Show("Deleted.");
                ModerateReviews_Load(sender, e);  // Reload the reviews after deletion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
