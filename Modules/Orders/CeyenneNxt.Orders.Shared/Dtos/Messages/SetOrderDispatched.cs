using System;

namespace CeyenneNxt.Orders.Shared.Dtos.Messages
{
  public class SetOrderDispatchedDto
  {
    public int OrderID { get; set; }

    public DateTime DispatchedAt { get; set; }
  }
}