using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderStatusHistoryRepository
  {
    int Create(IOrderModuleSession session,int orderID, int statusID, DateTime timestamp);
    IEnumerable<OrderStatusHistory> GetStatusHistoryByOrderID(IOrderModuleSession session,int orderID);
  }
}