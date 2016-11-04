using System;
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
  public class OrderLinesRepository : BaseRepository<OrderLine>, IOrderLinesRepository
  {

    public OrderLinesRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }

    public int Create(IOrderModuleSession session,OrderLine orderLine, int orderID)
    {
      var p = new DynamicParameters();
      p.Add("@ExternalOrderLineID", orderLine.ExternalOrderLineID, dbType: DbType.String);
      p.Add("@ExternalProductIdentifier", orderLine.ExternalProductIdentifier, dbType: DbType.String);
      p.Add("@OrderQuantityUnitID", orderLine.QuantityUnit.ID, dbType: DbType.Int32);
      p.Add("@OrderID", orderID, DbType.Int32);
      p.Add("@Quantity", orderLine.Quantity, DbType.Int32);
      p.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
      p.Add("@TotalPrice", orderLine.TotalPrice, DbType.Decimal);
      p.Add("@UnitPrice", orderLine.UnitPrice, DbType.Decimal);
      p.Add("@PriceTaxAmount", orderLine.PriceTaxAmount, DbType.Decimal);

      session.Connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderLines.Create), p, session.Transaction,
        commandType: CommandType.StoredProcedure);

      return p.Get<int>("ID");
    }

    public OrderLine GetByID(IOrderModuleSession session,int id)
    {
      var p = new DynamicParameters();
      p.Add("@ID", id, DbType.Int32);

      return session.Connection.Query<OrderLine>(GetStoredProcedureName(Constants.StoredProcedures.OrderLines.GetByID), p,
        transaction: session.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    public OrderLine GetFullByID(IOrderModuleSession session,int id)
    {
      if (id <= 0)
        throw new ArgumentException("id must be greater than 0", nameof(id));

      using (var orderMultiple = session.Connection.QueryMultiple(GetStoredProcedureName(Constants.StoredProcedures.OrderLines.GetByIdDetails), new { ID = id }, commandType: CommandType.StoredProcedure))
      {
        var orderLine = orderMultiple.Read<OrderLine>().FirstOrDefault();

        if (orderLine == null)
        {
          return null;
        }

        orderLine.Attributes = orderMultiple.Read<OrderLineAttributeValue, OrderLineAttribute, OrderLineAttributeValue>((val, attr) =>
          {
            val.Attribute = attr;
            return val;
          }, splitOn: "ID"
        ).ToList();

        orderLine.StatusHistories = orderMultiple.Read<OrderLineStatusHistory, OrderLineStatus, OrderLineStatusHistory>(
          (statusHistory, status) =>
          {
            statusHistory.Status = status;
            return statusHistory;
          }, splitOn: "ID"
        ).ToList();

        return orderLine;
      }
    }

  }
}