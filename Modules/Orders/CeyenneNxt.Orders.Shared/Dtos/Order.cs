using System;
using System.Collections.Generic;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class OrderDto
  {
    public OrderDto()
    {
      AttributeValues = new List<AttributeValueDto>();
      Addresses = new List<AddressDto>();
      OrderLines = new List<OrderLineDto>();
      History = new List<OrderStatusHistoryDto>();
    }

    public int ID { get; set; }

    public string BackendID { get; set; }

    public string ChannelIdentifier { get; set; }

    public string OrderType { get; set; }

    public List<OrderLineDto> OrderLines { get; set; }

    public CustomerDto Customer { get; set; }

    public List<AddressDto> Addresses { get; set; }

    public List<AttributeValueDto> AttributeValues { get; set; }

    public List<OrderStatusHistoryDto> History { get; set; }

    public bool HoldOrder { get; set; }

    public decimal Subtotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal ShippingCosts { get; set; }

    public decimal ShippingCostsTaxAmount { get; set; }

    public decimal Total { get; set; }

    public DateTime CreatedAt { get; set; }
  }
}