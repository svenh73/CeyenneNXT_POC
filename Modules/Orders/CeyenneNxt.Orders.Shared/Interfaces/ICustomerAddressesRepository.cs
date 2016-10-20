using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface ICustomerAddressesRepository
  {
    int Create(CustomerAddress model, int customerID, SqlConnection connection, SqlTransaction transaction);
    CustomerAddress Get(int id, SqlConnection connection, SqlTransaction transaction);

    int GetByCustomerAndBackendID(int customerID, string backendID, SqlConnection connection,
      SqlTransaction transaction);
  }
}