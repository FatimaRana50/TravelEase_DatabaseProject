using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace DB_module2
{
    public partial class TravSearchandBooking : Form
    {
        public TravSearchandBooking()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void TravSearchandBooking_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Category FROM Trips", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                comboBox1.Items.Clear(); // Clear existing items

                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["Category"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load activity types: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                StringBuilder query = new StringBuilder(@"
            SELECT t.TripID, t.Title, d.Name AS Destination, t.StartDate, t.EndDate,
                   t.PricePerPerson, t.Category, t.maxCapacity
            FROM Trips t
            INNER JOIN Destinations d ON t.DestinationID = d.DestinationID
            WHERE 1=1
        ");

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                // Destination
                if (checkBox1.Checked)
                {
                    query.Append(" AND LOWER(d.Name) LIKE @destination");
                    cmd.Parameters.AddWithValue("@destination", "%" + textBox1.Text.Trim().ToLower() + "%");
                }

                // Group Size
                if (checkBox2.Checked)
                {
                    query.Append(" AND t.maxCapacity >= @groupSize");
                    cmd.Parameters.AddWithValue("@groupSize", numericUpDown3.Value);
                }

                // Date Range
                if (checkBox3.Checked)
                {
                    query.Append(" AND t.StartDate >= @startDate AND t.EndDate <= @endDate");
                    cmd.Parameters.AddWithValue("@startDate", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@endDate", dateTimePicker2.Value.Date);
                }

                // Price Range
                if (checkBox4.Checked)
                {
                    query.Append(" AND t.PricePerPerson BETWEEN @minPrice AND @maxPrice");
                    cmd.Parameters.AddWithValue("@minPrice", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@maxPrice", numericUpDown2.Value);
                }

                // Activity Type
                if (checkBox5.Checked)
                {
                    query.Append(" AND t.Category = @activity");
                    cmd.Parameters.AddWithValue("@activity", comboBox1.SelectedItem?.ToString() ?? "");
                }

                cmd.CommandText = query.ToString();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No trips found with selected filters.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a trip to book.");
                return;
            }

            // Get the selected trip details from the DataGridView
            int tripID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TripID"].Value);
            string tripTitle = dataGridView1.SelectedRows[0].Cells["Title"].Value.ToString();
            decimal pricePerPerson = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["PricePerPerson"].Value);
            int maxCapacity = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["maxCapacity"].Value);

            // Assuming you have a global Traveler ID (from login or registration)
            int travelerID = Global.TravelerID; // You should set this from your login process

            if (travelerID == 0)
            {
                MessageBox.Show("You must be logged in to make a booking.");
                return;
            }
            this.Hide();
            ServiceSelectionForm serviceSelectionForm = new ServiceSelectionForm(travelerID, tripID);
            serviceSelectionForm.ShowDialog();
            this.Show();
            // Ask the user if they want to proceed with the payment
            //DialogResult paymentDialog = MessageBox.Show("Do you want to proceed with the payment?", "Payment Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

           // string paymentStatus = paymentDialog == DialogResult.Yes ? "Paid" : "Unpaid";
            //string status = paymentDialog == DialogResult.Yes ? "Confirmed" : "Pending";
            string paymentStatus = "Unpaid"; // Assuming the payment is not done yet
            string status = "Pending";
            // Insert the booking details into the Booking table
            
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Booking (TripID, TravelerID, OperatorID, BookingDate, Status, amount, PaymentStatus) VALUES (@TripID, @TravelerID, @OperatorID, @BookingDate, @Status, @Amount, @PaymentStatus)", conn);

                // Get the operator ID for the selected trip (assuming it’s in the Trips table)
                int operatorID = GetOperatorIDForTrip(tripID);

                cmd.Parameters.AddWithValue("@TripID", tripID);
                cmd.Parameters.AddWithValue("@TravelerID", travelerID);
                cmd.Parameters.AddWithValue("@OperatorID", operatorID);
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now); // Current date
                cmd.Parameters.AddWithValue("@Status", status);  // 'Confirmed' or 'Pending'
                cmd.Parameters.AddWithValue("@Amount", pricePerPerson);  // Price per person
                cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus);  // 'Paid' or 'Unpaid'

                cmd.ExecuteNonQuery();
                MessageBox.Show("Your booking request has been forwarded to the assigned opertor!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while booking the trip.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

        }
        private int GetOperatorIDForTrip(int tripID)
        {
            int operatorID = 0;

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT OperatorID FROM Trips WHERE TripID = @TripID", conn);
                cmd.Parameters.AddWithValue("@TripID", tripID);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    operatorID = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving Operator ID: " + ex.Message);
            }

            return operatorID;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int traveID = Global.TravelerID;
            this.Hide();
            BookingStatus bookingStatusForm = new BookingStatus(traveID);
            bookingStatusForm.ShowDialog();
            this.Show();
        }
    }
}
