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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminmanageUser adm = new AdminmanageUser();
            adm.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminManageOperator adm = new AdminManageOperator();
            adm.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminviewsCategory adminviewsCategory = new AdminviewsCategory();
            adminviewsCategory.ShowDialog();
            this.Show();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ModerateReviews moderateReviews = new ModerateReviews();
            moderateReviews.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            if (Global.HomeInstance != null)
            {
                Global.HomeInstance.Show();
                Global.HomeInstance.BringToFront();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReportMenu reportMenu = new ReportMenu();
            reportMenu.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            AuditForm auditForm = new AuditForm();
            auditForm.ShowDialog();
            this.Show();
        }
    }
}
