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
    public partial class ViewAssignments : Form
    {
        public ViewAssignments()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");

        private void ViewAssignments_Load(object sender, EventArgs e)
        {
            LoadAssignments();
        }
        private void LoadAssignments()
        {
            string query = @"
                SELECT 
                    t.FirstName AS TravelerName,
                    s.Name AS ServiceName,
                    s.Type AS ServiceType
                FROM 
                    ServiceProvidedTo sp
                JOIN 
                    Traveler t ON sp.TravelerID = t.TravelerID
                JOIN 
                    Services s ON sp.ServiceID = s.ServiceID
                ORDER BY 
                    TravelerName";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadAssignments();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
