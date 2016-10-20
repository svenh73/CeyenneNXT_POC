using System.Collections.Generic;
using System.Linq;
using CeyenneNxt.Orders.Shared.Enums;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class OrderLine
  {
    public int ID { get; set; }

    public string ExternalOrderLineID { get; set; }

    public OrderLineQuantityUnit QuantityUnit { get; set; }

    public List<OrderLineAttributeValue> Attributes { get; set; }

    public IEnumerable<OrderLineStatusHistory> StatusHistories { get; set; }

    public int Quantity { get; set; }

    public int? QuantityShipped
    {
      get
      {
        return StatusHistories?.Where(s => s.Status.Code == OrderLineStatuses.Shipped).Sum(s => s.QuantityChanged.GetValueOrDefault());
      }
    }

    public string ExternalProductIdentifier { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal PriceTaxAmount { get; set; }
  }
}