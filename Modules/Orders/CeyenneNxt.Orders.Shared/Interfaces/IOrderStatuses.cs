using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderStatusesRepository
  {
    IEnumerable<OrderStatus> GetAll(IOrderModuleSession session);
    int GetStatusIDByCode(IOrderModuleSession session,string code);
  }
}