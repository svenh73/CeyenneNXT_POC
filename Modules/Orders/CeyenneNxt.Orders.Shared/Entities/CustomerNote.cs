using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class CustomerNote: AuditEntity
  {
    public int ID { get; set; }

    public int CustomerID { get; set; }

    public int? UserID { get; set; }

    public string Subject { get; set; }

    public string Details { get; set; }
  }
}
