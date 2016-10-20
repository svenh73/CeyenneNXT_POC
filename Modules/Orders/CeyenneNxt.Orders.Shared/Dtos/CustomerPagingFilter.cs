using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class CustomerPagingFilterDto : PagingFilter
  {
    public string Name { get; set; }

    public string Phone { get; set; }

    public string Company { get; set; }

    public string Email { get; set; }
  }
}