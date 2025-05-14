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
    public partial class Tpass : Form
    {
        private string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

        public Tpass()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Tpass_Load(object sender, EventArgs e)
        {
            LoadTravelPasses();
        }
        private void LoadTravelPasses()
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
                adapter.SelectCommand.Parameters.AddWithValue("@travelerID", Global.TravelerID);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.Columns["PassID"].HeaderText = "Pass ID";
                dataGridView1.Columns["Eticket"].HeaderText = "E-Ticket";
                dataGridView1.Columns["HotelVoucher"].HeaderText = "Hotel Voucher";
                dataGridView1.Columns["ActivityPass"].HeaderText = "Activity Pass";
                dataGridView1.Columns["IssueDate"].HeaderText = "Issue Date";
                dataGridView1.Columns["ExpiryDate"].HeaderText = "Expiry Date";

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.ReadOnly = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];

               richTextBox1.Text = row.Cells["Eticket"].Value?.ToString() ?? "N/A";
             richTextBox2.Text = row.Cells["HotelVoucher"].Value?.ToString() ?? "N/A";
                richTextBox3.Text = row.Cells["ActivityPass"].Value?.ToString() ?? "N/A";
               textBox1.Text = row.Cells["IssueDate"].Value?.ToString() ?? "N/A";
               textBox2.Text = row.Cells["ExpiryDate"].Value?.ToString() ?? "N/A";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
