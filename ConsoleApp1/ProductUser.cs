namespace ConsoleApp1;

public class ProductUser
{
    public int UserId { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserEmail { get; set; }
    public string UserPhoneNumber { get; set; }
    public int PlanId { get; set; }

    public ProductUser(int userId, string userFirstName, string userLastName, string userEmail, string userPhoneNumber, int planId)
    {
        UserId = userId;
        UserFirstName = userFirstName;
        UserLastName = userLastName;
        UserEmail = userEmail;
        UserPhoneNumber = userPhoneNumber;
        PlanId = planId;
    }

    public ProductUser(string userFirstName, string userLastName, string userEmail, string userPhoneNumber, int planId)
    {
        UserFirstName = userFirstName;
        UserLastName = userLastName;
        UserEmail = userEmail;
        UserPhoneNumber = userPhoneNumber;
        PlanId = planId;
    }
}