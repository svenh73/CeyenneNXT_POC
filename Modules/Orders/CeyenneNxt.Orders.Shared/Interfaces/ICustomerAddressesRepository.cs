using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface ICustomerAddressesRepository
  {
    int Create(IOrderModuleSession session,CustomerAddress model, int customerID);
    CustomerAddress Get(IOrderModuleSession session,int id);

    int GetByCustomerAndBackendID(IOrderModuleSession session,int customerID, string backendID);
  }
}