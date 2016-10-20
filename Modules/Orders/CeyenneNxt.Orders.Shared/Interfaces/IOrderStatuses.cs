using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderStatusesRepository
  {
    IEnumerable<OrderStatus> GetAll(SqlConnection connection);
    int GetStatusIDByCode(string code, SqlConnection connection, SqlTransaction transaction);
  }
}