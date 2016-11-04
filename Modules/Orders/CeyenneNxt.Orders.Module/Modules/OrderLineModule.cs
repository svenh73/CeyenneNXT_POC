using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Module.Repositories;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Enums;
using CeyenneNxt.Orders.Shared.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace CeyenneNxt.Orders.Module.Modules
{
  public class OrderLineModule : BaseOrderModule, IOrderLineModule
  {
    public OrderLineModule()
    {
    }

    public OrderLineDto GetFullByID(IOrderModuleSession session,int id)
    {
      var orderline = OrderLinesRepository.GetFullByID(session,id);
      return Mapper.Map<OrderLine, OrderLineDto>(orderline);
    }

    public IEnumerable<OrderLineStatusDto> GetAllStatuses(IOrderModuleSession session)
    {
      return OrderLineStatusesRepository.GetAllStatuses(session).Select(p => Mapper.Map<OrderLineStatus,OrderLineStatusDto>(p));
    }

    public int AddStatusHistory(IOrderModuleSession session,int orderLineID, string statusCode, int? quantityChanged, DateTime timestamp, string message)
    {
      session.BeginTransaction();
      try
      {
        var orderLine = OrderLinesRepository.GetByID(session,orderLineID);

        if (orderLine == null)
          throw new NotSupportedException($"Order with id {orderLineID} doesn't exist");

        var orderStatusID = OrderLineStatusHistoryRepository.GetStatusIDByCode(session,statusCode);
        int orderStatusHistoryID = 0;
        if (orderStatusID > 0)
        {
          orderStatusHistoryID = OrderLineStatusHistoryRepository.Create(session,orderLineID, orderStatusID, quantityChanged, timestamp, message);
        }

        session.Commit();
        return orderStatusHistoryID;
      }
      catch (Exception)
      {
        session.Rollback();
        throw;
      }
    }

    public IEnumerable<OrderLineStatusHistoryDto> GetStatusHistoryByOrderLineID(IOrderModuleSession session, int orderLineID)
    {
      return OrderLineStatusHistoryRepository.GetStatusHistoryByOrderLineID(session,orderLineID).Select(p => Mapper.Map<OrderLineStatusHistory, OrderLineStatusHistoryDto>(p));
    }

    public void AddAttribute(IOrderModuleSession session,int orderLineID, string attributeCode, string attributeName, string attributeValue)
    {
      if (string.IsNullOrEmpty(attributeCode))
        throw new ArgumentNullException(nameof(attributeCode));

      if (string.IsNullOrEmpty(attributeName))
        throw new ArgumentNullException(nameof(attributeName));

      var attributeID = OrderLineAttributesRepository.GetIDByCode(session,attributeCode);

      if (attributeID <= 0)
      {
        attributeID = OrderLineAttributesRepository.Create(session,attributeCode, attributeName);
      }

      OrderLineAttributesRepository.CreateValue(session,orderLineID, attributeID, attributeValue);
    }
  }
}
