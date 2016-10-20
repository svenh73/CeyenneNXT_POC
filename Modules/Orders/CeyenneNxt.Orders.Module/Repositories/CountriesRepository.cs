using System.Data;
using System.Data.SqlClient;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class CountryRepository : BaseRepository, ICountryRepository
  {
    public CountryRepository() : base(SchemaConstants.Orders)
    {
    }

    public int GetByCode(string code, SqlConnection connection, SqlTransaction transaction)
    {
      var param = new DynamicParameters();
      param.Add("@Code", code);

      return GetItem<int>(param, Constants.StoredProcedures.Countries.GetByCode, connection, transaction);
    }

    public int Create(Country addressCountry, SqlConnection connection, SqlTransaction transaction)
    {
      var param = new DynamicParameters();
      param.Add("@Code", addressCountry.Code, DbType.String);
      param.Add("@Name", addressCountry.Name, DbType.String);
      param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

      return Execute(param, Constants.StoredProcedures.Countries.Insert, connection, transaction).Get<int>("ID");
    }


  }
}