using System;
using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Orders.Shared.Objects
{
  public class Refund: AuditEntity
  {
    public int ID { get; set; }
    public int OrderID { get; set; }
    public int CurrencyID { get; set; }
    public int? ReturnCodeID { get; set; }
    public int? PaymentMethodID { get; set; }
    public decimal? Amount { get; set; }
    public string Description { get; set; }
    public string Invoice { get; set; }
    public DateTime Timestamp { get; set; }
  }
}
