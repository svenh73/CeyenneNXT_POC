using System.Collections.Generic;

namespace CeyenneNxt.Orders.Shared.Objects
{
  public class SearchResult<T>
  {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRows { get; set; }
    public List<T> Rows { get; set; }
  }
}