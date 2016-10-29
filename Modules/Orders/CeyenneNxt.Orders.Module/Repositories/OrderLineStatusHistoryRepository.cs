using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderLineStatusHistoryRepository : BaseRepository, IOrderLineStatusHistoryRepository
  {
    public OrderLineStatusHistoryRepository() : base(SchemaConstants.Orders)
    {
      
    }

    public int Create(int orderLineID, int statusID, int? quantityChanged, DateTime timestamp, string message, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@OrderLineID", orderLineID, DbType.Int32);
      p.Add("@StatusID", statusID, DbType.Int32);
      p.Add("@QuantityChanged", quantityChanged, DbType.Int32);
      p.Add("@Timestamp", timestamp, DbType.DateTime);
      p.Add("@Message", message, DbType.String);
      p.Add("@ID", direction: ParameterDirection.Output, dbType: DbType.Int32);

      Execute(p, Constants.StoredProcedures.OrderLineStatusHistory.CreateStatusHistory, connection, transaction);

      return p.Get<int>("@ID");
    }

    public int GetStatusIDByCode(string code, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@Code", code, DbType.String);

      return GetItem<int>(p, Constants.StoredProcedures.OrderLineStatus.GetIDByCode, connection, transaction);
    }

    public IEnumerable<OrderLineStatusHistory> GetStatusHistoryByOrderLineID(int orderLineID, SqlConnection connection)
    {
      if (orderLineID <= 0)
        throw new ArgumentException("orderLineID should be greater than 0", nameof(orderLineID));

      List<OrderLineStatusHistory> statusHistory;

      using (
        var statusHistoryMultiple =
          connection.QueryMultiple(GetStoredProcedureName(Constants.StoredProcedures.OrderLineStatusHistory.GetStatusHistoryByOrderLineID),
            new { OrderLineID = orderLineID }, commandType: CommandType.StoredProcedure))
      {
        statusHistory = statusHistoryMultiple.Read<OrderLineStatusHistory, OrderLineStatus, OrderLineStatusHistory>(
          (history, status) =>
          {
            history.Status = status;
            return history;
          }, splitOn: "ID").ToList();
      }

      return statusHistory;
    }
  }
}
