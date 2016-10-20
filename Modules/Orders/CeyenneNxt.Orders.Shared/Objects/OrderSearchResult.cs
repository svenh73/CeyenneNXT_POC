using System;

namespace CeyenneNxt.Orders.Shared.Objects
{
  public class OrderSearchResult
  {
    public int ID { get; set; }
    public string BackendID { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ChannelIdentifier { get; set; }
    public string OrderType { get; set; }
    public string OrderStatus { get; set; }
    public bool HoldOrder { get; set; }
  }
}