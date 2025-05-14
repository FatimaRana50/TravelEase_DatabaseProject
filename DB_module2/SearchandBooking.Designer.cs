namespace DB_module2
{
    partial class SearchandBooking
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.numMinPrice = new System.Windows.Forms.NumericUpDown();
            this.numMaxPrice = new System.Windows.Forms.NumericUpDown();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvTrips = new System.Windows.Forms.DataGridView();
            this.btnBook = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.numGroupSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numMinPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrips)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = " Destination";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Start Date";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(677, 72);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Select End Date";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 147);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Min Price";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(260, 147);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Max Price";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(492, 147);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Select Group Size";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpStartDate.Location = new System.Drawing.Point(421, 69);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(209, 26);
            this.dtpStartDate.TabIndex = 7;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerStartDate_ValueChanged);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpEndDate.Location = new System.Drawing.Point(810, 69);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(221, 26);
            this.dtpEndDate.TabIndex = 8;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dateTimePickerEndDate_ValueChanged);
            // 
            // numMinPrice
            // 
            this.numMinPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numMinPrice.Location = new System.Drawing.Point(102, 147);
            this.numMinPrice.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numMinPrice.Name = "numMinPrice";
            this.numMinPrice.Size = new System.Drawing.Size(120, 26);
            this.numMinPrice.TabIndex = 9;
            this.numMinPrice.ValueChanged += new System.EventHandler(this.textBoxMinPrice_ValueChanged);
            // 
            // numMaxPrice
            // 
            this.numMaxPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numMaxPrice.Location = new System.Drawing.Point(344, 145);
            this.numMaxPrice.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numMaxPrice.Name = "numMaxPrice";
            this.numMaxPrice.Size = new System.Drawing.Size(120, 26);
            this.numMaxPrice.TabIndex = 10;
            this.numMaxPrice.ValueChanged += new System.EventHandler(this.textBoxMaxPrice_ValueChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.Location = new System.Drawing.Point(466, 205);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(147, 46);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.Text = "Search Trips";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.buttonSearchTrips_Click);
            // 
            // dgvTrips
            // 
            this.dgvTrips.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvTrips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrips.Location = new System.Drawing.Point(120, 283);
            this.dgvTrips.Name = "dgvTrips";
            this.dgvTrips.RowHeadersWidth = 62;
            this.dgvTrips.RowTemplate.Height = 28;
            this.dgvTrips.Size = new System.Drawing.Size(806, 182);
            this.dgvTrips.TabIndex = 13;
            this.dgvTrips.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTrips_CellContentClick);
            // 
            // btnBook
            // 
            this.btnBook.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBook.Location = new System.Drawing.Point(421, 491);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(220, 51);
            this.btnBook.TabIndex = 14;
            this.btnBook.Text = "Book Trips";
            this.btnBook.UseVisualStyleBackColor = true;
            this.btnBook.Click += new System.EventHandler(this.buttonBookTrips_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(333, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(409, 32);
            this.label7.TabIndex = 15;
            this.label7.Text = "Trips Searching And Booking";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(832, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 20);
            this.label8.TabIndex = 18;
            this.label8.Text = "activity";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // cmbCategory
            // 
            this.cmbCategory.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "Budget",
            "Cultural",
            "Luxury",
            "Relaxation",
            "Budget",
            "Relaxation",
            "Relaxation",
            "Adventure",
            "Budget",
            "Luxury",
            "Cultural",
            "Budget",
            "Adventure",
            "Adventure",
            "Relaxation",
            "Budget",
            "Cultural",
            "Relaxation",
            "Adventure",
            "Adventure",
            "Budget",
            "Budget",
            "Cultural",
            "Budget",
            "Luxury",
            "Budget",
            "Luxury",
            "Budget",
            "Budget",
            "Relaxation",
            "Relaxation",
            "Cultural",
            "Budget",
            "Adventure",
            "Relaxation",
            "Budget",
            "Adventure",
            "Budget",
            "Cultural",
            "Luxury",
            "Relaxation",
            "Relaxation",
            "Luxury",
            "Cultural",
            "Adventure",
            "Relaxation",
            "Cultural",
            "Budget",
            "Budget",
            "Relaxation"});
            this.cmbCategory.Location = new System.Drawing.Point(910, 145);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 28);
            this.cmbCategory.TabIndex = 19;
            // 
            // txtDestination
            // 
            this.txtDestination.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDestination.Location = new System.Drawing.Point(120, 69);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(131, 26);
            this.txtDestination.TabIndex = 20;
            this.txtDestination.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // numGroupSize
            // 
            this.numGroupSize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numGroupSize.Location = new System.Drawing.Point(657, 145);
            this.numGroupSize.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numGroupSize.Name = "numGroupSize";
            this.numGroupSize.Size = new System.Drawing.Size(120, 26);
            this.numGroupSize.TabIndex = 21;
            // 
            // SearchandBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           // this.BackgroundImage = global::final_proj.Properties.Resources.Untitled_design__2_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1068, 578);
            this.Controls.Add(this.numGroupSize);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnBook);
            this.Controls.Add(this.dgvTrips);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.numMaxPrice);
            this.Controls.Add(this.numMinPrice);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SearchandBooking";
            this.Text = "BookingSearchForm";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMinPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrips)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.NumericUpDown numMinPrice;
        private System.Windows.Forms.NumericUpDown numMaxPrice;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvTrips;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.NumericUpDown numGroupSize;
    }
}