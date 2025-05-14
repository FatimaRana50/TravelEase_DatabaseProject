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
    public partial class ProfileHistory : Form
    {
        private int travelerID = Global.TravelerID; // Assuming Global.TravelerID holds the current traveler's ID
        private string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";

        public ProfileHistory()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Profile_Click(object sender, EventArgs e)
        {
            LoadTravelerProfile();

        }

        private void LoadTravelerProfile()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT firstName, middleName, lastName, Email, DOB, Nationality
                    FROM Traveler
                    WHERE TravelerID = @TravelerID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TravelerID", travelerID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["firstName"].ToString();
                    textBox2.Text = reader["lastName"].ToString();
                    textBox3.Text = reader["Email"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["DOB"]);
                    textBox4.Text = reader["Nationality"].ToString();
                }
                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateTravelerProfile();
        }
        private void UpdateTravelerProfile()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE Traveler
                    SET firstName = @FirstName, lastName = @LastName, Email = @Email, 
                        DOB = @DOB, Nationality = @Nationality
                    WHERE TravelerID = @TravelerID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
                cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
                cmd.Parameters.AddWithValue("@Email", textBox3.Text);
                cmd.Parameters.AddWithValue("@DOB", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Nationality", textBox4.Text);
                cmd.Parameters.AddWithValue("@TravelerID", travelerID);

                conn.Open();
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Profile updated successfully.");
                }
                else
                {
                    MessageBox.Show("Error updating profile.");
                }
            }
        }
        private void LoadTravelerPreferences()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT Preferences
                    FROM Traveler
                    WHERE TravelerID = @TravelerID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TravelerID", travelerID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    richTextBox1.Text = reader["Preferences"].ToString();
                }
                else
                {
                    richTextBox1.Text = "No preferences set.";  // Placeholder text if no preferences exist.
                }
                reader.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateTravelerPreferences();
        }
        private void UpdateTravelerPreferences()
        {
            string preferences = richTextBox1.Text;

            if (!string.IsNullOrEmpty(preferences))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                UPDATE Traveler
                SET Preferences = @Preferences
                WHERE TravelerID = @TravelerID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TravelerID", travelerID);
                    cmd.Parameters.AddWithValue("@Preferences", preferences);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Preferences updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Error updating preferences.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter your preferences.");
            }
        }



        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 0) // Index 0 for the first tab (Profile tab)
            {
                // This will execute when the first tab is selected
                LoadTravelerProfile(); // Call method to load profile
            }

            else if (tabControl1.SelectedIndex == 1) // Index 1 for the second tab (Preferences tab)
            {
                // This will execute when the second tab is selected
                LoadTravelerPreferences(); // Call method to load preferences
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
