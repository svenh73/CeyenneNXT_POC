using System.Collections.Generic;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class SearchResultDto<T>
  {
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalRows { get; set; }

    public List<T> Rows { get; set; }
  }
}