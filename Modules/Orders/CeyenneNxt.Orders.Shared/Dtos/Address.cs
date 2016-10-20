namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class AddressDto
  {
    public string BackendID { get; set; }

    public string Company { get; set; }

    public string Att { get; set; }

    public string Street { get; set; }

    public string HouseNumber { get; set; }

    public string HouseNumberExt { get; set; }

    public string ZIPCode { get; set; }

    public string City { get; set; }

    public CountryDto Country { get; set; }

    public AddressTypeDto Type { get; set; }
  }
}