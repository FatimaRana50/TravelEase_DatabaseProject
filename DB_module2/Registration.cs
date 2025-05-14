using DB_module2;
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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        SqlConnection con = new SqlConnection("Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False");

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

 

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

        // Check if Password and Confirm Password match
        if (txtPassword.Text != txtConfirmPassword.Text)
        {
            MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        
        try
        {
            con.Open();
                string query = "INSERT INTO Traveler (firstName, middleName, lastName, Email, regDate, Password, DOB, Nationality, Preferences) " +
                     "VALUES (@firstName, @middleName, @lastName, @Email, @regDate, @Password, @DOB, @Nationality, @Preferences); " +
                     "SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@middleName", txtMiddleName.Text);
            cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@regDate", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd.Parameters.AddWithValue("@DOB", dtpDOB.Value.Date);
            cmd.Parameters.AddWithValue("@Nationality", txtNationality.Text);
            cmd.Parameters.AddWithValue("@Preferences", txtPreferences.Text);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    Global.TravelerID = Convert.ToInt32(result); // Save Traveler ID globally
                    MessageBox.Show("Registration Successful! Traveler ID: " + Global.TravelerID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    this.Hide();
                    TravelerManagement travelerManagement = new TravelerManagement();
                    travelerManagement.Show();


                    // Optional: Redirect to dashboard or main traveler form

                }
                MessageBox.Show("Registration Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            // Optionally redirect to Login Form here
        }
        catch (Exception ex)
        {
            MessageBox.Show("Registration Failed!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            con.Close();
        }
    }

    private void ClearFields()
    {
        txtFirstName.Clear();
        txtMiddleName.Clear();
        txtLastName.Clear();
        txtEmail.Clear();
        txtPassword.Clear();
        txtConfirmPassword.Clear();
        txtNationality.Clear();
        txtPreferences.Clear();
        dtpDOB.Value = DateTime.Now;
    }



        private void button2_Click(object sender, EventArgs e)
        {
            
        
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
