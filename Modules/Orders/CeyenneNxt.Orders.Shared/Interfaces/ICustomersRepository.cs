using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Objects;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface ICustomersRepository
  {
    Customer Create(Customer customer, SqlConnection connection, SqlTransaction transaction);
    Customer GetByBackendID(string backendID, SqlConnection connection, SqlTransaction transaction);
    Customer GetByID(int id, SqlConnection connection, SqlTransaction transaction);
    CustomerSearchResult GetCustomerWithAddressesAndOrders(int customerID, SqlConnection connection);
    SearchResult<CustomerSearchResult> Search(CustomerPagingFilter filter, SqlConnection connection);
  }
}