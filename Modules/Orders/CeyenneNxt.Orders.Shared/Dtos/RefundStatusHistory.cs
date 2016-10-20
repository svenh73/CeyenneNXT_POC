using System;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class RefundStatusHistoryDto
  {
    public int ID { get; set; }
    public int RefundID { get; set; }
    public int RefundStatusID { get; set; }
    public RefundStatusDto RefundStatus { get; set; }
    public DateTime Timestamp { get; set; }
  }
}
