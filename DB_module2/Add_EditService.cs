using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
namespace DB_module2
{
    public partial class Add_EditService : Form
    {

        private int? serviceIdToEdit = null;
        public Add_EditService(int? serviceIdToEdit)
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.serviceIdToEdit = serviceIdToEdit;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Add_EditService_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new[] { "Accommodation", "TourGuide", "Transport" });
            comboBox2.Items.AddRange(new[] { "Accepted", "Rejected","Pending" });

            if (serviceIdToEdit != null)
            {
                // Edit mode: Load service details
                label7.Text = "Edit Service";
                LoadServiceDetails(serviceIdToEdit.Value);
            }
            else
            {
                label7.Text = "Add New Service";
            }
        }
        private void LoadServiceDetails(int serviceId)
        {
            string connStr = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                SELECT s.Name, s.Type, s.Description, s.AvailabilityStatus, s.Cost
                FROM Services s
                WHERE s.ServiceID = @ServiceID AND s.ProviderID = @ProviderID", conn);

                cmd.Parameters.AddWithValue("@ServiceID", serviceId);
                cmd.Parameters.AddWithValue("@ProviderID", Global.ProviderID);


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox1.Text = reader["Name"].ToString();
                        comboBox1.Text = reader["Type"].ToString();
                        textBox2.Text = reader["Description"].ToString();
                        comboBox2.Text = reader["AvailabilityStatus"].ToString();
                        textBox3.Text = reader["Cost"].ToString();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
{
            string name = textBox1.Text;
            string type = comboBox1.Text;
            string description = textBox2.Text;
            string status = comboBox2.Text;
            decimal cost = decimal.Parse(textBox3.Text);
            if (!decimal.TryParse(textBox3.Text, out  cost))
            {
                MessageBox.Show("Please enter a valid number for the cost.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check for negative cost
            if (cost < 0)
            {
                MessageBox.Show("Price cannot be negative.", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connStr = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    if (serviceIdToEdit == null)
                    {
                        SqlCommand insertService = new SqlCommand(@"
                    INSERT INTO Services (ProviderID, Type, Name, Description, AvailabilityStatus, Cost)
                    VALUES (@ProviderID, @Type, @Name, @Description, @Status, @Cost)", conn, transaction);

                        insertService.Parameters.AddWithValue("@ProviderID", Global.ProviderID);
                        insertService.Parameters.AddWithValue("@Type", type);
                        insertService.Parameters.AddWithValue("@Name", name);
                        insertService.Parameters.AddWithValue("@Description", description);
                        insertService.Parameters.AddWithValue("@Status", status);
                        insertService.Parameters.AddWithValue("@Cost", cost);

                        insertService.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand updateService = new SqlCommand(@"
                    UPDATE Services 
                    SET Type = @Type, Name = @Name, Description = @Description, AvailabilityStatus = @Status, Cost = @Cost
                    WHERE ServiceID = @ServiceID AND ProviderID = @ProviderID", conn, transaction);

                        updateService.Parameters.AddWithValue("@ServiceID", serviceIdToEdit.Value);
                        updateService.Parameters.AddWithValue("@ProviderID", Global.ProviderID);
                        updateService.Parameters.AddWithValue("@Type", type);
                        updateService.Parameters.AddWithValue("@Name", name);
                        updateService.Parameters.AddWithValue("@Description", description);
                        updateService.Parameters.AddWithValue("@Status", status);
                        updateService.Parameters.AddWithValue("@Cost", cost);

                        updateService.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Service saved successfully.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
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
