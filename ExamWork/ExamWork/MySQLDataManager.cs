using System;
using MySql.Data.MySqlClient;

namespace ExamWork
{
    public class MySQLDataManager
    {
        private MySqlConnection conn;

        public void Connect(string user, string password, string db)
        {
            conn = new MySqlConnection();
            try
            {
                string connectionString = $"datasource=127.0.0.1;port=3306;username={user};password={password};database={db}";
                conn.ConnectionString = connectionString;
                conn.Open();
                Console.WriteLine($"Successfully connected to {db}");

            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }

        public MySqlCommand Query(string query)
        {
            MySqlCommand command = new MySqlCommand(query, conn);
            command.CommandTimeout = 60;
            return command;
        }

        public void CloseConnection()
        {
            conn.Close();
        }
    }
}

