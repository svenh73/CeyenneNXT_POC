using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Objects;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderRepository
  {
    int Create(IOrderModuleSession session,Order model);
    Order GetByID(IOrderModuleSession session,int orderID);
    DashboardData GetDashboardData(IOrderModuleSession session);
    Order GetFullByExternalID(IOrderModuleSession session,string identifier);
    Order GetFullByID(IOrderModuleSession session,int id);
    int GetIDByBackendID(IOrderModuleSession session,string backendID);
    IEnumerable<Order> GetNotDispatched(IOrderModuleSession session);
    SearchResult<OrderSearchResult> Search(IOrderModuleSession session,OrderPagingFilter filter);
    void SetOrderDispatched(IOrderModuleSession session,int orderID, DateTime dispatchedAt);
    void UpdateHold(IOrderModuleSession session,int orderID, bool holdStatus);
    IEnumerable<int> GetBetweenStatuses(IOrderModuleSession session, int? statusIDWith, int? statusIDWithout);
    IEnumerable<int> GetByLatestStatus(IOrderModuleSession session, int statusID, int orderTypeID);
  }
}