using System;

namespace CeyenneNxt.Orders.Shared.Dtos.Messages
{
  public class OrderHistoryUpdateDto
  {
    public int OrderID { get; set; }

    public string StatusCode { get; set; }

    public DateTime Timestamp { get; set; }
  }
}