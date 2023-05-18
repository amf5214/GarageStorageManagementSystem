using MySql.Data.MySqlClient;
using MySqlConnector.Logging;

namespace ParkingGarageStorageManagement;

public class DbQueries : DbConnect
{
    public DbQueries(DBConnDetails connDetails) : base(connDetails)
    { 
        
    }

    public Car GetCarFromLicensePlate(string licensePlate)
    {
        if (OpenConnection())
        {
            string query = $"select * from Car where car_license_plate=\"{licensePlate}\";";
            List<Car> cars = new List<Car>();
            MySqlCommand command = new MySqlCommand(query, Connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                cars.Add(new Car(int.Parse(reader["car_id"] + ""), reader["car_make"] + "", reader["car_model"] + "", reader["car_color"] + "", reader["car_license_plate"] + "", int.Parse(reader["user_id"] + ""), int.Parse(reader["plan_id"] + "")));
            }
            CloseConnection();
            if (cars.Count > 0)
            {
                return cars[0];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public Garage GetGarage(int garageId)
    {
        if (OpenConnection())
        {
            string query = $"select * from Garage where garage_id=\"{garageId}\";";
            List<Garage> garages = new List<Garage>();
            MySqlCommand command = new MySqlCommand(query, Connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string readerOutput = reader["allowed_user_types"] + "";
                List<ParkingPassType> types = Garage.ConvertJsonStringToList(readerOutput);
                garages.Add(new Garage(int.Parse(reader["garage_id"] + ""), reader["garage_name"] + "", int.Parse(reader["address_id"] + ""), int.Parse(reader["total_spaces"] + ""), int.Parse(reader["current_occupants"] + ""), int.Parse(reader["plan_id"] + ""), types));
            }
            CloseConnection();
            if (garages.Count > 0)
            {
                return garages[0];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public GateEvent CreateGateEvent(GateEvent gateEvent)
    {
        if (OpenConnection())
        {
            string query = $"insert into GateEvent (plan_id, event_type, car_id, garage_id, event_datetime) values ({Constants.PlanId}, \"{gateEvent.EventType.ToString()}\", {gateEvent.CarId}, {gateEvent.GarageId}, \"{gateEvent.EventDateTime.ToString("yyyy-MM-dd HH:mm")}\");";
            MySqlCommand command = new MySqlCommand(query, Connection);
            command.ExecuteNonQuery();
            long index = command.LastInsertedId;
            gateEvent.EventId = int.Parse(index + "");
            CloseConnection();
            return gateEvent;

        }
        else
        {
            return null;
        }
    }

    public bool IncDecNumberOccupants(int garageId, bool isIncrement)
    {
        Garage garage = GetGarage(garageId);
        if (OpenConnection())
        {
            int newOccupantValue = isIncrement ? garage.CurrentOccupants + 1 : garage.CurrentOccupants - 1;
            string query = $"update Garage set current_occupants = {newOccupantValue} where garage_id = {garageId};";
            MySqlCommand command = new MySqlCommand(query, Connection);
            int returnValue = command.ExecuteNonQuery();
            CloseConnection();
            if (returnValue == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public Car CreateCar(Car car)
    {
        if (OpenConnection())
        {
            string query = $"insert into Car (car_make, car_model, car_color, car_license_plate, user_id, plan_id) values (\"{car.CarMake}\", \"{car.CarModel}\", \"{car.CarColor}\",\"{car.CarLicensePlate}\", {car.UserId}, {car.PlanId});";
            MySqlCommand command = new MySqlCommand(query, Connection);
            command.ExecuteNonQuery();
            long index = command.LastInsertedId;
            car.CarId = int.Parse(index + "");
            CloseConnection();
            return car;
        }
        else
        {
            return null;
        }
    }
    
    public ParkingPass CreateParkingPass(ParkingPass pass)
    {
        if (OpenConnection())
        {
            string query = $"insert into ParkingPass (pass_type, pass_user_id, pass_start_date, pass_expiration_date) values (\"{pass.Type.ToString()}\", {pass.UserId}, \"{pass.PassStartDate.ToString("yyyy-MM-dd")}\", \"{pass.PassExpirationDate.ToString("yyyy-MM-dd")}\");";
            MySqlCommand command = new MySqlCommand(query, Connection);
            command.ExecuteNonQuery();
            long index = command.LastInsertedId;
            pass.PassId = int.Parse(index + "");
            CloseConnection();
            return pass;
        }
        else
        {
            return null;
        }
    }

    public ParkingPass GetUserParkingPass(int userId)
    {
        if (OpenConnection())
        {
            string query = $"select * from ParkingPass where pass_user_id={userId} limit 1;";
            List<ParkingPass> passes = new List<ParkingPass>();
            MySqlCommand command = new MySqlCommand(query, Connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ParkingPassType type;
                DateTime startDate;
                DateTime endDate;
                bool didWork = ParkingPassType.TryParse(reader["pass_type"] + "", out type);
                bool didWork2 = DateTime.TryParse(reader["pass_start_date"] + "", out startDate);
                bool didWork3 = DateTime.TryParse(reader["pass_expiration_date"] + "", out endDate);
                if (didWork & didWork2 & didWork3)
                {
                    passes.Add(
                        new ParkingPass(int.Parse(reader["pass_id"] + ""), type, int.Parse(reader["pass_user_id"] + ""),
                            DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate)));
                }
            }
            CloseConnection();
            if (passes.Count > 0)
            {
                return passes[0];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public bool CheckIfUserIsAllowed(int userId, Garage garage)
    {
        ParkingPass pass = GetUserParkingPass(userId);
        return garage.AllowedUserTypes.Contains(pass.Type);
    }
    

}