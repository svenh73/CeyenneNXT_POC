using System;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class OrderLineStatusHistory
  {
    public int ID { get; set; }

    public OrderLineStatus Status { get; set; }

    public DateTime Timestamp { get; set; }

    public int? QuantityChanged { get; set; }

    public string Message { get; set; }

    public int OrderLineID { get; set; }
  }
}