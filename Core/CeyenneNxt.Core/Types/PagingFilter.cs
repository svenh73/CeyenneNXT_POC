namespace CeyenneNxt.Core.Types
{
  public abstract class PagingFilter
  {
    public int? PageNumber { get; set; }

    public int? PageSize { get; set; }
  }
}
