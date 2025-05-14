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
    public partial class CreateTrip : Form
    {
        public CreateTrip()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate numeric fields
            if (!decimal.TryParse(textBox6.Text, out decimal price))
            {
                MessageBox.Show("Please enter a valid number for Price.");
                return;
            }
            if (!decimal.TryParse(textBox7.Text, out decimal sustainability))
            {
                MessageBox.Show("Please enter a valid number for Sustainability Score.");
                return;
            }
            if (!int.TryParse(textBox8.Text, out int capacity))
            {
                MessageBox.Show("Please enter a valid number for Max Capacity.");
                return;
            }

            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Please select a Destination.");
                return;
            }

            int destinationID = Convert.ToInt32(comboBox1.SelectedValue);

            int operatorID = Global.OperatorID;
            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Trips 
        (DestinationID, Inclusion, Itineraries, Title, Description, Category, StartDate, EndDate, PricePerPerson, SustainabilityScore, maxCapacity, CancellationPolicy,OperatorID)
        VALUES 
        (@DestinationID, @Inclusion, @Itineraries, @Title, @Description, @Category, @StartDate, @EndDate, @PricePerPerson, @SustainabilityScore, @maxCapacity, @CancellationPolicy,@OperatorID)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DestinationID", destinationID);
                    cmd.Parameters.AddWithValue("@Inclusion", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Itineraries", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Title", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Description", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Category", textBox5.Text);
                    cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.Date);
                    cmd.Parameters.AddWithValue("@PricePerPerson", price);
                    cmd.Parameters.AddWithValue("@SustainabilityScore", sustainability);
                    cmd.Parameters.AddWithValue("@maxCapacity", capacity);
                    cmd.Parameters.AddWithValue("@CancellationPolicy", textBox9.Text);
                    cmd.Parameters.AddWithValue("@OperatorID", operatorID);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Trip created successfully!");
        }
       

        private void CreateTrip_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT DestinationID, Name FROM Destinations", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "Name";       // Show Name
                comboBox1.ValueMember = "DestinationID"; // Actual value is DestinationID
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

            // Reset combo box
            comboBox1.SelectedIndex = -1;

            // Reset date pickers to today
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
        }

        private void button3_Click(object sender, EventArgs e)
        {
          

            // Close (or hide) CreateTripForm
            this.Close();
        }
    }
}
