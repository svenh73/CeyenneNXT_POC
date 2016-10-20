using System;
using System.Collections.Generic;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class OrderUpdateMessage
  {
    public OrderUpdateMessage()
    {
      OrderUpdateMessageLines = new List<OrderUpdateMessageLine>();
    }

    public int ID { get; set; }


    public int OrderID { get; set; }

    public List<OrderUpdateMessageLine> OrderUpdateMessageLines { get; set; }

    public OrderStatusHistory OrderStatusHistory { get; set; }

    public int OrderStatusHistoryID { get; set; }

    public DateTime Timestamp { get; set; }

    public bool Processed { get; set; }
  }
}
