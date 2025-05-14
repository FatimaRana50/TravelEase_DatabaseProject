namespace DB_module2
{
    partial class travelpass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridView dgv;
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtExpiryDate = new System.Windows.Forms.TextBox();
            this.txtIssueDate = new System.Windows.Forms.TextBox();
            this.rtbActivity = new System.Windows.Forms.RichTextBox();
            this.rtbVoucher = new System.Windows.Forms.RichTextBox();
            this.rtbEticket = new System.Windows.Forms.RichTextBox();
            dgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new System.Drawing.Point(34, 81);
            dgv.Name = "dgv";
            dgv.RowHeadersWidth = 62;
            dgv.RowTemplate.Height = 28;
            dgv.Size = new System.Drawing.Size(960, 150);
            dgv.TabIndex = 0;
            dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(389, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "Available Travel Passes";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "E ticket ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 460);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Issue Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(29, 389);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Activity Pass";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 312);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 25);
            this.label6.TabIndex = 11;
            this.label6.Text = "Hotel Voucher";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(39, 521);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 25);
            this.label7.TabIndex = 12;
            this.label7.Text = "Expiry Date";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtExpiryDate
            // 
            this.txtExpiryDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtExpiryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpiryDate.Location = new System.Drawing.Point(180, 521);
            this.txtExpiryDate.Name = "txtExpiryDate";
            this.txtExpiryDate.Size = new System.Drawing.Size(142, 30);
            this.txtExpiryDate.TabIndex = 5;
            this.txtExpiryDate.TextChanged += new System.EventHandler(this.textExpiryDate_TextChanged);
            // 
            // txtIssueDate
            // 
            this.txtIssueDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtIssueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIssueDate.Location = new System.Drawing.Point(180, 460);
            this.txtIssueDate.Name = "txtIssueDate";
            this.txtIssueDate.Size = new System.Drawing.Size(142, 30);
            this.txtIssueDate.TabIndex = 4;
            this.txtIssueDate.TextChanged += new System.EventHandler(this.txtIssueDate_TextChanged);
            // 
            // rtbActivity
            // 
            this.rtbActivity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rtbActivity.Location = new System.Drawing.Point(180, 386);
            this.rtbActivity.Name = "rtbActivity";
            this.rtbActivity.Size = new System.Drawing.Size(154, 46);
            this.rtbActivity.TabIndex = 3;
            this.rtbActivity.Text = "";
            this.rtbActivity.TextChanged += new System.EventHandler(this.rtbActvity_TextChanged);
            // 
            // rtbVoucher
            // 
            this.rtbVoucher.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rtbVoucher.Location = new System.Drawing.Point(180, 312);
            this.rtbVoucher.Name = "rtbVoucher";
            this.rtbVoucher.Size = new System.Drawing.Size(154, 41);
            this.rtbVoucher.TabIndex = 2;
            this.rtbVoucher.Text = "";
            // 
            // rtbEticket
            // 
            this.rtbEticket.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rtbEticket.Location = new System.Drawing.Point(180, 237);
            this.rtbEticket.Name = "rtbEticket";
            this.rtbEticket.Size = new System.Drawing.Size(154, 46);
            this.rtbEticket.TabIndex = 1;
            this.rtbEticket.Text = "";
            this.rtbEticket.TextChanged += new System.EventHandler(this.rtbETicket_TextChanged);
            // 
            // travelpass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1178, 544);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtExpiryDate);
            this.Controls.Add(this.txtIssueDate);
            this.Controls.Add(this.rtbActivity);
            this.Controls.Add(this.rtbVoucher);
            this.Controls.Add(this.rtbEticket);
            this.Controls.Add(dgv);
            this.Name = "travelpass";
            this.Text = "travelpass";
            ((System.ComponentModel.ISupportInitialize)(dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtExpiryDate;
        private System.Windows.Forms.TextBox txtIssueDate;
        private System.Windows.Forms.RichTextBox rtbActivity;
        private System.Windows.Forms.RichTextBox rtbVoucher;
        private System.Windows.Forms.RichTextBox rtbEticket;
    }
}