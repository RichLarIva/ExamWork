using System;
using MySql.Data.MySqlClient;

namespace ExamWork;
class Program
{
    static MySqlConnection conn;

    public static void Connect(string user, string password)
    {
        conn = new MySqlConnection();
        try
        {
            string connectionString = "datasource=127.0.0.1;port=";
        }
        catch(Exception err)
        {
            Console.WriteLine(err.ToString);
        }

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Console.ReadKey();
    }
}

