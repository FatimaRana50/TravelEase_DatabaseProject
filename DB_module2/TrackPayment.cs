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
    public partial class TrackPayment : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");
        public TrackPayment()
        {

            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void TrackPayment_Load(object sender, EventArgs e)
        {
           LoadServicePayments();
        }
        private void LoadServicePayments()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False"))
            {
                string query = @"
        SELECT 
            s.Name AS ServiceName,
            s.Type,
            s.Cost,
            t.Title AS TripTitle,
            b.BookingID,
            b.PaymentStatus
        FROM Services s
        JOIN TravelerServiceSelection ss ON s.ServiceID = ss.ServiceID
        JOIN Trips t ON ss.TripID = t.TripID
        JOIN Booking b ON b.TripID = ss.TripID AND b.TravelerID = ss.TravelerID
        WHERE s.ProviderID = @ProviderID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@providerID", Global.ProviderID); // Replace with actual variable

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conn.Open();
                da.Fill(dt);
                conn.Close();

                dataGridView1.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
