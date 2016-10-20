using System;
using System.Collections.Generic;
using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class Order : AuditEntity
  {
    public Order()
    {
      OrderAddresses = new List<OrderAddress>();
      OrderLines = new List<OrderLine>();
      History = new List<OrderStatusHistory>();
    }

    public int ID { get; set; }

    public Customer Customer { get; set; }

    public string ChannelIdentifier { get; set; }

    public Order ParentOrder { get; set; }

    public string BackendID { get; set; }

    public OrderType OrderType { get; set; }

    public List<OrderAttributeValue> Attributes { get; set; }

    public List<OrderStatusHistory> History { get; set; }

    public List<OrderLine> OrderLines { get; set; }

    public List<OrderNote> OrderNotes { get; set; }

    public List<OrderAddress> OrderAddresses { get; set; }

    public DateTime? DispatchedAt { get; set; }

    public bool HoldOrder { get; set; }

    public decimal Subtotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal ShippingCosts { get; set; }

    public decimal ShippingCostsTaxAmount { get; set; }

    public decimal Total { get; set; }
  }
}