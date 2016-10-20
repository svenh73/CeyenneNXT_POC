using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderAttributesRepository
  {
    int Create(OrderAttribute attribute, SqlConnection connection, SqlTransaction transaction);
    void CreateValue(int orderID, int attributeID, string value, SqlConnection connection, SqlTransaction transaction);
    int GetIDByCode(string code, SqlConnection connection, SqlTransaction transaction);
  }
}