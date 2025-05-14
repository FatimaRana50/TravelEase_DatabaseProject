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
    public partial class ServiceListing : Form
    {
        public ServiceListing()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ServiceListing_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            string query = @"
            SELECT 
            ServiceID, 
            Name, 
            Type, 
            Description, 
            AvailabilityStatus, 
            Cost 
            FROM Services
            WHERE ProviderID = @ProviderID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ProviderID", Global.ProviderID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ServiceListing_Load(this, EventArgs.Empty);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service to delete.");
                return;
            }

            int serviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ServiceID"].Value);
            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            string deleteQuery = "DELETE FROM Services WHERE ServiceID = @ServiceID AND ProviderID = @ProviderID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
            {
                cmd.Parameters.AddWithValue("@ServiceID", serviceId);
                cmd.Parameters.AddWithValue("@ProviderID", Global.ProviderID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            ServiceListing_Load(this, EventArgs.Empty);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_EditService addForm = new Add_EditService(null);
            addForm.ShowDialog();
            ServiceListing_Load(this, EventArgs.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service to delete.");
                return;
            }

            int serviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ServiceID"].Value);

            Add_EditService addForm = new Add_EditService(serviceId);
            addForm.ShowDialog();
            ServiceListing_Load(this, EventArgs.Empty);
        }
    }
}
