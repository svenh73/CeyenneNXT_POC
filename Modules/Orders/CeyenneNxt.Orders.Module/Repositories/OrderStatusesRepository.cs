using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderStatusesRepository : BaseRepository, IOrderStatusesRepository
  {
    public OrderStatusesRepository() : base(SchemaConstants.Orders)
    {
    }
    public IEnumerable<OrderStatus> GetAll(SqlConnection connection)
    {
      return connection.Query<OrderStatus>(GetStoredProcedureName(Constants.StoredProcedures.OrderStatus.SelectAll),
        commandType: CommandType.StoredProcedure);
    }

    public int GetStatusIDByCode(string code, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@Code", code, DbType.String);

      return GetItem<int>(p, Constants.StoredProcedures.OrderStatus.GetIDByCode, connection, transaction);
    }
  }
}

