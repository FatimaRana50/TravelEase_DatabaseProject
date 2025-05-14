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
    public partial class AdminviewsCategory : Form
    {
        public AdminviewsCategory()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void AdminviewsCategory_Load(object sender, EventArgs e)
        {
            string connStr = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Load all categories
                SqlDataAdapter allAdapter = new SqlDataAdapter("SELECT * FROM Categories", conn);
                DataTable dtAll = new DataTable();
                allAdapter.Fill(dtAll);
                dataGridView1.DataSource = dtAll;

                // Load categories overseen by current admin
                SqlCommand myCmd = new SqlCommand(@"
            SELECT C.CategoryID, C.Name 
            FROM Categories C
            INNER JOIN AdminOverseesCategory AOC ON C.CategoryID = AOC.CategoryID
            WHERE AOC.AdminID = @AdminID", conn);
                myCmd.Parameters.AddWithValue("@AdminID", Global.AdminID);
                SqlDataAdapter myAdapter = new SqlDataAdapter(myCmd);
                DataTable dtMy = new DataTable();
                myAdapter.Fill(dtMy);
                dataGridView2.DataSource = dtMy;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a category to assign.");
                return;
            }

            int categoryId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CategoryID"].Value);
            string connStr = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO AdminOverseesCategory (AdminID, CategoryID) VALUES (@AdminID, @CategoryID)", conn);
                cmd.Parameters.AddWithValue("@AdminID", Global.AdminID);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Assigned successfully.");
                    AdminviewsCategory_Load(sender, e);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                        MessageBox.Show("Already overseeing this category.");
                    else
                        MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a category to remove.");
                return;
            }

            int categoryId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["CategoryID"].Value);
            string connStr = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM AdminOverseesCategory WHERE AdminID = @AdminID AND CategoryID = @CategoryID", conn);
                cmd.Parameters.AddWithValue("@AdminID", Global.AdminID);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Removed from oversight.");
                AdminviewsCategory_Load(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
