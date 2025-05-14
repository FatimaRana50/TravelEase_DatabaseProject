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
    public partial class ResourceCoordination : Form
    {
        public ResourceCoordination()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;



        }
        SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");
        private void ResourceCoordination_Load(object sender, EventArgs e)
        {
            LoadAcceptedServiceSelections();
        }



        private void LoadAcceptedServiceSelections()
        {
            string query = @"
SELECT 
    tss.TravelerID,
    tss.ServiceID,
    tss.TripID,
    t.FirstName + ' ' + t.LastName AS TravelerName,
    s.Name AS ServiceName,
    s.Type AS ServiceType,
    tss.SelectionDate
FROM TravelerServiceSelection tss
JOIN Traveler t ON t.TravelerID = tss.TravelerID
JOIN Services s ON s.ServiceID = tss.ServiceID
WHERE 
    tss.approvalStatus = 'Accepted'
    AND NOT EXISTS (
        SELECT 1 
        FROM ServiceProvidedTo sp
        WHERE sp.TravelerID = tss.TravelerID AND sp.ServiceID = tss.ServiceID
    )";

            using (SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False"))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;

                    // Set column headers
                    dataGridView1.Columns["TravelerID"].HeaderText = "Traveler ID";
                    dataGridView1.Columns["ServiceID"].HeaderText = "Service ID";
                    dataGridView1.Columns["TripID"].HeaderText = "Trip ID";
                    dataGridView1.Columns["TravelerName"].HeaderText = "Traveler Name";
                    dataGridView1.Columns["ServiceName"].HeaderText = "Service Name";
                    dataGridView1.Columns["ServiceType"].HeaderText = "Service Type";
                    dataGridView1.Columns["SelectionDate"].HeaderText = "Selection Date";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading service selections: " + ex.Message);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service assignment to confirm.");
                return;
            }

            int travelerID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TravelerID"].Value);
            int serviceID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ServiceID"].Value);

            // Optional: Check if already assigned to avoid duplicate key error
            string checkQuery = "SELECT COUNT(*) FROM ServiceProvidedTo WHERE TravelerID = @TravelerID AND ServiceID = @ServiceID";
            SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@TravelerID", travelerID);
            checkCmd.Parameters.AddWithValue("@ServiceID", serviceID);

            conn.Open();
            int exists = (int)checkCmd.ExecuteScalar();
            if (exists > 0)
            {
                MessageBox.Show("This service has already been assigned to the traveler.");
                conn.Close();
                return;
            }

            // Insert into ServiceProvidedTo
            string insertQuery = "INSERT INTO ServiceProvidedTo (TravelerID, ServiceID) VALUES (@TravelerID, @ServiceID)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
            insertCmd.Parameters.AddWithValue("@TravelerID", travelerID);
            insertCmd.Parameters.AddWithValue("@ServiceID", serviceID);

            try
            {
                insertCmd.ExecuteNonQuery();
                MessageBox.Show("Service successfully assigned!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error assigning service: " + ex.Message);
            }
            conn.Close();

            LoadAcceptedServiceSelections();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewAssignments viewForm = new ViewAssignments();
            viewForm.ShowDialog();
            this.Show();
        }
    }
}
