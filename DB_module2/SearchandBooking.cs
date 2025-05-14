//using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_module2
{
    public partial class SearchandBooking : Form
    {
         

        private int travelerID;

        public SearchandBooking(int travelerId)
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.travelerID = travelerId;
        }
        SqlConnection conn =new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");

        private void TripSearchBookingForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Category FROM Trips", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbCategory.Items.Add(reader.GetString(0));
                }
                conn.Close();
            }
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBoxMinPrice_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBoxGroupSize_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxMaxPrice_ValueChanged(object sender, EventArgs e)
        {

        }


        private void FormTripSearch_Load(object sender, EventArgs e)
        {

        }



        private void buttonSearchTrips_Click(object sender, EventArgs e)
        {
            string destination = txtDestination.Text.Trim().ToLower();
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;
            decimal minPrice = numMinPrice.Value;
            decimal maxPrice = numMaxPrice.Value;
            string category = cmbCategory.Text.Trim();
            int groupSize = (int)numGroupSize.Value;

             
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
SELECT t.TripID, t.Title, d.Name AS Destination, t.StartDate, t.EndDate,
       t.PricePerPerson, t.Category, t.maxCapacity
FROM Trips t
INNER JOIN Destinations d ON t.DestinationID = d.DestinationID
WHERE LOWER(d.Name) LIKE LOWER(@dest)
  AND t.StartDate >= @startDate
  AND t.EndDate <= @endDate
  AND t.PricePerPerson BETWEEN @minPrice AND @maxPrice
  AND t.maxCapacity >= @groupSize;

", conn);

                
                cmd.Parameters.AddWithValue("@dest", "%" + txtDestination.Text.ToLower() + "%");
                cmd.Parameters.AddWithValue("@startDate", dtpStartDate.Value.Date);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@minPrice", Convert.ToDecimal(minPrice));
                cmd.Parameters.AddWithValue("@maxPrice", Convert.ToDecimal(maxPrice));
                cmd.Parameters.AddWithValue("@groupSize", Convert.ToInt32(groupSize));
                cmd.Parameters.AddWithValue("@cat", cmbCategory.SelectedItem?.ToString() ?? "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvTrips.DataSource = dt;

                if (dt.Rows.Count == 0)
                    MessageBox.Show("No trips found with the selected filters.");
                    conn.Close();
            }

        }



        private void dataGridViewTrips_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonBookTrips_Click(object sender, EventArgs e)
        {
            if (dgvTrips.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a trip to book.");
                return;
            }

            int tripID = Convert.ToInt32(dgvTrips.SelectedRows[0].Cells["TripID"].Value);
            decimal price = Convert.ToDecimal(dgvTrips.SelectedRows[0].Cells["PricePerPerson"].Value);

            try
            {
                conn.Open();

                // Get operatorID for the trip
                SqlCommand getOpCmd = new SqlCommand("SELECT OperatorID FROM Trips WHERE TripID = @tid", conn);
                getOpCmd.Parameters.AddWithValue("@tid", tripID);
                object result = getOpCmd.ExecuteScalar();
                int opID = result != null ? Convert.ToInt32(result) : 0;

                SqlCommand cmd = new SqlCommand(@"INSERT INTO Booking
                    (TripID, TravelerID, OperatorID, BookingDate, Status, amount, PaymentStatus)
                    VALUES (@tripID, @travID, @opID, @bDate, 'Confirmed', @amount, 'Paid')", conn);

                cmd.Parameters.AddWithValue("@tripID", tripID);
                cmd.Parameters.AddWithValue("@travID", travelerID);
                cmd.Parameters.AddWithValue("@opID", opID);
                cmd.Parameters.AddWithValue("@bDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@amount", price);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Trip booked successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 👇 Open CancelBooking form after successful booking
                    CancelBooking cancelForm = new CancelBooking(travelerID);
                    cancelForm.Show();

                    this.Hide(); // optional: hide the current form
                }
                else
                {
                    MessageBox.Show("Booking failed. Try again.");
                }

            }
            //cmd.ExecuteNonQuery();
            //MessageBox.Show("Trip booked successfully!");

            //// Optionally show dashboard
            //CancelBooking  cancelBooking= new CancelBooking(travelerID);
            //cancelBooking.Show();

            //conn.Close();

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }



        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
