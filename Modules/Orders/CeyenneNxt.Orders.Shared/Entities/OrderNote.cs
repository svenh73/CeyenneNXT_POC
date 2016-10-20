using System.Collections.Generic;
using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class OrderNote : AuditEntity
  {
    public int OrderID { get; set; }
    public int? UserID { get; set; }
    public string Subject { get; set; }

    public string Details { get; set; }

    public IEnumerable<OrderNoteAction> Actions { get; set; }
  }
}