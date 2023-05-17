namespace ParkingGarageStorageManagement;

public class Address
{
    public int AddressId { get; set; }
    public string StreetAddress { get; set; }
    public string AddressCity { get; set; }
    public string AddressState { get; set; }
    public string AddressZipCode { get; set; }
    public string AddressCountry { get; set; }

    public Address(int addressId, string streetAddress, string addressCity, string addressState, string addressZipCode, string addressCountry)
    {
        AddressId = addressId;
        StreetAddress = streetAddress;
        AddressCity = addressCity;
        AddressState = addressState;
        AddressZipCode = addressZipCode;
        AddressCountry = addressCountry;
    }

    public Address(string streetAddress, string addressCity, string addressState, string addressZipCode, string addressCountry)
    {
        StreetAddress = streetAddress;
        AddressCity = addressCity;
        AddressState = addressState;
        AddressZipCode = addressZipCode;
        AddressCountry = addressCountry;
    }
    
}