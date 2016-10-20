using System;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class OrderLineStatusHistoryDto
  {
    public OrderLineStatusDto Status { get; set; }

    public DateTime Timestamp { get; set; }

    public int? QuantityChanged { get; set; }

    public string Message { get; set; }
  }
}
