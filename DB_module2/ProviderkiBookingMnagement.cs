using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace DB_module2
{
    public partial class ProviderkiBookingMnagement : Form
    {
        string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

        public ProviderkiBookingMnagement()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            dataGridView1.CellClick += dataGridView1_CellClick;

            LoadPendingBookings();
        }

        private void LoadPendingBookings()
        {
            int temp = Global.OperatorID;
            string query = @"
        SELECT DISTINCT 
            b.BookingID,
            b.TripID,
            b.TravelerID
        FROM Booking b
        WHERE b.Status = 'Pending' AND b.OperatorID = @OperatorID
        ORDER BY b.BookingID";

            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@OperatorID", temp);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adapter.Fill(table);
                }
            }

            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Make sure a valid row index is clicked
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Check if the cell exists and is not null
                if (selectedRow.Cells["BookingID"].Value != null &&
                    int.TryParse(selectedRow.Cells["BookingID"].Value.ToString(), out int bookingID))
                {
                    LoadBookingServices(bookingID);
                }
                else
                {
                    MessageBox.Show("Invalid booking selected.");
                }
            }
        }

        private void LoadBookingServices(int bookingID)
        {
           int temp = Global.OperatorID;
  
            
            string query = @"
        SELECT 
            s.ServiceID,
            s.Name AS ServiceName,
            s.AvailabilityStatus,
            tss.approvalStatus
        FROM Booking b
        JOIN TravelerServiceSelection tss 
            ON b.TripID = tss.TripID AND b.TravelerID = tss.TravelerID
        JOIN Services s ON tss.ServiceID = s.ServiceID
        WHERE b.BookingID = @BookingID
";

            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@BookingID", bookingID);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adapter.Fill(table);
                }
            }

            dataGridView2.DataSource = table;
        }

        private void UpdateAvailability(int serviceID, string status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE tss SET tss.approvalStatus = s.AvailabilityStatus FROM TravelerServiceSelection tss JOIN Services s ON tss.ServiceID = s.ServiceID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@serviceID", serviceID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a row belonging to a booking.");
                return;
            }

            int selectedBookingID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["BookingID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = @"
            SELECT 
                tss.ServiceID,
                tss.approvalStatus,
                s.AvailabilityStatus
            FROM TravelerServiceSelection tss
            JOIN Booking b ON b.TripID = tss.TripID AND b.TravelerID = tss.TravelerID
            JOIN Services s ON s.ServiceID = tss.ServiceID
            WHERE b.BookingID = @BookingID";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@BookingID", selectedBookingID);
                SqlDataAdapter adapter = new SqlDataAdapter(checkCmd);
                DataTable services = new DataTable();
                adapter.Fill(services);

                bool atLeastOneAccepted = false;
                bool hasPending = false;
                bool allRejected = true;

                // Check if services were returned
                if (services.Rows.Count == 0)
                {
                    // If no services found, ask the operator if they want to confirm or cancel
                    DialogResult result = MessageBox.Show(
                        "No services found for this booking. Do you want to confirm the booking?",
                        "Confirm Booking",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    string newStatus = (result == DialogResult.Yes) ? "Confirmed" : "Cancelled";

                    // Update booking status based on the operator's choice
                    string updateBooking = "UPDATE Booking SET Status = @status WHERE BookingID = @bookingID";
                    SqlCommand updateBookingCmd = new SqlCommand(updateBooking, conn);
                    updateBookingCmd.Parameters.AddWithValue("@status", newStatus);
                    updateBookingCmd.Parameters.AddWithValue("@bookingID", selectedBookingID);
                    updateBookingCmd.ExecuteNonQuery();

                    MessageBox.Show($"Booking {selectedBookingID} has been {newStatus.ToUpper()}.");

                    LoadPendingBookings();
                    dataGridView2.DataSource = null;
                    return;
                }

                foreach (DataRow row in services.Rows)
                {
                    string status = row["AvailabilityStatus"].ToString();
                    if (status == "Accepted") atLeastOneAccepted = true;
                    if (status == "Pending") hasPending = true;
                   
                }

                // If there are pending services, we can't confirm yet, so ask operator to proceed
                if (hasPending)
                {
                    MessageBox.Show("All services must either be accepted or rejected by the provider before confirming the booking.");
                    return;
                }

                // Ask the operator if they want to confirm or cancel the booking
                DialogResult confirmResult = MessageBox.Show(
                    "Do you want to confirm the booking based on the current service statuses?",
                    "Confirm Booking",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                string finalStatus = (confirmResult == DialogResult.Yes) ? "Confirmed" : "Cancelled";

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    foreach (DataRow row in services.Rows)
                    {
                        int serviceID = Convert.ToInt32(row["ServiceID"]);
                        string availability = row["approvalStatus"].ToString();

                        string updateTSS = @"
                    UPDATE TravelerServiceSelection
                    SET approvalStatus = @status
                    WHERE ServiceID = @serviceID AND EXISTS (
                        SELECT 1 FROM Booking b
                        WHERE b.BookingID = @BookingID
                        AND b.TripID = TravelerServiceSelection.TripID
                        AND b.TravelerID = TravelerServiceSelection.TravelerID
                    )";

                        SqlCommand updateCmd = new SqlCommand(updateTSS, conn, transaction);
                        updateCmd.Parameters.AddWithValue("@status", availability);
                        updateCmd.Parameters.AddWithValue("@serviceID", serviceID);
                        updateCmd.Parameters.AddWithValue("@BookingID", selectedBookingID);
                        updateCmd.ExecuteNonQuery();
                    }

                    string updateBooking = "UPDATE Booking SET Status = @status WHERE BookingID = @bookingID";
                    SqlCommand updateBookingCmd = new SqlCommand(updateBooking, conn, transaction);
                    updateBookingCmd.Parameters.AddWithValue("@status", finalStatus);
                    updateBookingCmd.Parameters.AddWithValue("@bookingID", selectedBookingID);
                    updateBookingCmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show($"Booking {selectedBookingID} has been {finalStatus.ToUpper()}.");

                    if (finalStatus == "Confirmed")
                    {
                        DialogResult proceed = MessageBox.Show(
                            "Do you want to proceed to resource assignment?",
                            "Next Step",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (proceed == DialogResult.Yes)
                        {
                            this.Hide(); // Better than Close() if this form needs to be reopened later
                            ResourceCoordination assignmentForm = new ResourceCoordination();
                            assignmentForm.ShowDialog(); // Waits until closed
                            this.Show(); // Optional: show this form again after resource coordination

                        }
                    }

                    LoadPendingBookings();
                    dataGridView2.DataSource = null;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}