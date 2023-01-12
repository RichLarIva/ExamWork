using System;
using MySql.Data.MySqlClient;

namespace ExamWork;
class Program
{
    static MySqlConnection conn;


    public static void Query(string query, MySqlConnection connection)
    {
        MySqlCommand command = new MySqlCommand(query, connection);
        command.CommandTimeout = 60;
    }

    public static void Connect(string user, string password, string db)
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

    static void Main(string[] args)
    {
        string query = "SELECT * FROM Persons";
        Connect("root", "", "MatsalApplikation");
        MySqlCommand cmd = new MySqlCommand(query, conn);
        cmd.CommandTimeout = 60;
        MySqlDataReader reader;
        try
        {
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2) };
                    Console.WriteLine(reader.GetString(2));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }

            conn.Close();
        }
        catch(Exception err)
        {
            Console.WriteLine(err.ToString());
        }

        Console.WriteLine("Hello, World!");
        Console.ReadKey();
    }
}

