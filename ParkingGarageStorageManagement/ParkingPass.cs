namespace ParkingGarageStorageManagement;

public class ParkingPass
{
    public int PassId { get; set; }
    public ParkingPassType Type { get; set; }
    public int UserId { get; set; }
    public DateOnly PassStartDate { get; set; }
    public DateOnly PassExpirationDate { get; set; }

    public ParkingPass(int passId, ParkingPassType type, int userId, DateOnly passStartDate, ParkingPassTerm term)
    {
        PassId = passId;
        Type = type;
        UserId = userId;
        PassStartDate = passStartDate;
        PassExpirationDate = GetExpirationDate(term, passStartDate);
    }

    public ParkingPass(int passId, ParkingPassType type, int userId, DateOnly passStartDate,
        DateOnly passExpirationDate)
    {
        PassId = passId;
        Type = type;
        UserId = userId;
        PassStartDate = passStartDate;
        PassExpirationDate = passExpirationDate;
    }

    public ParkingPass(ParkingPassType type, int userId, DateOnly passStartDate, ParkingPassTerm term)
    {
        PassId = -1;
        Type = type;
        UserId = userId;
        PassStartDate = passStartDate;
        PassExpirationDate = GetExpirationDate(term, passStartDate);
    }

    public static DateOnly GetExpirationDate(ParkingPassTerm term, DateOnly startDate)
    {
        switch (term)
        {
            case ParkingPassTerm.Daily:
                return (startDate.AddDays(1));

            case ParkingPassTerm.SixMonth:
                return (startDate.AddMonths(6));

            case ParkingPassTerm.ThreeMonth:
                return (startDate.AddMonths(3));

            case ParkingPassTerm.TwelveMonth:
                return (startDate.AddYears(1));

            case ParkingPassTerm.Permanent:
                return new DateOnly(9999, 1, 1);
        }

        return new DateOnly(9999, 1, 1);
    }

    public override string ToString()
    {
        string returnString = PassId != -1 ? PassId + "" : "";
        return $"{returnString} {UserId} {Type} {PassStartDate} {PassExpirationDate}";
    }


}