namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class OrderUpdateMessageLineDto
  {
    public OrderLineDto OrderLine { get; set; }

    public OrderLineStatusHistoryDto StatusHistory { get; set; }
  }
}
