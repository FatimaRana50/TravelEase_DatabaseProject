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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Resize += Home_Resize;
            //this.Resize += CenterLabel;
            //CenterLabel(null, null);
        }
        private void Home_Resize(object sender, EventArgs e)
        {
            float scaleX = this.Width / 1f;
            float scaleY = this.Height / 800f;

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Font = new Font("Segoe UI", 12 * Math.Min(scaleX, scaleY));

                // Optional: scale size if AutoSize = false
                ctrl.Width = (int)(ctrl.Width * scaleX);
                ctrl.Height = (int)(ctrl.Height * scaleY);
            }
        }
        private void CenterLabel(object sender, EventArgs e)
        {
            // Replace 'label1' with your actual label's name
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 2;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
            //this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_SignUP admin = new Admin_SignUP();
            admin.Show();
           // this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Service_Provider_signup service_Provider_signup = new Service_Provider_signup();
            service_Provider_signup.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();
            registration.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
