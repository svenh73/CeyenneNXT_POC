using System.Data.SqlClient;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderLineAttributesRepository
  {
    int Create(string code, string name, SqlConnection connection, SqlTransaction transaction);
    void CreateValue(int orderID, int attributeID, string value, SqlConnection connection, SqlTransaction transaction);
    int GetIDByCode(string code, SqlConnection connection, SqlTransaction transaction);
  }
}