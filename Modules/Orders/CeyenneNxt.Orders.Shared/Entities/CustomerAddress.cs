namespace CeyenneNxt.Orders.Shared.Entities
{
  public class CustomerAddress
  {
    public int ID { get; set; }

    public string BackendID { get; set; }

    public string Company { get; set; }

    public string Att { get; set; }

    public string Street { get; set; }

    public string HouseNumber { get; set; }

    public string HouseNumberExt { get; set; }

    public string ZIPCode { get; set; }

    public string City { get; set; }

    public Country Country { get; set; }

    public string Remark { get; set; }
  }
}