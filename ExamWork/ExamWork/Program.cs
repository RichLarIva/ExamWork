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
        List<string> scannedCodes = new List<string>();
        bool userFound = false;
        while (true)
        {
            
            Console.Write("Barcode: ");
            string barCode = Console.ReadLine();
            string query = $"SELECT * FROM Persons WHERE Barcode='{barCode}'";
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
                        userFound = true;
                    }
                }
                else
                {
                    Console.WriteLine("Couldn't find user.");
                }
                reader.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            if (userFound)
            {
                if (!scannedCodes.Contains(barCode))
                {
                    Console.WriteLine("Dont exist");
                    scannedCodes.Add(barCode);
                    query = $"INSERT INTO LunchScans(Barcode, ScanCode) VALUES('{barCode}', '1')";
                }
                else if (scannedCodes.Contains(barCode))
                {
                    query = $"INSERT INTO LunchScans(`Barcode`, `ScanCode`) VALUES('{barCode}', '0')";
                }
                MySqlCommand sendData = new MySqlCommand(query, conn);
                sendData.CommandTimeout = 60;
                try
                {
                    MySqlDataReader newReader = sendData.ExecuteReader();
                }
                catch (Exception err)
                {
                    Console.WriteLine($"FAILED TO STORE {err.ToString()}");
                }
            }
            conn.Close();
            Console.WriteLine("Hello, World!");
        }
    }
}

