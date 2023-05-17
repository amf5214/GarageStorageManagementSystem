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

    public Garage(int garageId, string garageName, int addressId, int totalSpaces, int currentOccupants, int planId)
    {
        GarageId = garageId;
        GarageName = garageName;
        AddressId = addressId;
        TotalSpaces = totalSpaces;
        CurrentOccupants = currentOccupants;
        PlanId = planId;
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
}