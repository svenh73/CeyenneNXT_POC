using System.Data;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class CountryRepository : BaseRepository<Country>, ICountryRepository
  {
    public CountryRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }

    public int GetByCode(IOrderModuleSession session,string code)
    {
      var param = new DynamicParameters();
      param.Add("@Code", code);

      return GetItem<int>(session,param, Constants.StoredProcedures.Countries.GetByCode);
    }

    public int Create(IOrderModuleSession session,Country addressCountry)
    {
      var param = new DynamicParameters();
      param.Add("@Code", addressCountry.Code, DbType.String);
      param.Add("@Name", addressCountry.Name, DbType.String);
      param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

      return Execute(session,param, Constants.StoredProcedures.Countries.Insert).Get<int>("ID");
    }


  }
}