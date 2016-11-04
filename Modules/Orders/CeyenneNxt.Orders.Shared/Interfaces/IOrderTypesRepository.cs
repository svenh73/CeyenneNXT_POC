using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderTypesRepository
  {
    int Create(IOrderModuleSession session,string orderTypeName);
    IEnumerable<OrderType> GetAll(IOrderModuleSession session);
    int GetByName(IOrderModuleSession session,string name);
  }
}