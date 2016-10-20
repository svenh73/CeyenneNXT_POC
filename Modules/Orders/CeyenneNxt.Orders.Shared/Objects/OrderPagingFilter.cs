namespace CeyenneNxt.Orders.Shared.Objects
{
  public class OrderPagingFilter : PagingFilter
  {
    public string BackendId { get; set; }

    public string OrderStatus { get; set; }

    public int? CustomerId { get; set; }

    public string CustomerBackendIdOrName { get; set; }

    public string Channel { get; set; }

    public int? TypeID { get; set; }
  }
}