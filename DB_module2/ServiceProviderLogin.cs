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
    public partial class ServiceProviderLogin : Form
    {
        public ServiceProviderLogin()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // Required field validation
            if (email.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("Email and password are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Database connection string
            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            // Select query to check email and password
            string query = "SELECT ProviderID FROM ServiceProvider WHERE Email = @email AND Password = @password";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", password);

                        // Execute the query and get the ProviderID
                        var result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            // Login successful
                            Global.ProviderID = Convert.ToInt32(result);  // Save ProviderID in the Global variable
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                           ProviderManagement providerManagement = new ProviderManagement();
                            providerManagement.Show();

                            // Proceed with redirection to the next form or functionality
                            // Example: this.Hide(); new MainForm().Show();
                        }
                        else
                        {
                            // Invalid login credentials
                            MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox1.Clear();
                            textBox2.Clear();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Service_Provider_signup signupForm = new Service_Provider_signup();
            signupForm.Show();

        }
    }
}
