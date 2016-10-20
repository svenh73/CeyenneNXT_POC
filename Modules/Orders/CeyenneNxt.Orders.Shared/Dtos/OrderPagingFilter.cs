using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class OrderPagingFilterDto : PagingFilter
  {
    public string BackendId { get; set; }

    public string OrderStatus { get; set; }

    public int? CustomerId { get; set; }

    public string CustomerBackendIDOrName { get; set; }

    public string Channel { get; set; }

    public int? TypeID { get; set; }
  }
}