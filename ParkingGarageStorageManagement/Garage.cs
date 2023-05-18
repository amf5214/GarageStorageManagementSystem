namespace ParkingGarageStorageManagement;

public class Garage
{
    /// <summary>
    /// This class creates an object representing an individual car garage that stores car objects and tracks the number
    /// of vehicles in the car garage.
    /// </summary>
    
    public int GarageId { get; set; }
    public string GarageName { get; set; }
    public int AddressId { get; set; }
    public int TotalSpaces { get; set; }
    public int CurrentOccupants { get; set; }
    public int PlanId { get; set; }
    public List<ParkingPassType> AllowedUserTypes { get; set; }

    public Garage(int garageId, string garageName, int addressId, int totalSpaces, int currentOccupants, int planId, List<ParkingPassType> allowedUserTypes)
    {
        GarageId = garageId;
        GarageName = garageName;
        AddressId = addressId;
        TotalSpaces = totalSpaces;
        CurrentOccupants = currentOccupants;
        PlanId = planId;
        AllowedUserTypes = allowedUserTypes;
    }
    
    public Garage(int garageId, string garageName, int addressId, int totalSpaces, int currentOccupants, int planId)
    {
        GarageId = garageId;
        GarageName = garageName;
        AddressId = addressId;
        TotalSpaces = totalSpaces;
        CurrentOccupants = currentOccupants;
        PlanId = planId;
    }

    public Garage(string garageName, int addressId, int totalSpaces, int currentOccupants, int planId, List<ParkingPassType> allowedUserTypes)
    {
        GarageName = garageName;
        AddressId = addressId;
        TotalSpaces = totalSpaces;
        CurrentOccupants = currentOccupants;
        PlanId = planId;
    }
    
    public Garage(string garageName, int addressId, int totalSpaces, int currentOccupants, int planId, string allowedUserTypes)
    {
        GarageName = garageName;
        AddressId = addressId;
        TotalSpaces = totalSpaces;
        CurrentOccupants = currentOccupants;
        PlanId = planId;
        AllowedUserTypes = ConvertJsonStringToList(allowedUserTypes);
    }
    
    public Garage(string garageName, int addressId, int totalSpaces, int currentOccupants, int planId)
    {
        GarageName = garageName;
        AddressId = addressId;
        TotalSpaces = totalSpaces;
        CurrentOccupants = currentOccupants;
        PlanId = planId;
    }

    public Garage()
    {
        
    }

    public GateEvent AddRemCar(string licensePlate, bool isAdd)
    {
        DbQueries query = new DbQueries(Constants.ConnDetails);
        Car car = query.GetCarFromLicensePlate(licensePlate);
        if (car != null)
        {
            Console.WriteLine(car);
            GateEvent gateEvent;
            if (isAdd)
            {
                gateEvent = new GateEvent(Constants.PlanId, GateEventType.Entry.ToString(), car.CarId,
                    GarageId, DateTime.Now);
            }
            else
            {
                gateEvent = new GateEvent(Constants.PlanId, GateEventType.Exit.ToString(), car.CarId,
                    GarageId, DateTime.Now);
            }

            gateEvent = query.CreateGateEvent(gateEvent);
            if (isAdd)
            {
                query.IncDecNumberOccupants(GarageId, true);
            }
            else
            {
                query.IncDecNumberOccupants(GarageId, false);
            }
            return gateEvent;
        }
        else
        {
            Console.WriteLine("Null Returned");
        }

        return null;
    }

    public static List<ParkingPassType> ConvertJsonStringToList(string allowedUserTypes)
    {
        string[] types = (allowedUserTypes.Substring(2, allowedUserTypes.Length - 4)).Split(',');
        List<ParkingPassType> garageTypes = new List<ParkingPassType>();
        
        foreach(string type in types)
        {
            Console.WriteLine(type);
            ParkingPassType passType;
            bool doesWork = ParkingPassType.TryParse(type.Substring(1, type.Length-2), out passType);
            if (doesWork)
            {
                garageTypes.Add(passType);
            }
        }
        
        return garageTypes;
    }

    public static string ConvertParkingPassListToString(List<ParkingPassType> types)
    {
        string jsonString = "'[";
        for (int i = 0; i < types.Count; i++)
        {
            jsonString += "\"";
            ParkingPassType type = types[i];
            jsonString += type.ToString() + "\"";
            if (i != types.Count - 1)
            {
                jsonString += ",";
            }
        }
        return (jsonString += "]'");
        
        
    }
    
}