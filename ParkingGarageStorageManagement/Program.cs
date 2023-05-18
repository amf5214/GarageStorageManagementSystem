﻿// See https://aka.ms/new-console-template for more information

using ParkingGarageStorageManagement;

DbQueries query = new DbQueries(Constants.ConnDetails);
Garage homeBase = query.GetGarage(Constants.GarageId);
GateEvent gateEvent = homeBase.AddRemCar("FRALSP", true);
GateEvent gateEvent2 = homeBase.AddRemCar("FRALSP", false);
GateEvent gateEvent3 = homeBase.AddRemCar("TRAC&2", true);
GateEvent gateEvent4 = homeBase.AddRemCar("TRAC&2", false);
Console.WriteLine(gateEvent);
Console.WriteLine(gateEvent2);
Console.WriteLine(gateEvent3);
Console.WriteLine(gateEvent4);

Car car = new Car("Toyota", "4Runner", "Nightshade Green", "NGHTSH", 1, 1);
// car.CreateCar();

List<ParkingPassType> types = Garage.ConvertJsonStringToList("'[\"Visitor\",\"Commutor\"]'");
foreach(ParkingPassType type in types)
{
    Console.WriteLine(type);
}

Console.WriteLine(Garage.ConvertParkingPassListToString(types));



Console.ReadLine();
