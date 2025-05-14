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
    public partial class AdminmanageUser : Form
    {
        public AdminmanageUser()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void AdminmanageUser_Load(object sender, EventArgs e)
        {
            string connStr = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TravelerID, FirstName FROM Traveler";
                SqlCommand cmd = new SqlCommand(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "FirstName";
                    comboBox1.ValueMember = "TravelerID";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading travelers: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int travelerId = (int)comboBox1.SelectedValue;
            string reason = textBox1.Text.Trim();
            DateTime actionDate = DateTime.Now;
            int adminId = Global.AdminID; // Replace this with your actual logic

            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Please provide a reason.");
                return;
            }

            string connStr = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
            string insertQuery = "INSERT INTO AdminTravelerAction (AdminID, TravelerID, ActionType, ActionDate, Reason) VALUES (@AdminID, @TravelerID, 'Blocked', @Date, @Reason)";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@AdminID", adminId);
                cmd.Parameters.AddWithValue("@TravelerID", travelerId);
                cmd.Parameters.AddWithValue("@Date", actionDate);
                cmd.Parameters.AddWithValue("@Reason", reason);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Traveler blocked successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int travelerId = (int)comboBox1.SelectedValue;
            string reason = textBox1.Text.Trim();
            DateTime actionDate = DateTime.Now;
            int adminId =Global.AdminID; // Replace this with your actual logic

            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Please provide a reason.");
                return;
            }

            string connStr = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlTransaction txn = conn.BeginTransaction();
                try
                {
                    // Log the action
                    string insertQuery = "INSERT INTO AdminTravelerAction (AdminID, TravelerID, ActionType, ActionDate, Reason) VALUES (@AdminID, @TravelerID, 'Removed', @Date, @Reason)";
                    SqlCommand logCmd = new SqlCommand(insertQuery, conn, txn);
                    logCmd.Parameters.AddWithValue("@AdminID", adminId);
                    logCmd.Parameters.AddWithValue("@TravelerID", travelerId);
                    logCmd.Parameters.AddWithValue("@Date", actionDate);
                    logCmd.Parameters.AddWithValue("@Reason", reason);
                    logCmd.ExecuteNonQuery();

                    // Delete traveler
                    string deleteQuery = "DELETE FROM Traveler WHERE TravelerID = @TravelerID";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, txn);
                    deleteCmd.Parameters.AddWithValue("@TravelerID", travelerId);
                    deleteCmd.ExecuteNonQuery();

                    txn.Commit();
                    MessageBox.Show("Traveler removed from the system.");
                }
                catch (Exception ex)
                {
                    txn.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
