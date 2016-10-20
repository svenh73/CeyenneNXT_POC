using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface ICountryRepository
  {
    int Create(Country addressCountry, SqlConnection connection, SqlTransaction transaction);
    int GetByCode(string code, SqlConnection connection, SqlTransaction transaction);
  }
}