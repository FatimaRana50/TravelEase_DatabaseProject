namespace DB_module2
{
    partial class CancelBooking
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
            this.btnCancelBooking = new System.Windows.Forms.Button();
            this.dgvUpcomingTrips = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpcomingTrips)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "List of this traveler’s upcoming bookings";
            // 
            // btnCancelBooking
            // 
            this.btnCancelBooking.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelBooking.BackColor = System.Drawing.Color.Cornsilk;
            this.btnCancelBooking.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelBooking.Location = new System.Drawing.Point(278, 397);
            this.btnCancelBooking.Name = "btnCancelBooking";
            this.btnCancelBooking.Size = new System.Drawing.Size(153, 66);
            this.btnCancelBooking.TabIndex = 6;
            this.btnCancelBooking.Text = "Cancel Booking";
            this.btnCancelBooking.UseVisualStyleBackColor = false;
            this.btnCancelBooking.Click += new System.EventHandler(this.btnCancelBooking_Click);
            // 
            // dgvUpcomingTrips
            // 
            this.dgvUpcomingTrips.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvUpcomingTrips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpcomingTrips.Location = new System.Drawing.Point(55, 102);
            this.dgvUpcomingTrips.Name = "dgvUpcomingTrips";
            this.dgvUpcomingTrips.RowHeadersWidth = 62;
            this.dgvUpcomingTrips.RowTemplate.Height = 28;
            this.dgvUpcomingTrips.ShowCellErrors = false;
            this.dgvUpcomingTrips.Size = new System.Drawing.Size(851, 206);
            this.dgvUpcomingTrips.TabIndex = 7;
            this.dgvUpcomingTrips.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUpcomingTrips_CellContentClick);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.SystemColors.Info;
            this.button1.Location = new System.Drawing.Point(539, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 66);
            this.button1.TabIndex = 8;
            this.button1.Text = "Get TravelPass";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // CancelBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
          //  this.BackgroundImage = global::final_proj.Properties.Resources.Untitled_design;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(956, 660);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvUpcomingTrips);
            this.Controls.Add(this.btnCancelBooking);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Name = "CancelBooking";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "TripDashboardForm";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpcomingTrips)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvUpcomingTrips;
        private System.Windows.Forms.Button btnCancelBooking;
        private System.Windows.Forms.Button button1;
    }
}