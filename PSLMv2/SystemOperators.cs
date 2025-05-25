using System;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace PSLMv2
{
    class SystemOperators
    {
        public static bool CheckIn(
            string plateNumber,
            string vehicleType,
            string customerType,
            string floor,
            string parkingZone,
            int checkedInBy)
        {
            string mappedFloor;
            string[] parkingZoneArrayB1 = { "A1", "A2", "A3" }; // Example array for parking zones
            string[] parkingZoneArrayB2 = { "A4", "A5", "A6" };
            if (parkingZoneArrayB1.Contains(parkingZone))
            {
                mappedFloor = "B1";
            }
            else if (parkingZoneArrayB2.Contains(parkingZone))
            {
                mappedFloor = "B2";
            }
            else
            {
                mappedFloor = floor; 
            }
            using (var con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();
                    string transactionQuery = @"INSERT INTO transaction 
                        (plate_number, vehicle_type, customer_type, floor, parking_zone, checked_in_by)
                        VALUES (@plate_number, @vehicle_type, @customer_type, @floor, @parking_zone, @checked_in_by)";

                    using (var app = new MySqlCommand(transactionQuery, con))
                    {
                        app.Parameters.AddWithValue("@plate_number", plateNumber);
                        app.Parameters.AddWithValue("@vehicle_type", vehicleType);
                        app.Parameters.AddWithValue("@customer_type", customerType);
                        app.Parameters.AddWithValue("@floor", mappedFloor);
                        app.Parameters.AddWithValue("@parking_zone", parkingZone);
                        app.Parameters.AddWithValue("@checked_in_by", Session.UserID.ToString());

                        int affectedRows = app.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    // Optionally log or display the error
                    MessageBox.Show("Database Error: " + ex.Message);
                    return false;
                }
            }
        }

        public static DataTable GetRecentCheckIns()
        {
            DataTable dt = new DataTable();
            using (var con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = @"SELECT 
                    plate_number AS 'Plate Number',
                    parking_zone AS 'Parking Zone', 
                    checkin_time AS 'Check-In Time',
                    floor AS 'Floor',
                    customer_type AS 'Customer Type',
                    vehicle_type AS 'Vehicle Type'
                    FROM transaction
                    ORDER BY checkin_time DESC";
                    using (var cmd = new MySqlCommand(query, con))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                }
            }
            return dt;
        }
    }

}
