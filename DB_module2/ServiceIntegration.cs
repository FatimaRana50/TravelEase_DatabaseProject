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
    public partial class ServiceIntegration : Form
    {
        public ServiceIntegration()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;

        }

        private void ServiceIntegration_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
            string query = @"
            SELECT ServiceID, Name AS ServiceName, Type, Description, Cost, Availabilitystatus
            FROM Services
            WHERE ProviderID = @ProviderID;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ProviderID", Global.ProviderID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }

            // Add Accept/Reject button columns if not already added
            if (!dataGridView1.Columns.Contains("Accept"))
            {
                DataGridViewButtonColumn acceptBtn = new DataGridViewButtonColumn();
                acceptBtn.Name = "Accept";
                acceptBtn.Text = "Accept";
                acceptBtn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(acceptBtn);
            }

            if (!dataGridView1.Columns.Contains("Reject"))
            {
                DataGridViewButtonColumn rejectBtn = new DataGridViewButtonColumn();
                rejectBtn.Name = "Reject";
                rejectBtn.Text = "Reject";
                rejectBtn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(rejectBtn);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            int serviceId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ServiceID"].Value);

            if (columnName == "Accept" || columnName == "Reject")
            {
                string newStatus = columnName == "Accept" ? "Accepted" : "Rejected";

                string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
                string updateQuery = @"
            UPDATE Services
            SET Availabilitystatus = @Status
            WHERE ServiceID = @ServiceID AND ProviderID = @ProviderID";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@ProviderID", Global.ProviderID);
                    cmd.Parameters.AddWithValue("@ServiceID", serviceId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Refresh the grid
                ServiceIntegration_Load(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
