using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Objects;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderRepository
  {
    int Create(Order model, SqlConnection connection, SqlTransaction transaction);
    Order GetByID(int orderID, SqlConnection connection);
    DashboardData GetDashboardData(SqlConnection connection);
    Order GetFullByExternalID(string identifier, SqlConnection sqlConnection);
    Order GetFullByID(int id, SqlConnection connection);
    int GetIDByBackendID(string backendID, SqlConnection connection);
    int GetIDByBackendID(string backendID, SqlConnection connection, SqlTransaction transaction);
    IEnumerable<Order> GetNotDispatched(SqlConnection connection);
    SearchResult<OrderSearchResult> Search(OrderPagingFilter filter, SqlConnection connection);
    void SetOrderDispatched(int orderID, DateTime dispatchedAt, SqlConnection connection);
    void UpdateHold(int orderID, bool holdStatus, SqlConnection connection);
    IEnumerable<int> GetBetweenStatuses(SqlConnection connection, int? statusIDWith, int? statusIDWithout);
    IEnumerable<int> GetByLatestStatus(SqlConnection connection, int statusID, int orderTypeID);
  }
}