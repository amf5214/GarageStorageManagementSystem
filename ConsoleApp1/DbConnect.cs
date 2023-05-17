using MySql.Data.MySqlClient;
using System;

namespace ConsoleApp1;

public class DbConnect
{
    protected MySqlConnection Connection;
    protected DBConnDetails DbDetails;

    //Constructor
    public DbConnect(DBConnDetails connDetails)
    {
        DbDetails = connDetails;
        Initialize();
        bool output = OpenConnection();
        if (!output)
        {
            return;
        }
        CloseConnection();
        
    }

    //Initialize values
    protected void Initialize()
    {
        string connectionString = $"server={DbDetails.Server} database={DbDetails.DatabaseName} uid={DbDetails.UserId} password={DbDetails.Password}";
        Connection = new MySqlConnection(connectionString);
    }

    //open connection to database
    protected bool OpenConnection()
    {
        try
        {
            Connection.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex);
            //When handling errors, you can your application's response based 
            //on the error number.
            //The two most common error numbers when connecting are as follows:
            //0: Cannot connect to server.
            //1045: Invalid user name and/or password.
            switch (ex.Number)
            {
                case 0:
                    Console.WriteLine("Cannot connect to server.  Contact administrator");
                    break;

                case 1045:
                    Console.WriteLine("Invalid username/password, please try again");
                    break;
            }
            return false;
        }
    }
    
    //Close connection
    protected bool CloseConnection()
    {
        try
        {
            Connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}