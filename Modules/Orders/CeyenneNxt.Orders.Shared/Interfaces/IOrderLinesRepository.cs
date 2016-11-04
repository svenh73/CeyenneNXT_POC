using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderLinesRepository
  {
    int Create(IOrderModuleSession session,OrderLine orderLine, int orderID);
    OrderLine GetByID(IOrderModuleSession session,int id);
    OrderLine GetFullByID(IOrderModuleSession session,int id);
  }
}