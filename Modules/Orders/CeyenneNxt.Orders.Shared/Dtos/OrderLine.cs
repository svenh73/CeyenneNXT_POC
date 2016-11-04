using System.Collections.Generic;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class OrderLineDto
  {
    public OrderLineDto()
    {
      AttributeValues = new List<AttributeValueDto>();
    }

    public int ID { get; set; }

    public string ExternalOrderLineID { get; set; }

    public string ExternalProductIdentifier { get; set; }

    public int Quantity { get; set; }

    public int? QuantityShipped { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal PriceTaxAmount { get; set; }

    public OrderLineQuantityUnitDto QuantityUnit { get; set; }

    public List<AttributeValueDto> AttributeValues { get; set; }

    public List<OrderLineStatusHistoryDto> StatusHistories { get; set; }
  }
}