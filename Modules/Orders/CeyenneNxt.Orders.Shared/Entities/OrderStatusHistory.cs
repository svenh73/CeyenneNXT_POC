using System;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class OrderStatusHistory
  {
    public OrderStatus Status { get; set; }

    public DateTime Timestamp { get; set; }
  }
}