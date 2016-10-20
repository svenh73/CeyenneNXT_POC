using System;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class RefundStatusHistory
  {
    public int ID { get; set; }
    public int RefundID { get; set; }
    public int RefundStatusID { get; set; }
    public RefundStatus RefundStatus { get; set; }
    public DateTime Timestamp { get; set; }
  }
}
