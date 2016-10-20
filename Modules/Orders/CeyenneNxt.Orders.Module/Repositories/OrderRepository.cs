using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNxt.Orders.Shared.Objects;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;
using Microsoft.Practices.ServiceLocation;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderRepository : BaseRepository, IOrderRepository
  {
    public OrderRepository() : base(SchemaConstants.Orders) { }

    public int GetIDByBackendID(string backendID, SqlConnection connection)
    {
      var p = new DynamicParameters();

      p.Add("@BackendID", backendID);

      return connection.Query<int>(GetStoredProcedureName(Constants.StoredProcedures.Orders.GetIDByBackendID), p, commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    public int GetIDByBackendID(string backendID, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();

      p.Add("@BackendID", backendID);

      return
        connection.Query<int>(GetStoredProcedureName(Constants.StoredProcedures.Orders.GetIDByBackendID), p,
          transaction: transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
    }


    public int Create(Order model, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();

      p.Add("@CustomerID", model.Customer.ID);
      p.Add("@ChannelIdentifier", model.ChannelIdentifier);
      p.Add("@ParentOrderID", model.ParentOrder?.ID);
      p.Add("@BackendID", model.BackendID);
      p.Add("@OrderTypeID", model.OrderType.ID);
      p.Add("@CreatedAt", DateTime.Now.ToUniversalTime());
      p.Add("@ID", DbType.Int32, direction: ParameterDirection.Output);

      connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.Orders.Create), p, transaction,
        commandType: CommandType.StoredProcedure);
      return p.Get<int>("ID");
    }

    public Order GetFullByID(int id, SqlConnection connection)
    {
      if (id <= 0)
        throw new NotSupportedException("Seriously? OrderID > 0");

      Order order;


      using (var orderMultiple = connection.QueryMultiple(GetStoredProcedureName(Constants.StoredProcedures.Orders.GetByIdDetails), new { ID = id }, commandType: CommandType.StoredProcedure))
      {
        order = orderMultiple.Read<Order>().FirstOrDefault();

        if (order == null)
        {
          return null;
        }

        order.OrderLines = orderMultiple.Read<OrderLine>().ToList();

        order.OrderType = orderMultiple.Read<OrderType>().FirstOrDefault();

        order.OrderAddresses = orderMultiple.Read<OrderAddress, CustomerAddress, CustomerAddressType, Country, OrderAddress>(
            (oa, ca, cat, country) =>
            {
              oa.Type = cat;
              oa.Address = ca;
              oa.Address.Country = country;
              return oa;
            }, splitOn: "ID").ToList();

        order.Attributes = orderMultiple.Read<OrderAttributeValue, OrderAttribute, OrderAttributeValue>((val, attr) =>
        {
          val.Attribute = attr;
          return val;
        }, "ID"
        ).ToList();




        order.History = orderMultiple.Read<OrderStatusHistory, OrderStatus, OrderStatusHistory>(
          (statusHistory, status) =>
          {
            statusHistory.Status = status;
            return statusHistory;
          }, splitOn: "ID").ToList();

        order.Customer = orderMultiple.Read<Customer>().FirstOrDefault();
      }

      foreach (var orderLine in order.OrderLines)
      {
        var p = new DynamicParameters();
        p.Add("OrderLineID", orderLine.ID, dbType: DbType.Int32);
        orderLine.Attributes = connection.Query<OrderLineAttributeValue, OrderLineAttribute, OrderLineAttributeValue>(GetStoredProcedureName(Constants.StoredProcedures.OrderLineAttributes.GetAttributes), (val, attr) =>
        {
          val.Attribute = attr;
          return val;
        }, p, splitOn: "ID", commandType: CommandType.StoredProcedure
      ).ToList();
      }

      IOrderLineStatusHistoryRepository orderLineStatusHistoryRepository = ServiceLocator.Current.GetInstance<IOrderLineStatusHistoryRepository>();
      if (orderLineStatusHistoryRepository == null)
      {
        throw new Exception("Dependency 'IOrderLineStatusHistoryRepository' not resolved");
      }

      //TODO: See if it is possible to get it the same way as other information is get (within the procedure)
      foreach (var item in order.OrderLines)
      {
        item.StatusHistories = orderLineStatusHistoryRepository.GetStatusHistoryByOrderLineID(item.ID, connection);
      }

      return order;
    }

    public Order GetByID(int orderID, SqlConnection connection)
    {
      var p = new DynamicParameters();
      p.Add("@ID", orderID, DbType.Int32);

      return connection.Query<Order>(GetStoredProcedureName(Constants.StoredProcedures.Orders.GetByID), p,
        commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    public DashboardData GetDashboardData(SqlConnection connection)
    {
      var dashboardData = new DashboardData();
      var parameters = new DynamicParameters();
      parameters.Add("@NewOrdersCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
      parameters.Add("@InProcessOrdersCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
      dashboardData.DayCounts = connection.Query<DayCount>(GetStoredProcedureName(Constants.StoredProcedures.Orders.GetDashboardData), parameters, commandType: CommandType.StoredProcedure).ToList();
      dashboardData.NewOrdersCount = parameters.Get<int>("@NewOrdersCount");
      dashboardData.InProcessOrdersCount = parameters.Get<int>("@InProcessOrdersCount");

      return dashboardData;
    }

    public SearchResult<OrderSearchResult> Search(OrderPagingFilter filter, SqlConnection connection)
    {
      if (!filter.PageSize.HasValue)
      {
        filter.PageSize = Constants.DefaultPageSize;
      }
      if (!filter.PageNumber.HasValue)
      {
        filter.PageNumber = 1;
      }

      var parameters = new DynamicParameters();
      parameters.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);
      parameters.Add("@PageNumber", filter.PageNumber.Value);
      parameters.Add("@PageSize", filter.PageSize.Value);

      if (!string.IsNullOrWhiteSpace(filter.BackendId))
      {
        parameters.Add("@BackendId", filter.BackendId);
      }
      if (!string.IsNullOrWhiteSpace(filter.OrderStatus))
      {
        parameters.Add("@OrderStatus", filter.OrderStatus);
      }
      if (filter.CustomerId != null)
      {
        parameters.Add("@CustomerId", filter.CustomerId);
      }
      if (!string.IsNullOrEmpty(filter.CustomerBackendIdOrName))
      {
        parameters.Add("@CustomerBackendIdOrName", filter.CustomerBackendIdOrName);
      }
      if (!string.IsNullOrEmpty(filter.Channel))
      {
        parameters.Add("@Channel", filter.Channel);
      }
      if (filter.TypeID.HasValue)
      {
        parameters.Add("@TypeID", filter.TypeID);
      }

      var orders = connection.Query<OrderSearchResult>(GetStoredProcedureName(Constants.StoredProcedures.Orders.OrderSearch), parameters, commandType: CommandType.StoredProcedure).ToList();


      var totalRows = parameters.Get<int>("TotalRows");
      var result = new SearchResult<OrderSearchResult>
      {
        PageNumber = filter.PageNumber.Value,
        PageSize = filter.PageSize.Value,
        TotalRows = totalRows,
        Rows = orders
      };

      return result;
    }

    public IEnumerable<Order> GetNotDispatched(SqlConnection connection)
    {
      return connection.Query<Order>(GetStoredProcedureName(Constants.StoredProcedures.Orders.GetByNotDispatched), commandType: CommandType.StoredProcedure);
    }

    public void UpdateHold(int orderID, bool holdStatus, SqlConnection connection)
    {
      var p = new DynamicParameters();
      p.Add("@OrderID", orderID, DbType.Int32);
      p.Add("@HoldOrder", holdStatus, DbType.Boolean);

      connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.Orders.UpdateHoldStatus), p, commandType: CommandType.StoredProcedure);
    }

    public void SetOrderDispatched(int orderID, DateTime dispatchedAt, SqlConnection connection)
    {
      var p = new DynamicParameters();
      p.Add("@OrderID", orderID, DbType.Int32);
      p.Add("@DispatchedAt", dispatchedAt, DbType.DateTime);

      connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.Orders.SetOrderDispatched), p, commandType: CommandType.StoredProcedure);
    }

    public Order GetFullByExternalID(string identifier, SqlConnection sqlConnection)
    {
      var id = GetIDByBackendID(identifier, sqlConnection);

      return GetFullByID(id, sqlConnection);
    }

    public IEnumerable<int> GetBetweenStatuses(SqlConnection connection, int? statusIDWith, int? statusIDWithout)
    {
      var p = new DynamicParameters();

      p.Add("@StatusIDWith", statusIDWith, DbType.Int32);
      p.Add("@StatusIDWithout", statusIDWithout, DbType.Int32);

      return connection.Query<int>(GetStoredProcedureName(Constants.StoredProcedures.Orders.GetBetweenStatuses), p, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<int> GetByLatestStatus(SqlConnection connection, int statusID, int orderTypeID)
    {
      var p = new DynamicParameters();

      p.Add("@StatusID", statusID, DbType.Int32);
      if (orderTypeID > 0)
      {
        p.Add("@OrderTypeID", orderTypeID, DbType.Int32);
      }

      return connection.Query<int>(GetStoredProcedureName(Constants.StoredProcedures.Orders.GetByLatestStatus), p, commandType: CommandType.StoredProcedure);
    }
  }
}