using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderQuantityUnitsRepository
  {
    int GetIDByCode(IOrderModuleSession session,string code);
    int Create(IOrderModuleSession session, OrderLineQuantityUnit quantityUnit);
  }
}