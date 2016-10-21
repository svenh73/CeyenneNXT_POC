using System;
using System.ComponentModel.DataAnnotations;

namespace CeyenneNxt.Orders.Shared.Dtos.Messages
{
  public class OrderLineHistoryUpdateDto
  {
    public int OrderLineID { get; set; }

    [Required]
    public string StatusCode { get; set; }

    [Range(1, 2147483647)]
    public int? QuantityChanged { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public string Message { get; set; }
  }
}
