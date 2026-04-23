using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp.Database
{
    public class DatabaseManager
    {
        // Replace with actual production configuration later
        private readonly string _connectionString = "server=localhost;user=root;password=;database=RestaurantDB";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Connection Error: {ex.Message}");
                return false;
            }
        }

        public DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (var conn = GetConnection())
            {
                using (var cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetTables()
        {
            return ExecuteQuery("SELECT * FROM Tables");
        }
    }
}
