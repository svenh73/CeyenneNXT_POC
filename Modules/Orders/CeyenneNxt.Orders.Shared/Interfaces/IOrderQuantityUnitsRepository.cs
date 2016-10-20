using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderQuantityUnitsRepository
  {
    int GetIDByCode(string code, SqlConnection connection, SqlTransaction transaction);
    int Create(OrderLineQuantityUnit quantityUnit, SqlConnection connection, SqlTransaction transaction);
  }
}