using System.Collections.Generic;

namespace CeyenneNxt.Orders.Shared.Objects
{
  public class CustomerSearchResult
  {
    public int ID { get; set; }
    public string BackendID { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Company { get; set; }
    public List<CustomerAddressSelect> Addresses { get; set; }
    public List<OrderSearchResult> Orders { get; set; }
  }
}