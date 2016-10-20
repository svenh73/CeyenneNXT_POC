namespace CeyenneNxt.Orders.Shared.Entities
{
  public class OrderLineStatus
  {
    public int ID { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public bool QuantityRequired { get; set; }
  }
}