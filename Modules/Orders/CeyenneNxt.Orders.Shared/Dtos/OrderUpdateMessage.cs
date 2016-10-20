using System;
using System.Collections.Generic;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class OrderUpdateMessageDto
  {
    public int ID { get; set; }

    public int OrderID { get; set; }

    public List<OrderUpdateMessageLineDto> OrderUpdateMessageLines { get; set; }

    public OrderStatusHistoryDto OrderStatusHistory { get; set; }

    public int OrderStatusHistoryID { get; set; }

    public DateTime Timestamp { get; set; }

    public bool Processed { get; set; }
  }
}
