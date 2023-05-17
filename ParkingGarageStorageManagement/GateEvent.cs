namespace ParkingGarageStorageManagement;

public class GateEvent
{
    public int EventId { get; set; }
    public int PlanId { get; set; }
    public string EventType { get; set; }
    public int CarId { get; set; }
    public int GarageId { get; set; }
    
    public DateTime EventDateTime { get; set; }

    public GateEvent(int eventId, int planId, string eventType, int carId, int garageId, DateTime eventDateTime)
    {
        EventId = eventId;
        PlanId = planId;
        EventType = eventType;
        CarId = carId;
        GarageId = garageId;
        EventDateTime = eventDateTime;
    }

    public GateEvent(int planId, string eventType, int carId, int garageId, DateTime eventDateTime)
    {
        PlanId = planId;
        EventType = eventType;
        CarId = carId;
        GarageId = garageId;
        EventDateTime = eventDateTime;
    }

    public override string ToString()
    {
        return $"PlanId:{PlanId}; CarId:{CarId}; EventType:{EventType}; GarageId:{GarageId}; EventDateTime:{EventDateTime.ToString("yyyy-MM-dd HH:mm")}";
    }
}