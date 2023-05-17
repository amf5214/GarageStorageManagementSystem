namespace ParkingGarageStorageManagement;

public class Car
{
    /// <summary>
    /// This class represents the vehicles within the car garages and stores the characteristics of the vehicles.
    /// </summary>

    public int CarId { get; set; }
    public string CarMake { get; set; }
    public string CarModel { get; set; }
    public string CarColor { get; set; }
    public string CarLicensePlate { get; set; }
    public int UserId { get; set; }
    public int PlanId { get; set;  }

    public Car(int carId, string carMake, string carModel, string carColor, string carLicensePlate, int userId, int planId)
    {
        CarId = carId;
        CarMake = carMake;
        CarModel = carModel;
        CarColor = carColor;
        CarLicensePlate = carLicensePlate;
        UserId = userId;
        PlanId = planId;
    }

    public Car(string carMake, string carModel, string carColor, string carLicensePlate, int userId, int planId)
    {
        CarId = -1;
        CarMake = carMake;
        CarModel = carModel;
        CarColor = carColor;
        CarLicensePlate = carLicensePlate;
        UserId = userId;
        PlanId = planId;
    }

    public override string ToString()
    {
        string carId = CarId != -1 ? CarId + "" : "";
        return $"{carId} {CarColor} {CarMake} {CarModel}";
    }

    public Car CreateCar()
    {
        DbQueries query = new DbQueries(Constants.ConnDetails);
        return query.CreateCar(this);
    }
}

