using System.Collections.Generic;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class Customer
  {
    public int ID { get; set; }

    public string BackendID { get; set; }

    public string FullName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Company { get; set; }

    public IEnumerable<CustomerCommunication> CommunicationTypes { get; set; }

    public IEnumerable<CustomerAddress> Addresses { get; set; }

    public IEnumerable<Order> Orders { get; set; }
  }
}