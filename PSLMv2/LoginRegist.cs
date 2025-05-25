using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PSLMv2
{
    public static class Session
    {
        public static int UserID { get; set; }
        public static string Role { get; set; }
        public static string Username { get; set; }
    }
    public class InsertUser
    {

        public static class DatabaseConfiguration
        {
            public static string ConnectionString = "server=localhost; Database=pslmv2; user=root; password=;";
        }

        public static void InsertNewUser(string username, string password_hash, string fullname, string role)
        {
            using (MySqlConnection con = new MySqlConnection(DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();
                    string Insertquery = "INSERT INTO users (username, password_hash, fullname, role) VALUES (@username, @password_hash, @fullname, @role)";

                    using (MySqlCommand app = new MySqlCommand(Insertquery, con))
                    {
                        app.Parameters.AddWithValue("@username", username);
                        app.Parameters.AddWithValue("@password_hash", password_hash);
                        app.Parameters.AddWithValue("@fullname", fullname);
                        app.Parameters.AddWithValue("@role", role);
                        int affectRows = app.ExecuteNonQuery();

                        if (affectRows > 0)
                        {
                            MessageBox.Show("User inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("User insertion failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            
        }
    }

    public class LoginUser 
    { 
        public static bool Authenticate(string username, string password)
        {
            using (MySqlConnection con = new MySqlConnection(InsertUser.DatabaseConfiguration.ConnectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM users WHERE username = @username AND password_hash = @password_hash";
                    using (MySqlCommand app = new MySqlCommand(query, con))
                    {
                        app.Parameters.AddWithValue("@username", username);
                        app.Parameters.AddWithValue("@password_hash", password);
                        using (MySqlDataReader reader = app.ExecuteReader())
                        {
                         if (reader.Read())
                            {
                                Session.UserID = reader.GetInt32("userID");
                                Session.Role = reader.GetString("role");
                                Session.Username = username;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                    return false;
                }
            }
        }
    }


}
