using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;

namespace CeyenneNxt.Orders.Module.Modules
{
  public interface IOrderLineModule
  {
    void AddAttribute(IOrderModuleSession session,int orderLineID, string attributeCode, string attributeName, string attributeValue);
    int AddStatusHistory(IOrderModuleSession session,int orderLineID, string statusCode, int? quantityChanged, DateTime timestamp, string message);
    IEnumerable<OrderLineStatusDto> GetAllStatuses(IOrderModuleSession session);
    OrderLineDto GetFullByID(IOrderModuleSession session,int id);
    IEnumerable<OrderLineStatusHistoryDto> GetStatusHistoryByOrderLineID(IOrderModuleSession session,int orderLineID);
  }
}