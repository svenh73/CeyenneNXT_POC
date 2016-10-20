namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class OrderLineStatusDto
  {
    public int ID { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public bool QuantityRequired { get; set; }
  }
}
