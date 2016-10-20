using System.Collections.Generic;

namespace CeyenneNxt.Orders.Shared.Dtos
{
  public class CustomerSearchResultDto
  {
    public int ID { get; set; }
    public string BackendID { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Company { get; set; }
    public List<CustomerAddressSelectDto> Addresses { get; set; }
    public List<OrderSearchResultDto> Orders { get; set; }
  }
}