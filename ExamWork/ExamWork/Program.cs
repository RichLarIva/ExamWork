using System;
using System.Net;
using System.Security.Policy;
using System.Text;
using MySql.Data.MySqlClient;
namespace ExamWork;

class Program
{

    static void Main(string[] args)
    {
        MySQLDataManager mySqlData = new MySQLDataManager();
        ConnectionManager connectionManager = new ConnectionManager();
        connectionManager.Setup();

        // scannedCodes list is used to manage whether the code has been scanned previously.
        List<string> scannedCodes = new List<string>();
        string personName = "";
        
        while (true)
        {
            bool userFound = false;
            Console.Write("Barcode: ");
            string barCode = Console.ReadLine().ToUpper();
            string query = $"SELECT * FROM Persons WHERE Barcode='{barCode}'";
            mySqlData.Connect("root", "", "MatsalApplikation");
            MySqlCommand cmd = mySqlData.Query(query);
            MySqlDataReader reader;
            
            try
            {
                reader = cmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        personName = reader.GetString(0);
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
                // If it hasn't been scanned add it to the list.
                if (!scannedCodes.Contains(barCode))
                {
                    Console.WriteLine(personName);
                    scannedCodes.Add(barCode);
                    query = connectionManager.ReturnQuery(personName, barCode, 1);
                }
                // If it has been scanned before, add the error code into the query.
                else if (scannedCodes.Contains(barCode))
                {
                    Console.WriteLine($"Already scanned {personName}");
                    query = connectionManager.ReturnQuery("Already Scanned", barCode, 0);
                }
                MySqlCommand sendData = mySqlData.Query(query);
                try
                {
                    MySqlDataReader newReader = sendData.ExecuteReader();
                }
                catch (Exception err)
                {
                    Console.WriteLine($"FAILED TO STORE {err.ToString()}");
                }
            }
            mySqlData.CloseConnection();
        }
        
    }
}

