using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface ICountryRepository
  {
    int Create(IOrderModuleSession session,Country addressCountry);
    int GetByCode(IOrderModuleSession session,string code);
  }
}