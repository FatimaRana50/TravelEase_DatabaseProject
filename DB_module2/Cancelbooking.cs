using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_module2
{
    public partial class CancelBooking : Form
    {
        int travelerID=-1;
        public CancelBooking(int travelerID)
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.travelerID = travelerID;
        }
        SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");


        private void Form5_Load(object sender, EventArgs e)
        {
            LoadBookings(travelerID);
        }

        private void LoadBookings(int travelerID)

        {

            try
            {
                conn.Open();
                string query = @"Select 
                b.BookingID,
                t.TripID,
                b.TravelerID,
                t.Title,
                d.Name AS Destination,
                t.StartDate,
                t.EndDate,
                t.PricePerPerson,
                t.Itineraries,
                t.CancellationPolicy,
                b.Status AS BookingStatus,
                b.PaymentStatus,
                b.Amount
            FROM Booking b
            INNER JOIN Trips t ON b.TripID = t.TripID
            INNER JOIN Destinations d ON t.TripID = d.DestinationID
            WHERE 
                b.TravelerID = @TravelerID AND
                
                t.StartDate >= CAST(GETDATE() AS DATE)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@travelerID", travelerID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvUpcomingTrips.AutoGenerateColumns = true;
                dgvUpcomingTrips.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data returned from the query.");
                }
                else
                {
                    dgvUpcomingTrips.DataSource = dt;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trips: " + ex.Message);
            }
            finally
            {
                conn.Close() ;
            }
        }


        private void dgvUpcomingTrips_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void rtbItinerary_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            if (dgvUpcomingTrips.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking to cancel.");
                return;
            }

            int bookingId = Convert.ToInt32(dgvUpcomingTrips.SelectedRows[0].Cells["BookingID"].Value);

            try
            {
                conn.Open();
                string query = "DELETE FROM Booking WHERE BookingID = @BookingID AND TravelerID = @TravelerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingId);
                cmd.Parameters.AddWithValue("@TravelerID", travelerID);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Booking cancelled successfully.");
                    LoadBookings(travelerID); // refresh
                }
                else
                {
                    MessageBox.Show("Booking cancellation failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cancelling booking: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void CancelBookingById(int bookingID)
        {
            string query = "DELETE FROM Booking WHERE BookingID = @BookingID";
            
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingID);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Booking cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookings(travelerID); // Refresh the grid
                    }
                    else
                    {
                        MessageBox.Show("Booking ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error cancelling booking: " + ex.Message);
                }
                finally
                {

                    conn.Close();
                }
            }


        }

        
    }
}

