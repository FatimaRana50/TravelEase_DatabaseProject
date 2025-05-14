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
    public partial class touropLogin : Form
    {
        public touropLogin()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredEmail = textBox1.Text;
            string enteredPassword = textBox2.Text;

            // Database connection string
            string connectionString = @"Data Source=FATIMA\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            // SQL query to check if the email and password match an operator in the database
            string query = "SELECT OperatorID FROM TourOperator WHERE Email = @Email AND Password = @Password";

            // Create a connection to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Create the command with parameters to prevent SQL injection
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", enteredEmail);
                        cmd.Parameters.AddWithValue("@Password", enteredPassword);

                        // Execute the query and get the result
                        var result = cmd.ExecuteScalar();

                        // If result is not null, login is successful
                        if (result != null)
                        {
                            // Assign the OperatorID to the Global class
                            Global.OperatorID = Convert.ToInt32(result);

                            // Show success message
                            MessageBox.Show("You have successfully logged in!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Close the login form and open the ManageTripsForm
                            this.Close();
                            ManageTripsForm manageTripsForm = new ManageTripsForm();
                            manageTripsForm.Show();
                        }
                        else
                        {
                            // Show error message if login fails
                            MessageBox.Show("Invalid email or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox1.Clear();
                            textBox2.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors during the connection or query execution
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 signUpForm = new Form1();

            // Show the sign-up form
            signUpForm.Show();

            // Optionally, hide the current login form (if you want to hide it instead of closing it)
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            if (Global.HomeInstance != null)
            {
                Global.HomeInstance.Show();
                Global.HomeInstance.BringToFront();
            }
        }
    }
}
