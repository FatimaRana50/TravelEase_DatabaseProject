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
    public partial class travelpass : Form
    {
        private int travelerID;
        private string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
        private System.Windows.Forms.DataGridView dgvTravelPasses;

        public travelpass(int travelerID)
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.travelerID = travelerID;

            // ✅ First initialize the DataGridView
            this.dgvTravelPasses = new System.Windows.Forms.DataGridView();
            this.dgvTravelPasses.Size = new Size(1000, 250);
            this.dgvTravelPasses.Location = new Point(50, 20);
            this.Controls.Add(this.dgvTravelPasses);

            // ✅ Then load data
            LoadTravelPasses(travelerID);




        }
        private void LoadTravelPasses(int travellerID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT tp.PassID, tp.Eticket, tp.HotelVoucher, tp.ActivityPass,
                       tp.IssueDate, tp.ExpiryDate
                FROM TravelPass tp
                INNER JOIN Booking b ON tp.BookingID = b.BookingID
                WHERE b.TravelerID = @travelerID AND b.Status = 'Confirmed'";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@travelerID", travelerID);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvTravelPasses.DataSource = dt;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTravelPasses.SelectedRows.Count > 0)
            {
                var row = dgvTravelPasses.SelectedRows[0];
                rtbEticket.Text = row.Cells["Eticket"].Value?.ToString() ?? "N/A";
                rtbVoucher.Text = row.Cells["HotelVoucher"].Value?.ToString() ?? "N/A";
                rtbActivity.Text = row.Cells["ActivityPass"].Value?.ToString() ?? "N/A";
                txtIssueDate.Text = row.Cells["IssueDate"].Value?.ToString() ?? "N/A";
                txtExpiryDate.Text = row.Cells["ExpiryDate"].Value?.ToString() ?? "N/A";
            }
        }


        private void rtbActvity_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rtbETicket_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIssueDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void textExpiryDate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
