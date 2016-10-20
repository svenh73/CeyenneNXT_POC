using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Objects;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderModule
  {
    void AddAttribute(int orderID, string attributeCode, string attributeName, string attributeValue);
    void AddAttribute(int orderID, string attributeCode, string attributeName, string attributeValue, SqlConnection connection, SqlTransaction transaction);
    int AddStatus(int orderID, string statusCode, DateTime timestamp);
    Order CreateOrder(OrderDto order);
    IEnumerable<OrderStatus> GetAllStatuses();
    IEnumerable<OrderType> GetAllTypes();
    DashboardData GetDashboardData();
    Order GetFullByExternalID(string identifier);
    Order GetFullByID(int id);
    List<Order> GetNotDispatchedOrders();
    IEnumerable<int> GetOrderIDsBetweenStatuses(string statusCodeWith, string statusCodeWithout);
    IEnumerable<int> GetOrderIDsByLatestStatus(string statusCode);
    IEnumerable<int> GetOrderIDsByLatestStatus(string statusCode, string orderTypeCode);
    IEnumerable<OrderStatusHistory> GetStatusHistoryByOrderID(int orderID);
    void Hold(int orderID, bool holdStatus);
    SearchResult<OrderSearchResult> Search(OrderPagingFilter filter);
    void SetDispatched(int orderID, DateTime dispatchedAt);
    Order UpdateOrder(Order order);
  }
}