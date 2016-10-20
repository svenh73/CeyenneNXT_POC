namespace CeyenneNxt.Orders.Shared.Entities
{
  public class OrderUpdateMessageLine
  {
    public OrderLine OrderLine { get; set; }

    public OrderLineStatusHistory OrderLineStatusHistory { get; set; }
  }
}
