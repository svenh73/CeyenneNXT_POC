using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class RefundPagingFilterDto : PagingFilter
  {
    public int? RefundStatusID { get; set; }
    public string SearchText { get; set; }
  }
}
