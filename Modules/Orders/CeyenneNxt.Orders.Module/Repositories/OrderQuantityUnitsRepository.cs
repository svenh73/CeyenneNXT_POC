using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderQuantityUnitsRepository : BaseRepository<OrderLineQuantityUnit>, IOrderQuantityUnitsRepository
  {
    public OrderQuantityUnitsRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }

    public int GetIDByCode(IOrderModuleSession session,string code)
    {
      var p = new DynamicParameters();
      p.Add("@Code", code, DbType.String);

      return
        session.Connection.Query<int>(GetStoredProcedureName(Constants.StoredProcedures.OrderQuantityUnit.GetIDByCode), p,
          commandType: CommandType.StoredProcedure, transaction: session.Transaction).FirstOrDefault();
    }

    public int Create(IOrderModuleSession session, OrderLineQuantityUnit quantityUnit)
    {
      var p = new DynamicParameters();
      p.Add("@Code", quantityUnit.Code, dbType: DbType.String);
      p.Add("@Name", quantityUnit.Name, dbType: DbType.String);
      p.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

      session.Connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderQuantityUnit.Create), p, session.Transaction,
        commandType: CommandType.StoredProcedure);

      return p.Get<int>("ID");
    }
  }
}