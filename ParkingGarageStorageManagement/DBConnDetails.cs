namespace ParkingGarageStorageManagement;

public class DBConnDetails
{
    public string Server { get; set; }
    public string DatabaseName { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }

    public DBConnDetails(string server, string databaseName, string userId, string password)
    {
        Server = server;
        DatabaseName = databaseName;
        UserId = userId;
        Password = password;
    }
}