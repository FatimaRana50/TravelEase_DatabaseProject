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
    public partial class BookingManagement : Form
    {
        public BookingManagement()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");

        private void button1_Click(object sender, EventArgs e)
        {
            string searchQuery = textBox1.Text.Trim();  // Get the search term entered by the user
            string selectedCriterion = comboBox1.SelectedItem.ToString();  // Get the selected search criterion

            // Create base SQL query based on the selected search criterion
            string query = "SELECT b.BookingID, b.BookingDate, b.Amount, b.Status, b.PaymentStatus, t.firstName AS TravelerName FROM Booking b ";
            query += "JOIN Traveler t ON b.TravelerID = t.TravelerID WHERE ";

            // Add conditions based on the selected search criterion
            switch (selectedCriterion)
            {
                case "BookingID":
                    query += "b.BookingID = @SearchQuery";  // No wildcards for integers
                    break;
                case "TravelerName":
                    query += "t.firstName LIKE @SearchQuery";  // Use wildcards for text
                    break;
                case "Status":
                    query += "b.Status LIKE @SearchQuery";  // Use wildcards for text
                    break;
                case "PaymentStatus":
                    // Handle search for PaymentStatus (can be 'Paid', 'Unpaid', 'Refunded', or 'Failed')
                    query += "b.PaymentStatus IN ('Paid', 'Unpaid', 'Refunded', 'Failed')";

                    // Add the dynamic condition based on the search query
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " AND b.PaymentStatus LIKE @SearchQuery";  // Allow partial matching for any of these statuses
                    }
                    break;

                case "BookingDate":
                    // Ensure the query works for dates. Assuming format is 'yyyy-MM-dd'.
                    query += "b.BookingDate = @SearchQuery";
                    break;
                default:
                    // Default query if no search criterion is selected
                    query = "SELECT b.BookingID, b.BookingDate, b.Amount, b.Status, b.PaymentStatus, t.firstName AS TravelerName FROM Booking b JOIN Traveler t ON b.TravelerID = t.TravelerID";
                    break;
            }

            // Create SqlCommand to execute the query with the search term
            SqlCommand cmd = new SqlCommand(query, conn);

            // Handle parameter based on the search type
            if (selectedCriterion == "BookingID" || selectedCriterion == "BookingDate")
            {
                // For BookingID and BookingDate, pass as exact values without wildcards
                cmd.Parameters.AddWithValue("@SearchQuery", searchQuery);
            }
            else
            {
                // For text fields, add wildcards for partial matching
                cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
            }

            // Use SqlDataAdapter to fetch data
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);  // Fill the DataTable with the query results

            // Bind the filtered results to the DataGridView
            dataGridView1.DataSource = dt;
        }

        private void BookingManagement_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            // Add search criteria options to ComboBox
            comboBox1.Items.Add("BookingID");
            comboBox1.Items.Add("TravelerName");
            comboBox1.Items.Add("Status");
            comboBox1.Items.Add("PaymentStatus");
            comboBox1.Items.Add("BookingDate");

            // Optionally set a default selected item
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
