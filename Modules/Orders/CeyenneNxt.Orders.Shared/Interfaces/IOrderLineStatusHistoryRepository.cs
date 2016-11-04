using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderLineStatusHistoryRepository
  {
    int Create(IOrderModuleSession session,int orderLineID, int statusID, int? quantityChanged, DateTime timestamp, string message);
    IEnumerable<OrderLineStatusHistory> GetStatusHistoryByOrderLineID(IOrderModuleSession session,int orderLineID);
    int GetStatusIDByCode(IOrderModuleSession session,string code);
  }
}