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
    public partial class TripUpdate : Form
    {
        public TripUpdate()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a trip to update.");
                return;
            }

            // Validation for numeric fields
            if (!decimal.TryParse(textBox6.Text, out decimal price))
            {
                MessageBox.Show("Please enter a valid number for Price.");
                return;
            }
            if (!decimal.TryParse(textBox7.Text, out decimal sustainabilityScore))
            {
                MessageBox.Show("Please enter a valid number for Sustainability Score.");
                return;
            }
            if (!int.TryParse(textBox8.Text, out int maxCapacity))
            {
                MessageBox.Show("Please enter a valid number for Max Capacity.");
                return;
            }

            // Get TripID from selected item
            int tripID = (int)comboBox1.SelectedValue; // Assuming SelectedValue is bound to TripID

            // Now update the trip
            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Trips 
                         SET Title = @Title,
                             Inclusion = @Inclusion,
                             Itineraries = @Itineraries,
                             Description = @Description,
                             Category = @Category,
                             StartDate = @StartDate,
                             EndDate = @EndDate,
                             PricePerPerson = @PricePerPerson,
                             SustainabilityScore = @SustainabilityScore,
                             maxCapacity = @MaxCapacity,
                             CancellationPolicy = @CancellationPolicy
                         WHERE TripID = @TripID";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Set all parameters
                cmd.Parameters.AddWithValue("@Title", textBox1.Text);
                cmd.Parameters.AddWithValue("@Inclusion", textBox2.Text);
                cmd.Parameters.AddWithValue("@Itineraries", textBox4.Text);
                cmd.Parameters.AddWithValue("@Description", textBox3.Text);
                cmd.Parameters.AddWithValue("@Category", textBox6.Text);
                cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.Date);
                cmd.Parameters.AddWithValue("@PricePerPerson", price);
                cmd.Parameters.AddWithValue("@SustainabilityScore", sustainabilityScore);
                cmd.Parameters.AddWithValue("@maxCapacity", maxCapacity);
                cmd.Parameters.AddWithValue("@CancellationPolicy", textBox9.Text);
                cmd.Parameters.AddWithValue("@TripID", tripID);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Trip updated successfully!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the trip.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && comboBox1.SelectedValue != null && comboBox1.SelectedValue is int)
            {
                int selectedTripID = Convert.ToInt32(comboBox1.SelectedValue);

                string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Trips WHERE TripID = @TripID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TripID", selectedTripID);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Fill all TextBoxes
                        textBox1.Text = reader["Title"].ToString();                // Trip Name
                        textBox2.Text = reader["Inclusion"].ToString();            // Inclusion
                        textBox4.Text = reader["Itineraries"].ToString();          // Itinerary
                        textBox3.Text = reader["Description"].ToString();          // Description
                        textBox5.Text = reader["Category"].ToString();             // Category
                        textBox6.Text = reader["PricePerPerson"].ToString();       // Price
                        textBox7.Text = reader["SustainabilityScore"].ToString();  // Sustainability Score
                        textBox8.Text = reader["MaxCapacity"].ToString();          // Max Capacity
                        textBox9.Text = reader["CancellationPolicy"].ToString();   // Cancellation Policy
                        dateTimePicker1.Value = Convert.ToDateTime(reader["StartDate"]);
                        dateTimePicker2.Value = Convert.ToDateTime(reader["EndDate"]);

                        // ✅ Now set Destination ComboBox according to DestinationID
                        int destinationId = Convert.ToInt32(reader["DestinationID"]);
                        comboBox2.SelectedValue = destinationId;
                    }
                    reader.Close();
                }
            }
        }


        private void TripUpdate_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // ✅ First load DESTINATIONS into comboBox2
                SqlCommand cmdDest = new SqlCommand("SELECT DestinationID, Name FROM Destinations", conn);
                SqlDataAdapter adapterDest = new SqlDataAdapter(cmdDest);
                DataTable destTable = new DataTable();
                adapterDest.Fill(destTable);

                comboBox2.DataSource = destTable;
                comboBox2.DisplayMember = "Name";         // Show Destination Name
                comboBox2.ValueMember = "DestinationID";  // Store DestinationID

                // ✅ Now load TRIPS into comboBox1
                SqlCommand cmdTrips = new SqlCommand("SELECT TripID, Title FROM Trips", conn);
                SqlDataAdapter adapterTrips = new SqlDataAdapter(cmdTrips);
                DataTable tripsTable = new DataTable();
                adapterTrips.Fill(tripsTable);

                comboBox1.DataSource = tripsTable;
                comboBox1.DisplayMember = "Title";    // Show Trip Title
                comboBox1.ValueMember = "TripID";      // Store TripID
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); // Trip Name
            textBox2.Clear(); // Inclusion
            textBox4.Clear(); // Itinerary
            textBox3.Clear(); // Description
            textBox5.Clear(); // Category
            textBox6.Clear(); // Price
            textBox7.Clear(); // Sustainability Score
            textBox8.Clear(); // Max Capacity
            textBox9.Clear(); // Cancellation Policy

            // Reset DateTimePickers
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            // Reset ComboBoxes
            comboBox1.SelectedIndex = -1; // Trip selection
            comboBox2.SelectedIndex = -1; // Destination selection
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
