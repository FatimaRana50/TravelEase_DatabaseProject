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
    public partial class TripDashboard : Form
    {
        public TripDashboard()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");

        private void TripDashboard_Load(object sender, EventArgs e)
        {
            LoadUpcomingTrips();
        }
        private void LoadUpcomingTrips()
        {
            try
            {
                conn.Open();

                string query = @"
                SELECT 
    t.TripID, 
    t.Title, 
    t.StartDate, 
    t.EndDate, 
    t.Itineraries, 
    t.CancellationPolicy,
    ISNULL(b.Status, 'Not Booked') AS BookingStatus,
    CASE 
        WHEN b.TravelerID = @TravelerID THEN 'Your Trip' 
        ELSE 'Other Trip' 
    END AS TripIndicator,
    CASE 
        WHEN b.TravelerID = @TravelerID THEN b.BookingID 
        ELSE NULL 
    END AS BookingID
FROM Trips t
LEFT JOIN Booking b ON t.TripID = b.TripID AND (b.TravelerID = @TravelerID OR b.TravelerID IS NULL)
WHERE t.StartDate > GETDATE();
 "; // Only upcoming trips

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TravelerID", Global.TravelerID); // Assuming Global.TravelerID holds the current traveler's ID

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                // Optional: Adding column headers manually if necessary
                dataGridView1.Columns["TripID"].HeaderText = "Trip ID";
                dataGridView1.Columns["Title"].HeaderText = "Trip Title";
                dataGridView1.Columns["StartDate"].HeaderText = "Start Date";
                dataGridView1.Columns["EndDate"].HeaderText = "End Date";
                dataGridView1.Columns["Itineraries"].HeaderText = "Itinerary";
                dataGridView1.Columns["CancellationPolicy"].HeaderText = "Cancellation Policy";
                dataGridView1.Columns["BookingStatus"].HeaderText = "Booking Status";
                dataGridView1.Columns["TripIndicator"].HeaderText = "Trip Type";

                // Set styles based on whether the trip belongs to the traveler
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string tripIndicatorValue = row.Cells["TripIndicator"].Value?.ToString() ?? "Other Trip";
                    if (tripIndicatorValue == "Your Trip")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No upcoming trips.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading upcoming trips: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a trip to cancel.");
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            string tripType = selectedRow.Cells["TripIndicator"].Value?.ToString();
            if (tripType != "Your Trip")
            {
                MessageBox.Show("You can only cancel your own bookings.");
                return;
            }

            object bookingIdObj = selectedRow.Cells["BookingID"].Value;
            if (bookingIdObj == DBNull.Value || bookingIdObj == null)
            {
                MessageBox.Show("Booking ID not found. Cannot cancel.");
                return;
            }

            int bookingID = Convert.ToInt32(bookingIdObj);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Booking SET Status = 'Cancelled' WHERE BookingID = @BookingID", conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingID);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Your booking has been cancelled successfully.");
                //LoadUpcomingTrips();  // Refresh the grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cancelling the trip: " + ex.Message);
            }
            finally
            {
                conn.Close();  // ✅ ensure connection is closed before calling LoadUpcomingTrips
                LoadUpcomingTrips();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
