using System;
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
  public class OrderLinesRepository : BaseRepository, IOrderLinesRepository
  {

    public OrderLinesRepository() : base(SchemaConstants.Orders)
    {
    }

    public int Create(OrderLine orderLine, int orderID, SqlConnection connection, SqlTransaction transaction)
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

      connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderLines.Create), p, transaction,
        commandType: CommandType.StoredProcedure);

      return p.Get<int>("ID");
    }

    public OrderLine GetByID(int id, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@ID", id, DbType.Int32);

      return connection.Query<OrderLine>(GetStoredProcedureName(Constants.StoredProcedures.OrderLines.GetByID), p,
        transaction: transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    public OrderLine GetFullByID(int id, SqlConnection connection)
    {
      if (id <= 0)
        throw new ArgumentException("id must be greater than 0", nameof(id));

      using (var orderMultiple = connection.QueryMultiple(GetStoredProcedureName(Constants.StoredProcedures.OrderLines.GetByIdDetails), new { ID = id }, commandType: CommandType.StoredProcedure))
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