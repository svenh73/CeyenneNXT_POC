using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderStatusHistoryRepository
  {
    int Create(int orderID, int statusID, DateTime timestamp, SqlConnection connection, SqlTransaction transaction);
    IEnumerable<OrderStatusHistory> GetStatusHistoryByOrderID(int orderID, SqlConnection connection);
  }
}