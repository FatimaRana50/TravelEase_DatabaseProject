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
    public partial class ServiceSelectionForm : Form
    {
        int travelerID;
        int tripID;
        string connectionString = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=TravelEase2;Integrated Security=True;Encrypt=False";
        public ServiceSelectionForm(int travelerID, int tripID)
        {
            InitializeComponent();
            this.Size = new Size(1200, 600);
            this.MaximumSize = new Size(1100, 600);
            this.MinimumSize = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.travelerID = travelerID;
            this.tripID = tripID;
            LoadServices();
        }

        private void ServiceSelectionForm_Load(object sender, EventArgs e)
        {
            LoadServices();
        }
        private void LoadServices()
        {
            string query = @"
    SELECT 
        s.ServiceID,
        s.Name,
        s.Type,
        s.Description,
        ISNULL(r.RoomType, '') AS RoomType,
        ISNULL(r.TotalRooms, 0) AS TotalRooms,
        ISNULL(tg.LanguageSpoken, '') AS GuideLanguage,
        ISNULL(tg.Experience, 0) AS GuideExperience,
        ISNULL(ts.VehicleType, '') AS TransportVehicleType,
        ISNULL(ts.Capacity, 0) AS TransportCapacity
    FROM Services s
    LEFT JOIN RoomService r ON s.ServiceID = r.ServiceID
    LEFT JOIN TourGuideService tg ON s.ServiceID = tg.ServiceID
    LEFT JOIN TransportService ts ON s.ServiceID = ts.ServiceID
   ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // Clear the CheckedListBox before loading new data
                checkedListBox1.Items.Clear();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string type = reader.GetString(2);
                    string description = reader.GetString(3);  // Service description

                    string extraInfo = "";

                    // Build the extra info based on the service type
                    if (type == "Room")
                    {
                        string roomType = reader["RoomType"].ToString();
                        int totalRooms = reader.GetInt32(4);
                        extraInfo = $"Room Type: {roomType}, Total Rooms: {totalRooms}";
                    }
                    else if (type == "Tour Guide")
                    {
                        string guideLanguage = reader["GuideLanguage"].ToString();
                        int guideExperience = reader.GetInt32(5);
                        extraInfo = $"Language: {guideLanguage}, Experience: {guideExperience} years";
                    }
                    else if (type == "Transport")
                    {
                        string vehicleType = reader["TransportVehicleType"].ToString();
                        int transportCapacity = reader.GetInt32(7);
                        extraInfo = $"Vehicle: {vehicleType}, Capacity: {transportCapacity} people";
                    }

                    // Combine the service name, type, description, and extra info
                    string displayText = $"{name} ({type}) - {description} - {extraInfo}";

                    // Add the item to the CheckedListBox
                    checkedListBox1.Items.Add(new ServiceItem { ServiceID = id, Display = displayText });
                }
            }

            // Set the DisplayMember and ValueMember properties of the CheckedListBox
            checkedListBox1.DisplayMember = "Display";
            checkedListBox1.ValueMember = "ServiceID";
        }
        // ServiceItem class to hold the service information
        private class ServiceItem
        {
            public int ServiceID { get; set; }
            public string Display { get; set; }
            public override string ToString() => Display;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (ServiceItem item in checkedListBox1.CheckedItems)
                {
                    string insertQuery = @"
                INSERT INTO TravelerServiceSelection (TravelerID, TripID, ServiceID) 
                VALUES (@TravelerID, @TripID, @ServiceID)";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@TravelerID", travelerID);
                    cmd.Parameters.AddWithValue("@TripID", tripID);
                    cmd.Parameters.AddWithValue("@ServiceID", item.ServiceID);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Services selected successfully!");
            this.Close();
        }
    }
}
