using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Exceptions;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderStatusHistoryRepository : BaseRepository<OrderStatusHistory>, IOrderStatusHistoryRepository
  {
    public OrderStatusHistoryRepository() : base(Core.Constants.SchemaConstants.Orders)
    {
    }

    public int Create(IOrderModuleSession session,int orderID, int statusID, DateTime timestamp)
    {
      var p = new DynamicParameters();
      p.Add("@OrderID", orderID, DbType.Int32);
      p.Add("@StatusID", statusID, DbType.Int32);
      p.Add("@Timestamp", timestamp, DbType.DateTime);
      p.Add("@ID", direction: ParameterDirection.Output, dbType: DbType.Int32);

      var isStatusUnique = !GetStatusHistory(session,orderID, statusID).Any();

      if (isStatusUnique)
      {
        Execute(session,p, Constants.StoredProcedures.OrderStatus.Create);
      }
      else
      {
        throw new OrderStatusDuplicationException("There is such status for this order. An order cannot have duplication of statuses.");
      }

      return p.Get<int>("@ID");
    }

    public IEnumerable<OrderStatusHistory> GetStatusHistoryByOrderID(IOrderModuleSession session,int orderID)
    {
      if (orderID <= 0)
        throw new ArgumentException("orderID should be greater than 0", nameof(orderID));

      List<OrderStatusHistory> statusHistory;

      using (
        var statusHistoryMultiple =
          session.Connection.QueryMultiple(GetStoredProcedureName(Constants.StoredProcedures.OrderStatus.GetStatusHistoryByOrderID),
            new { OrderID = orderID }, commandType: CommandType.StoredProcedure))
      {
        statusHistory = statusHistoryMultiple.Read<OrderStatusHistory, OrderStatus, OrderStatusHistory>(
          (history, status) =>
          {
            history.Status = status;
            return history;
          }, splitOn: "ID").ToList();
      }

      return statusHistory;
    }

    private IEnumerable<OrderStatusHistory> GetStatusHistory(IOrderModuleSession session,int? orderID, int? statusID)
    {
      if (orderID <= 0 || statusID <= 0)
        throw new ArgumentException("If provided, orderID and statusID should be greater than 0");

      if (!orderID.HasValue && !statusID.HasValue)
        throw new ArgumentException("At least orderID or statusID should be provided");

      List<OrderStatusHistory> statusHistory;

      var p = new DynamicParameters();

      if (orderID.HasValue)
        p.Add("@OrderID", orderID.Value, DbType.Int32);

      if (statusID.HasValue)
        p.Add("@StatusID", statusID.Value, DbType.Int32);

      using (
        var statusHistoryMultiple =
          session.Connection.QueryMultiple(GetStoredProcedureName(Constants.StoredProcedures.OrderStatus.GetStatusHistory),
            p, commandType: CommandType.StoredProcedure, transaction: session.Transaction))
      {
        statusHistory = statusHistoryMultiple.Read<OrderStatusHistory, OrderStatus, OrderStatusHistory>(
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
