using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Objects;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface ICustomersRepository
  {
    Customer Create(IOrderModuleSession session,Customer customer);
    Customer GetByBackendID(IOrderModuleSession session,string backendID);
    Customer GetByID(IOrderModuleSession session,int id);
    CustomerSearchResult GetCustomerWithAddressesAndOrders(IOrderModuleSession session,int customerID);
    SearchResult<CustomerSearchResult> Search(IOrderModuleSession session,CustomerPagingFilter filter);
  }
}