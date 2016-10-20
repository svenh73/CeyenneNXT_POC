using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface ICustomerAddressTypesRepository
  {
    int Create(CustomerAddressType customerAddressType, SqlConnection connection, SqlTransaction transaction);
    CustomerAddressType GetByID(int id, SqlConnection connection, SqlTransaction transaction);
    int GetIDByCode(string code, SqlConnection connection, SqlTransaction transaction);
  }
}