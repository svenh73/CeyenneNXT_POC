namespace CeyenneNxt.Orders.Shared.Objects
{
  public abstract class PagingFilter
  {
    public int? PageNumber { get; set; }

    public int? PageSize { get; set; }
  }
}