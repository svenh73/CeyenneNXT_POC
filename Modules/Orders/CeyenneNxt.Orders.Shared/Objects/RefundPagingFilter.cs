namespace CeyenneNxt.Orders.Shared.Objects
{
  public class RefundPagingFilter: PagingFilter
  {
    public int? RefundStatusID { get; set; }
    public string SearchText { get; set; }
  }
}
