using System;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class OrderNoteAction
  {
    public int ID { get; set; }

    public string UserIdentifier { get; set; }

    public DateTime? ReminderDateTime { get; set; }

    public DateTime? DueDateTime { get; set; }

    public bool? ReminderProcessed { get; set; }
  }
}