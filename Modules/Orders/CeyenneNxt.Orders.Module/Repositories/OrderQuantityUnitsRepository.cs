using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderQuantityUnitsRepository : BaseRepository, IOrderQuantityUnitsRepository
  {
    public OrderQuantityUnitsRepository() : base(SchemaConstants.Orders)
    {
    }

    public int GetIDByCode(string code, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@Code", code, DbType.String);

      return
        connection.Query<int>(GetStoredProcedureName(Constants.StoredProcedures.OrderQuantityUnit.GetIDByCode), p,
          commandType: CommandType.StoredProcedure, transaction: transaction).FirstOrDefault();
    }

    public int Create(OrderLineQuantityUnit quantityUnit, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@Code", quantityUnit.Code, dbType: DbType.String);
      p.Add("@Name", quantityUnit.Name, dbType: DbType.String);
      p.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

      connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderQuantityUnit.Create), p, transaction,
        commandType: CommandType.StoredProcedure);

      return p.Get<int>("ID");
    }
  }
}