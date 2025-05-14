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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");

        private void button1_Click(object sender, EventArgs e)
        {

            string companyName = textBox1.Text;  // Assuming textBoxCompanyName is where the company name is entered
            string companyEmail = textBox2.Text;       // Assuming textBoxEmail is where the email is entered
            string password = textBox3.Text;        // Assuming textBoxPassword is where the password is entered

            if (string.IsNullOrEmpty(companyName) || string.IsNullOrEmpty(companyEmail) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";  // Your actual connection string
            string query = "INSERT INTO TourOperator (CompanyName, Email, Password, joiningDate, RegistrationStatus) " +
                           "VALUES (@CompanyName, @Email, @Password, @JoiningDate, @RegistrationStatus)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@CompanyName", companyName);
                        command.Parameters.AddWithValue("@Email", companyEmail);
                        command.Parameters.AddWithValue("@Password", password);

                        // Use DateTime.Now.Date to only get the date part (ignoring time)
                        command.Parameters.AddWithValue("@JoiningDate", DateTime.Now.Date);  // Use current date only
                        command.Parameters.AddWithValue("@RegistrationStatus", "Pending");  // Default status is 'Pending'

                        // Execute the query
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            // Query to get the OperatorID of the newly inserted operator
                            string getIdQuery = "SELECT OperatorID FROM TourOperator WHERE Email = @Email";
                            using (SqlCommand getIdCommand = new SqlCommand(getIdQuery, connection))
                            {
                                getIdCommand.Parameters.AddWithValue("@Email", companyEmail);

                                // Execute the query and retrieve the OperatorID
                                var operatorId = getIdCommand.ExecuteScalar();

                                if (operatorId != null)
                                {
                                    // Store the OperatorID in the Global class
                                    Global.OperatorID = Convert.ToInt32(operatorId);

                                    MessageBox.Show("Sign up successful!");
                                    this.Hide();  // Hide the current form
                                    ManageTripsForm manageTripsForm = new ManageTripsForm();
                                    manageTripsForm.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Unable to retrieve OperatorID. Please try again.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            touropLogin loginForm = new touropLogin();

            // Show the TourOpLogin form
            loginForm.Show();

            // Optionally, hide the current Registration Form (if you want to hide it)
            this.Hide();
        }
    }
}
