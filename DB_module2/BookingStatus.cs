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
    public partial class BookingStatus : Form
    {
        int travelerID;
        SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");
        public BookingStatus(int travelerID)
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.travelerID = travelerID;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
           
        }
        private void LoadConfirmedBookings()
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT b.BookingID, t.Title, b.BookingDate, b.amount, b.PaymentStatus
                    FROM Booking b
                    JOIN Trips t ON b.TripID = t.TripID
                    WHERE b.TravelerID = @travelerID AND b.Status = 'Confirmed'", conn);
                da.SelectCommand.Parameters.AddWithValue("@travelerID", travelerID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            finally
            {
                conn.Close();
            }
        }

        private void LoadPendingBookings()
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT b.BookingID, t.Title, b.BookingDate, b.amount, b.PaymentStatus
                    FROM Booking b
                    JOIN Trips t ON b.TripID = t.TripID
                    WHERE b.TravelerID = @travelerID AND b.Status = 'Pending'", conn);
                da.SelectCommand.Parameters.AddWithValue("@travelerID", travelerID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a confirmed booking to pay for.");
                return;
            }

            int bookingID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["BookingID"].Value);
            decimal tripAmount = 0;
            decimal servicesAmount = 0;
            int operatorID = 0;

            try
            {
                conn.Open();

                // Get base trip cost and OperatorID
                SqlCommand cmdTrip = new SqlCommand(@"
            SELECT b.Amount, t.OperatorID
            FROM Booking b
            JOIN Trips t ON b.TripID = t.TripID
            WHERE b.BookingID = @bookingID", conn);
                cmdTrip.Parameters.AddWithValue("@bookingID", bookingID);
                SqlDataReader reader = cmdTrip.ExecuteReader();
                if (reader.Read())
                {
                    tripAmount = reader.GetDecimal(0);
                    operatorID = reader.GetInt32(1);
                }
                reader.Close();

                // Get total cost of all services in SelectedServices
                SqlCommand cmdServices = new SqlCommand(@"
    SELECT ISNULL(SUM(s.Cost), 0)
    FROM Booking b
    JOIN TravelerServiceSelection ss 
        ON b.TripID = ss.TripID AND b.TravelerID = ss.TravelerID
    JOIN Services s ON ss.ServiceID = s.ServiceID
    WHERE b.BookingID = @bookingID", conn);
                cmdServices.Parameters.AddWithValue("@bookingID", bookingID);
                servicesAmount = Convert.ToDecimal(cmdServices.ExecuteScalar());

                decimal totalAmount = tripAmount + servicesAmount;

                DialogResult confirm = MessageBox.Show(
                    $"Trip Cost: {tripAmount:C}\nService Cost: {servicesAmount:C}\n\nTotal: {totalAmount:C}\n\nProceed with payment?",
                    "Confirm Payment",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                {
                    return;
                }

                // Update Booking payment status
                SqlCommand cmdUpdate = new SqlCommand(
                    "UPDATE Booking SET PaymentStatus = 'Paid' WHERE BookingID = @bookingID", conn);
                cmdUpdate.Parameters.AddWithValue("@bookingID", bookingID);
                cmdUpdate.ExecuteNonQuery();

                // Insert into Payment table
                SqlCommand cmdInsert = new SqlCommand(@"
            INSERT INTO Payment 
                (BookingID, OperatorID, PaymentDate, PaymentMethod, totalAmount, TransactionStatus, isDisputed, DisputeStatus)
            VALUES
                (@bookingID, @operatorID, @date, @method, @amount, 'Successful', 0, 'Resolved')", conn);

                cmdInsert.Parameters.AddWithValue("@bookingID", bookingID);
                cmdInsert.Parameters.AddWithValue("@operatorID", operatorID);
                cmdInsert.Parameters.AddWithValue("@date", DateTime.Now.Date);
                cmdInsert.Parameters.AddWithValue("@method", "Cash");
                cmdInsert.Parameters.AddWithValue("@amount", totalAmount);

                cmdInsert.ExecuteNonQuery();

                //MessageBox.Show("Payment successful.");
                SqlCommand cmdPass = new SqlCommand(@"
    INSERT INTO TravelPass (BookingID, Eticket, HotelVoucher, ActivityPass, IssueDate, ExpiryDate)
    VALUES (@bookingID, @eticket, @voucher, @activity, @issued, @expiry)", conn);

                cmdPass.Parameters.AddWithValue("@bookingID", bookingID);
                cmdPass.Parameters.AddWithValue("@eticket", $"E-Ticket #{bookingID}-T{DateTime.Now.Ticks % 100000}");
                cmdPass.Parameters.AddWithValue("@voucher", $"Hotel Voucher for Booking #{bookingID}");
                cmdPass.Parameters.AddWithValue("@activity", $"Activity Pass for Booking #{bookingID}");
                cmdPass.Parameters.AddWithValue("@issued", DateTime.Now.Date);
                cmdPass.Parameters.AddWithValue("@expiry", DateTime.Now.Date.AddMonths(1));

                cmdPass.ExecuteNonQuery();

                MessageBox.Show("Payment successful.\nTravel Pass has been issued.");



            }
            catch (Exception ex)
            {
                MessageBox.Show("Payment error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            LoadConfirmedBookings(); // Refresh grid
        }


        private void BookingStatus_Load(object sender, EventArgs e)
        {
            LoadConfirmedBookings();
            LoadPendingBookings();
        }
    }
    
}
