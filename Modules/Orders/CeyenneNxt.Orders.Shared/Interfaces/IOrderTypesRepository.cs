using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderTypesRepository
  {
    int Create(string orderTypeName, SqlConnection connection, SqlTransaction transaction);
    IEnumerable<OrderType> GetAll(SqlConnection connection);
    int GetByName(string name, SqlConnection connection, SqlTransaction transaction);
  }
}