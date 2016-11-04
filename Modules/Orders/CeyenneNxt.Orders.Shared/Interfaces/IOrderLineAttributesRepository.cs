using System.Data.SqlClient;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderLineAttributesRepository
  {
    int Create(IOrderModuleSession session,string code, string name);
    void CreateValue(IOrderModuleSession session,int orderID, int attributeID, string value);
    int GetIDByCode(IOrderModuleSession session,string code);
  }
}