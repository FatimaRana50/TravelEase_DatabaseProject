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
    public partial class SendReminder : Form
    {
        public SendReminder()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void SendReminder_Load(object sender, EventArgs e)
        {
            LoadTravelers();
        }
        private void LoadTravelers()
        {
            string connStr = @"Data Source=FATIMA\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
    SELECT DISTINCT 
        t.TravelerID, 
        t.FirstName + ' ' + t.LastName AS TravelerName, 
        b.Status AS BookingStatus,
        b.PaymentStatus
    FROM Booking b
    JOIN Traveler t ON b.TravelerID = t.TravelerID
    
";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OperatorID", Global.OperatorID);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)

        {
            string connStr = @"Data Source=FATIMA\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a traveler from the list.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a reminder message.");
                return;
            }

            int travelerID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TravelerID"].Value);
            string reminderText = textBox1.Text;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "INSERT INTO SendReminder (OperatorID, TravelerID, Reminder) VALUES (@OperatorID, @TravelerID, @Reminder)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@OperatorID", Global.OperatorID);
                cmd.Parameters.AddWithValue("@TravelerID", travelerID);
                cmd.Parameters.AddWithValue("@Reminder", reminderText);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Reminder sent successfully!");
                    textBox1.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
