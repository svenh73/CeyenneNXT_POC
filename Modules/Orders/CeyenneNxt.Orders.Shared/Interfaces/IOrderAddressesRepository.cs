using System.Data.SqlClient;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderAddressesRepository
  {
    void Create(IOrderModuleSession session,int orderID, int customerAddressID, int addressTypeID);
  }
}