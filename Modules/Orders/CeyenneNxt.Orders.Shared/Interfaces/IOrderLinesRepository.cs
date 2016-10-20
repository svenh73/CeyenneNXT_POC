using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderLinesRepository
  {
    int Create(OrderLine orderLine, int orderID, SqlConnection connection, SqlTransaction transaction);
    OrderLine GetByID(int id, SqlConnection connection, SqlTransaction transaction);
    OrderLine GetFullByID(int id, SqlConnection connection);
  }
}