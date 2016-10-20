using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;

namespace CeyenneNxt.Orders.Module.Modules
{
  public interface IOrderLineModule
  {

    void AddAttribute(int orderLineID, string attributeCode, string attributeName, string attributeValue);
    void AddAttribute(int orderLineID, string attributeCode, string attributeName, string attributeValue, SqlConnection connection, SqlTransaction transaction);
    int AddStatusHistory(int orderLineID, string statusCode, int? quantityChanged, DateTime timestamp, string message);
    IEnumerable<OrderLineStatus> GetAllStatuses();
    OrderLine GetFullByID(int id);
    IEnumerable<OrderLineStatusHistory> GetStatusHistoryByOrderLineID(int orderLineID);
  }
}