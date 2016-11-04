using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderAttributesRepository
  {
    int Create(IOrderModuleSession session,OrderAttribute attribute);
    void CreateValue(IOrderModuleSession session,int orderID, int attributeID, string value);
    int GetIDByCode(IOrderModuleSession session,string code);
  }
}