namespace CeyenneNxt.Orders.Shared.Objects
{
  public class CustomerPagingFilter : PagingFilter
  {
    public string Name { get; set; }

    public string Phone { get; set; }

    public string Company { get; set; }

    public string Email { get; set; }
  }
}