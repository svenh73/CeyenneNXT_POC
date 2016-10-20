using System.Data.SqlClient;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderAddressesRepository
  {
    void Create(int orderID, int customerAddressID, int addressTypeID, SqlConnection connection, SqlTransaction transaction);
  }
}