using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Module.Repositories;
using CeyenneNxt.Orders.Shared.Constants;
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

    public OrderLine GetFullByID(int id)
    {
      using (var sqlConnection = GetNewConnection())
      {
        return OrderLinesRepository.GetFullByID(id, sqlConnection);
      }
    }

    public IEnumerable<OrderLineStatus> GetAllStatuses()
    {
      using (var sqlConnection = GetNewConnection())
      {
        return OrderLineStatusesRepository.GetAllStatuses(sqlConnection);
      }
    }

    public int AddStatusHistory(int orderLineID, string statusCode, int? quantityChanged, DateTime timestamp, string message)
    {
      using (var connection = GetNewConnection())
      {
        connection.Open();
        var transaction = connection.BeginTransaction();
        try
        {
          var orderLine = OrderLinesRepository.GetByID(orderLineID, connection, transaction);

          if (orderLine == null)
            throw new NotSupportedException($"Order with id {orderLineID} doesn't exist");

          var orderStatusID = OrderLineStatusHistoryRepository.GetStatusIDByCode(statusCode, connection, transaction);
          int orderStatusHistoryID = 0;
          if (orderStatusID > 0)
          {
            orderStatusHistoryID = OrderLineStatusHistoryRepository.Create(orderLineID, orderStatusID, quantityChanged, timestamp, message, connection, transaction);
          }

          transaction.Commit();
          return orderStatusHistoryID;
        }
        catch (Exception)
        {
          transaction.Rollback();
          throw;
        }
      }
    }

    public IEnumerable<OrderLineStatusHistory> GetStatusHistoryByOrderLineID(int orderLineID)
    {
      using (var connection = GetNewConnection())
      {
        var histories = OrderLineStatusHistoryRepository.GetStatusHistoryByOrderLineID(orderLineID, connection);

        return histories;
      }
    }


    public void AddAttribute(int orderLineID, string attributeCode, string attributeName, string attributeValue)
    {
      using (var connection = GetNewConnection())
      {
        AddAttribute(orderLineID, attributeCode, attributeName, attributeValue, connection, null);
      }
    }

    public void AddAttribute(int orderLineID, string attributeCode, string attributeName, string attributeValue, SqlConnection connection, SqlTransaction transaction)
    {
      if (string.IsNullOrEmpty(attributeCode))
        throw new ArgumentNullException(nameof(attributeCode));

      if (string.IsNullOrEmpty(attributeName))
        throw new ArgumentNullException(nameof(attributeName));

      var attributeID = OrderLineAttributesRepository.GetIDByCode(attributeCode, connection, transaction);

      if (attributeID <= 0)
      {
        attributeID = OrderLineAttributesRepository.Create(attributeCode, attributeName, connection, transaction);
      }

      OrderLineAttributesRepository.CreateValue(orderLineID, attributeID, attributeValue, connection, transaction);
    }
  }
}
