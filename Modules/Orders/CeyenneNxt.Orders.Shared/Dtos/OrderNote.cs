using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class OrderNoteDto : AuditEntity
  {
    public int ID { get; set; }

    public int OrderID { get; set; }

    public int? UserID { get; set; }

    public string Subject { get; set; }

    public string Details { get; set; }
  }
}
