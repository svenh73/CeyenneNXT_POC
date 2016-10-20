using System;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class OrderNoteSearchResultDto
  {
    public string Subject { get; set; }
    public string Details { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
