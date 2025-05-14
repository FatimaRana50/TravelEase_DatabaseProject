using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Data.SqlClient;
namespace DB_module2
{
    public partial class ProcessRefunds : Form
    {
        public ProcessRefunds()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ProcessRefunds_Load(object sender, EventArgs e)
        {

            comboBox1.Visible = false;
            button2.Visible = false;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Refunded");
            SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");
            conn.Open();

            string query = @"SELECT BookingID, BookingDate, Amount,status, PaymentStatus 
                     FROM Booking 
                     WHERE Status = 'Cancelled' AND PaymentStatus = 'Paid'";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Add Checkbox column
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "Select";
            checkBoxColumn.Width = 50;
            checkBoxColumn.Name = "Select";
            dataGridView1.Columns.Add(checkBoxColumn);

            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = true;        // Show dropdown
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string newStatus = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(newStatus))
            {
                MessageBox.Show("Please select a status first.");
                return;
            }

            SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");

            conn.Open();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                if (isSelected)
                {
                    int bookingId = Convert.ToInt32(row.Cells["BookingID"].Value);

                    string updateQuery = "UPDATE Booking SET PaymentStatus = @PaymentStatus WHERE BookingID = @BookingID";

                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@PaymentStatus", newStatus);
                    cmd.Parameters.AddWithValue("@BookingID", bookingId);

                    cmd.ExecuteNonQuery();
                }
            }

            conn.Close();

            MessageBox.Show("Selected bookings updated successfully!");

            // Reload the DataGridView
            ProcessRefunds_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
