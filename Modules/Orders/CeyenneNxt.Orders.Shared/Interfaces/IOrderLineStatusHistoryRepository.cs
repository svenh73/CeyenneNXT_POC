using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderLineStatusHistoryRepository
  {
    int Create(int orderLineID, int statusID, int? quantityChanged, DateTime timestamp, string message, SqlConnection connection, SqlTransaction transaction);
    IEnumerable<OrderLineStatusHistory> GetStatusHistoryByOrderLineID(int orderLineID, SqlConnection connection);
    int GetStatusIDByCode(string code, SqlConnection connection, SqlTransaction transaction);
  }
}