using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface ICustomerAddressTypesRepository
  {
    int Create(IOrderModuleSession session,CustomerAddressType customerAddressType);
    CustomerAddressType GetByID(IOrderModuleSession session,int id);
    int GetIDByCode(IOrderModuleSession session,string code);
  }
}