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
    public partial class AdminManageOperator : Form
    {
        public AdminManageOperator()
        {
            InitializeComponent();

            this.Size = new Size(1200, 600);
           this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void AdminManageOperator_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
            string query = "SELECT OperatorID, CompanyName, Email, RegistrationStatus FROM TourOperator";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            comboBox1.Items.AddRange(new string[] { "Pending", "Approved", "Rejected" });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a row and status.");
                return;
            }

            int operatorId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OperatorID"].Value);
            string newStatus = comboBox1.SelectedItem.ToString();

            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
            string query = "UPDATE TourOperator SET RegistrationStatus = @Status WHERE OperatorID = @OperatorID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@OperatorID", operatorId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Status updated.");
            AdminManageOperator_Load(sender, e);  // Refresh gri
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row.");
                return;
            }

            string status = dataGridView1.SelectedRows[0].Cells["RegistrationStatus"].Value.ToString();

            if (status != "Rejected")
            {
                MessageBox.Show("Only rejected operators can be deleted.");
                return;
            }

            int operatorId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OperatorID"].Value);
            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
            string query = "DELETE FROM TourOperator WHERE OperatorID = @OperatorID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OperatorID", operatorId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Rejected operator deleted.");
            AdminManageOperator_Load(sender, e);  // Refresh grid
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
