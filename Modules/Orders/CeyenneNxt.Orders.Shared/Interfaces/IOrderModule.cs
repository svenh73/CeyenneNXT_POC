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
    void AddAttribute(IOrderModuleSession session,int orderID, string attributeCode, string attributeName, string attributeValue);
    int AddStatus(IOrderModuleSession session, int orderID, string statusCode, DateTime timestamp);
    OrderDto CreateOrder(IOrderModuleSession session, OrderDto order);
    IEnumerable<OrderStatusDto> GetAllStatuses(IOrderModuleSession session);
    IEnumerable<OrderTypeDto> GetAllTypes(IOrderModuleSession session);
    DashboardDataDto GetDashboardData(IOrderModuleSession session);
    OrderDto GetFullByExternalID(IOrderModuleSession session,string identifier);
    OrderDto GetFullByID(IOrderModuleSession session,int id);
    List<OrderDto> GetNotDispatchedOrders(IOrderModuleSession session);
    IEnumerable<int> GetOrderIDsBetweenStatuses(IOrderModuleSession session,string statusCodeWith, string statusCodeWithout);
    IEnumerable<int> GetOrderIDsByLatestStatus(IOrderModuleSession session,string statusCode);
    IEnumerable<int> GetOrderIDsByLatestStatus(IOrderModuleSession session,string statusCode, string orderTypeCode);
    IEnumerable<OrderStatusHistoryDto> GetStatusHistoryByOrderID(IOrderModuleSession session,int orderID);
    void Hold(IOrderModuleSession session,int orderID, bool holdStatus);
    SearchResultDto<OrderSearchResultDto> Search(IOrderModuleSession session,OrderPagingFilterDto filter);
    void SetDispatched(IOrderModuleSession session,int orderID, DateTime dispatchedAt);
    OrderDto UpdateOrder(IOrderModuleSession session,Order order);
  }
}