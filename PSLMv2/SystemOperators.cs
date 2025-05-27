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
            string[] parkingZoneArrayB1 = { "A1", "A2", "A3" };
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

            string[] paidTypes = { "Guest", "Staff", "Services" };
            string isPaidValue = paidTypes.Contains(customerType) ? "yes" : "no";

            using (var con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();
                    string transactionQuery = @"INSERT INTO transaction 
                        (plate_number, vehicle_type, customer_type, floor, parking_zone, checked_in_by, is_paid)
                        VALUES (@plate_number, @vehicle_type, @customer_type, @floor, @parking_zone, @checked_in_by, @is_paid)";

                    using (var app = new MySqlCommand(transactionQuery, con))
                    {
                        app.Parameters.AddWithValue("@plate_number", plateNumber);
                        app.Parameters.AddWithValue("@vehicle_type", vehicleType);
                        app.Parameters.AddWithValue("@customer_type", customerType);
                        app.Parameters.AddWithValue("@floor", mappedFloor);
                        app.Parameters.AddWithValue("@parking_zone", parkingZone);
                        app.Parameters.AddWithValue("@checked_in_by", Session.UserID.ToString());
                        app.Parameters.AddWithValue("@is_paid", isPaidValue);

                        int affectedRows = app.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
                catch (MySqlException ex)
                {
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
                        vehicle_type AS 'Vehicle Type',
                        is_paid AS 'Is Paid'
                        FROM transaction
                        WHERE checkout_time IS NULL
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

        public static (bool Success, string Message, decimal Fee) CheckOut(string plateNumber)
        {
            using (var con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();

                    // 1. Retrieve the latest transaction for the given plate number
                    string selectQuery = @"SELECT transaction_id, checkin_time, is_paid FROM transaction 
                                           WHERE plate_number = @plate_number 
                                           ORDER BY checkin_time DESC LIMIT 1";
                    using (var selectCmd = new MySqlCommand(selectQuery, con))
                    {
                        selectCmd.Parameters.AddWithValue("@plate_number", plateNumber);

                        using (var reader = selectCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                return (false, "Plate number not found.", 0);
                            }

                            int transactionId = reader.GetInt32("transaction_id");
                            DateTime checkInTime = reader.GetDateTime("checkin_time");
                            string isPaid = reader["is_paid"]?.ToString();

                            reader.Close();

                            // Check for active monthly pass
                            bool isPassUser = HasActiveMonthlyPass(plateNumber);

                            if (isPassUser)
                            {
                                // Set is_paid = 'yes', is_pass_user = 'true', skip fee
                                string updateQuery = @"UPDATE transaction 
                                                       SET is_paid = 'yes', is_pass_user = 'true', checkout_time = @checkout_time, checked_out_by = @checked_out_by 
                                                       WHERE transaction_id = @transaction_id";
                                using (var updateCmd = new MySqlCommand(updateQuery, con))
                                {
                                    updateCmd.Parameters.AddWithValue("@checkout_time", DateTime.Now);
                                    updateCmd.Parameters.AddWithValue("@checked_out_by", Session.UserID.ToString());
                                    updateCmd.Parameters.AddWithValue("@transaction_id", transactionId);
                                    updateCmd.ExecuteNonQuery();
                                }
                                return (true, "No fee required. Monthly pass user.", 0);
                            }
                            else if (isPaid == "yes")
                            {
                                // Already paid, no fee required, but set is_pass_user = 'false'
                                string updateQuery = @"UPDATE transaction 
                                                       SET checkout_time = @checkout_time, checked_out_by = @checked_out_by, is_pass_user = 'false'
                                                       WHERE transaction_id = @transaction_id";
                                using (var updateCmd = new MySqlCommand(updateQuery, con))
                                {
                                    updateCmd.Parameters.AddWithValue("@checkout_time", DateTime.Now);
                                    updateCmd.Parameters.AddWithValue("@checked_out_by", Session.UserID.ToString());
                                    updateCmd.Parameters.AddWithValue("@transaction_id", transactionId);
                                    updateCmd.ExecuteNonQuery();
                                }
                                return (true, "No fee required. Already paid/exempted.", 0);
                            }
                            else
                            {
                                // Calculate fee, set is_pass_user = 'false'
                                DateTime now = DateTime.Now;
                                TimeSpan duration = now - checkInTime;
                                int hours = (int)Math.Ceiling(duration.TotalHours);
                                decimal fee = hours * 30m;

                                string updateQuery = @"UPDATE transaction 
                                                       SET is_paid = 'yes', checkout_time = @checkout_time, fee = @fee, checked_out_by = @checked_out_by, is_pass_user = 'false'
                                                       WHERE transaction_id = @transaction_id";
                                using (var updateCmd = new MySqlCommand(updateQuery, con))
                                {
                                    updateCmd.Parameters.AddWithValue("@checkout_time", now);
                                    updateCmd.Parameters.AddWithValue("@fee", fee);
                                    updateCmd.Parameters.AddWithValue("@checked_out_by", Session.UserID.ToString());
                                    updateCmd.Parameters.AddWithValue("@transaction_id", transactionId);
                                    updateCmd.ExecuteNonQuery();
                                }

                                return (true, $"Fee to pay: {fee} pesos.", fee);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                    return (false, "Database error occurred.", 0);
                }
            }
        }

        public static DataTable GetCompletedTransactions()
        {
            DataTable dt = new DataTable();
            using (var con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = @"
                        SELECT 
                            t.transaction_id AS 'Transaction ID',
                            t.plate_number AS 'Plate No.',
                            t.vehicle_type AS 'Vehicle',
                            t.customer_type AS 'Customer',
                            t.floor AS Floor,
                            t.parking_zone AS 'Zone',
                            t.checkin_time AS 'Check-in',
                            t.checkout_time AS 'Check-out',
                            t.is_paid AS 'Paid Status',
                            t.fee AS 'Fee',
                            t.is_pass_user AS 'Pass User',
                            u_in.fullname AS 'Checked In By',
                            u_out.fullname AS 'Checked Out By'
                        FROM transaction t
                        LEFT JOIN users u_in ON t.checked_in_by = u_in.userID
                        LEFT JOIN users u_out ON t.checked_out_by = u_out.userID
                        WHERE t.checkout_time IS NOT NULL AND t.fee IS NOT NULL
                        ORDER BY t.checkout_time DESC";
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
        public static (bool Success, string Message) GrantMonthlyPass(string plateNumber, string customerType)
        {
            // Cross-check in the transaction table if this plate number is a Visitor
            using (var con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();

                    string visitorCheckQuery = @"SELECT COUNT(*) FROM transaction 
                                                 WHERE plate_number = @plate_number 
                                                 AND customer_type = 'Visitors'";
                    using (var visitorCheckCmd = new MySqlCommand(visitorCheckQuery, con))
                    {
                        visitorCheckCmd.Parameters.AddWithValue("@plate_number", plateNumber);
                        int visitorCount = Convert.ToInt32(visitorCheckCmd.ExecuteScalar());
                        if (visitorCount == 0)
                            return (false, "This plate number does not belong to a Visitor. Only Visitors can avail a monthly pass.");
                    }

                    // Check for existing active pass
                    string checkQuery = @"SELECT COUNT(*) FROM monthly_access_pass 
                                          WHERE plate_number = @plate_number 
                                          AND status = 'Active'
                                          AND end_date >= @today";
                    using (var checkCmd = new MySqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@plate_number", plateNumber);
                        checkCmd.Parameters.AddWithValue("@today", DateTime.Today);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                            return (false, "This plate already has an active monthly pass.");
                    }

                    // Insert new pass
                    string insertQuery = @"INSERT INTO monthly_access_pass 
                        (plate_number, start_date, end_date, issued_by, status)
                        VALUES (@plate_number, @start_date, @end_date, @issued_by, 'Active')";
                    using (var insertCmd = new MySqlCommand(insertQuery, con))
                    {
                        insertCmd.Parameters.AddWithValue("@plate_number", plateNumber);
                        insertCmd.Parameters.AddWithValue("@start_date", DateTime.Today);
                        insertCmd.Parameters.AddWithValue("@end_date", DateTime.Today.AddDays(30));
                        insertCmd.Parameters.AddWithValue("@issued_by", Session.UserID.ToString());
                        insertCmd.ExecuteNonQuery();
                    }
                    return (true, "Monthly pass granted successfully.");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                    return (false, "Database error occurred.");
                }
            }
        }

        // Check if a plate number has an active monthly pass
        public static bool HasActiveMonthlyPass(string plateNumber)
        {
            using (var con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = @"SELECT COUNT(*) FROM monthly_access_pass 
                                     WHERE plate_number = @plate_number 
                                     AND status = 'Active'
                                     AND start_date <= @today AND end_date >= @today";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@plate_number", plateNumber);
                        cmd.Parameters.AddWithValue("@today", DateTime.Today);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                    return false;
                }
            }
        }

        public static DataTable GetMonthlyAccessPasses()
        {
            DataTable dt = new DataTable();
            using (var con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = @"
                        SELECT 
                            m.pass_id AS 'Pass ID',
                            m.plate_number AS 'Plate Number',
                            m.start_date AS 'Start Date',
                            m.end_date AS 'End Date',
                            COALESCE(u.fullname, 'Unknown') AS 'Issued By',
                            m.status AS 'Status'
                        FROM monthly_access_pass m
                        LEFT JOIN users u ON m.issued_by = u.userID
                        ORDER BY m.start_date DESC";
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

        public static DataTable GetVisitorTransactions()
        {
            var dt = new DataTable();
            using (var con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = @"
                        SELECT 
                            transaction_id AS 'Transaction ID', 
                            plate_number AS 'Plate Number', 
                            checkin_time AS 'Check-In Time'
                        FROM transaction
                        WHERE customer_type = 'Visitors'
                        ORDER BY checkin_time DESC";
                    using (var cmd = new MySqlCommand(query, con))
                    using (var adapter = new MySqlDataAdapter(cmd))
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

        public static (bool Success, string Message) RenewMonthlyPass(string plateNumber)
        {
            using (var con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();

                    // Check for expired active pass
                    string checkQuery = @"SELECT pass_id FROM monthly_access_pass 
                                  WHERE plate_number = @plate_number 
                                  AND status = 'Active'
                                  AND end_date < @today
                                  ORDER BY end_date DESC LIMIT 1";
                    int? passId = null;
                    using (var checkCmd = new MySqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@plate_number", plateNumber);
                        checkCmd.Parameters.AddWithValue("@today", DateTime.Today);
                        var result = checkCmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            passId = Convert.ToInt32(result);
                    }

                    if (passId == null)
                        return (false, "No expired active pass found for this plate number. Only expired passes can be renewed.");

                    // Renew: update start_date and end_date
                    string updateQuery = @"UPDATE monthly_access_pass
                                   SET start_date = @start_date, end_date = @end_date
                                   WHERE pass_id = @pass_id";
                    using (var updateCmd = new MySqlCommand(updateQuery, con))
                    {
                        updateCmd.Parameters.AddWithValue("@start_date", DateTime.Today);
                        updateCmd.Parameters.AddWithValue("@end_date", DateTime.Today.AddDays(30));
                        updateCmd.Parameters.AddWithValue("@pass_id", passId.Value);
                        updateCmd.ExecuteNonQuery();
                    }

                    return (true, "Monthly access pass renewed for 30 days.");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                    return (false, "Database error occurred.");
                }
            }
        }

        public static DataTable GetZoneOccupancy()
        {
            DataTable dt = new DataTable();
            using (var con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = @"
                SELECT parking_zone, COUNT(*) AS occupied
                FROM transaction
                WHERE checkout_time IS NULL AND parking_zone IN ('A1','A2','A3','A4','A5','A6')
                GROUP BY parking_zone";
                    using (var cmd = new MySqlCommand(query, con))
                    using (var adapter = new MySqlDataAdapter(cmd))
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
