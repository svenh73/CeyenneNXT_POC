using System;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class OrderStatusHistoryDto
  {
    public OrderStatusDto Status { get; set; }

    public DateTime Timestamp { get; set; }
  }
}
