namespace CeyenneNxt.Orders.Shared.Entities
{
  public class OrderAddress
  {
    public CustomerAddressType Type { get; set; }

    public CustomerAddress Address { get; set; }
  }
}