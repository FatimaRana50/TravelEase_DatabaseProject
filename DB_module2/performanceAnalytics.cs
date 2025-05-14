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
using System.Windows.Forms.DataVisualization.Charting;

namespace DB_module2
{
    public partial class performanceAnalytics : Form
    {
        private int _operatorId;
        public performanceAnalytics()
        {
            InitializeComponent();
            _operatorId = Global.OperatorID;
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1200, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

        }
        string connectionString="Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

        private void performanceAnalytics_Load(object sender, EventArgs e)
        {
            LoadAnalytics();
        }
        private void LoadAnalytics()
        {
            string query = @"
        SELECT 
            T.TripID,
            T.Title,
            COUNT(B.BookingID) AS TotalBookings,
            SUM(CASE WHEN B.Status = 'Confirmed' THEN 1 ELSE 0 END) AS ConfirmedBookings,
            SUM(CASE WHEN B.PaymentStatus = 'Paid' THEN B.Amount ELSE 0 END) AS TotalRevenue,
            SUM(CASE WHEN B.PaymentStatus IN ('Unpaid', 'Failed') THEN B.Amount ELSE 0 END) AS PendingAmount,
            ISNULL(AVG(CAST(R.Rating AS FLOAT)), 0) AS AverageRating,
            COUNT(R.ReviewID) AS ReviewCount
        FROM Trips T
        LEFT JOIN Booking B ON B.TripID = T.TripID AND B.OperatorID = @OperatorID
        LEFT JOIN Payment P ON P.BookingID = B.BookingID
        LEFT JOIN Review R ON R.TripID = T.TripID
        WHERE T.OperatorID = @OperatorID
        GROUP BY T.TripID, T.Title;
    ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OperatorID", _operatorId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load analytics: " + ex.Message);
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
