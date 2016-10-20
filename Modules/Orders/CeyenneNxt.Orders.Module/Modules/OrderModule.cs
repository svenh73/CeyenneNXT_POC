using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Module.Repositories;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Enums;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNxt.Orders.Shared.Objects;
using Microsoft.Practices.ServiceLocation;

namespace CeyenneNxt.Orders.Module.Modules
{
  public class OrderModule : BaseOrderModule, IOrderModule
  {
   
    public Order CreateOrder(OrderDto orderDto)
    {
      var order = InitOrder(orderDto);

      using (var connection = GetNewConnection())
      {
        connection.Open();
        var transaction = connection.BeginTransaction();
        try
        {
          //customer
          var customer = CustomersRepository.GetByBackendID(order.Customer.BackendID, connection, transaction) ??
            CustomersRepository.Create(order.Customer, connection, transaction);

          order.Customer.ID = customer.ID;

          foreach (var address in order.OrderAddresses)
          {
            //type 
            address.Type.ID = CustomerAddressTypesRepository.GetIDByCode(address.Type.Code, connection, transaction);
            if (address.Type.ID <= 0)
            {
              address.Type.ID = CustomerAddressTypesRepository.Create(address.Type, connection, transaction);
            }

            //country
            address.Address.Country.ID = CountryRepository.GetByCode(address.Address.Country.Code, connection, transaction);
            if (address.Address.Country.ID <= 0)
            {
              address.Address.Country.ID = CountryRepository.Create(address.Address.Country, connection, transaction);
            }


            //customer address
            address.Address.ID = CustomerAddressesRepository.GetByCustomerAndBackendID(customer.ID, address.Address.BackendID,
              connection, transaction);
            if (address.Address.ID <= 0)
            {
              address.Address.ID = CustomerAddressesRepository.Create(address.Address, customer.ID, connection, transaction);
            }
          }

          //order type
          order.OrderType.ID = OrderTypesRepository.GetByName(order.OrderType.Name, connection, transaction);
          if (order.OrderType.ID <= 0)
          {
            order.OrderType.ID = OrderTypesRepository.Create(order.OrderType.Name, connection, transaction);
          }

          var orderID = OrderRepository.GetIDByBackendID(order.BackendID, connection, transaction);
          if (orderID == 0)
          {
            orderID = OrderRepository.Create(order, connection, transaction);

            //order state
            var orderStatus = order.History.FirstOrDefault();
            if (orderStatus != null)
            {
              AddHistoryStatus(orderStatus.Status.Code, orderStatus.Timestamp, connection, transaction, orderID);
            }


            //order addresses
            foreach (var customerAddress in order.OrderAddresses)
            {
              OrderAddressesRepository.Create(orderID, customerAddress.Address.ID, customerAddress.Type.ID, connection, transaction);
            }

            //order quantity unit
            foreach (var orderLine in order.OrderLines)
            {
              orderLine.QuantityUnit.ID = OrderQuantityUnitsRepository.GetIDByCode(orderLine.QuantityUnit.Code, connection,
                transaction);
              if (orderLine.QuantityUnit.ID <= 0)
              {
                orderLine.QuantityUnit.ID = OrderQuantityUnitsRepository.Create(orderLine.QuantityUnit, connection,
                  transaction);
              }

              orderLine.ID = OrderLinesRepository.Create(orderLine, orderID, connection, transaction);

              //attributes
              foreach (var orderLineAttribute in (orderLine.Attributes ?? new List<OrderLineAttributeValue>()).Where(c => !string.IsNullOrEmpty(c.Value)))
              {
                OrderLineModule.AddAttribute(orderLine.ID, orderLineAttribute.Attribute.Code, orderLineAttribute.Attribute.Name, orderLineAttribute.Value, connection, transaction);
              }
            }

            foreach (var orderAttributesAttribute in (order.Attributes ?? new List<OrderAttributeValue>()).Where(c => !string.IsNullOrEmpty(c.Value)))
            {
              orderAttributesAttribute.Attribute.ID =
                OrderAttributesRepository.GetIDByCode(orderAttributesAttribute.Attribute.Code, connection, transaction);

              if (orderAttributesAttribute.Attribute.ID <= 0)
              {
                orderAttributesAttribute.Attribute.ID = OrderAttributesRepository.Create(
                  orderAttributesAttribute.Attribute, connection, transaction);
              }

              OrderAttributesRepository.CreateValue(orderID, orderAttributesAttribute.Attribute.ID, orderAttributesAttribute.Value,
                connection, transaction);
            }


            transaction.Commit();
          }
          else
          {
            transaction.Rollback();
          }

          return OrderRepository.GetFullByID(orderID, connection);
        }
        catch (Exception ex)
        {
          transaction.Rollback();
          throw;
        }
      }
    }

    private Order InitOrder(OrderDto orderDto)
    {
      var order = Mapper.Map<OrderDto, Order>(orderDto);

      if (order == null)
      {
        throw new InvalidOperationException("Order for creation needs to be supplied");
      }

      order.Attributes = new List<OrderAttributeValue>();

      //for new order -> add new status
      order.History.Add(new OrderStatusHistory
      {
        Status = new OrderStatus {Code = "N"},
        Timestamp = DateTime.UtcNow
      });

      foreach (var attributeValue in orderDto.AttributeValues)
      {
        order.Attributes.Add(new OrderAttributeValue
        {
          Value = attributeValue.Value,
          Attribute = new OrderAttribute
          {
            Code = attributeValue.Code,
            Name = attributeValue.Code
          }
        });
      }

      foreach (var orderLine in orderDto.OrderLines)
      {
        var modelLine = order.OrderLines.FirstOrDefault(c => c.ExternalOrderLineID == orderLine.ExternalOrderLineID);

        if (modelLine != null)
        {
          modelLine.Attributes = new List<OrderLineAttributeValue>();
          foreach (var attribute in orderLine.AttributeValues)
          {
            modelLine.Attributes.Add(new OrderLineAttributeValue
            {
              Attribute = new OrderLineAttribute
              {
                Code = attribute.Code,
                Name = attribute.Name
              },
              Value = attribute.Value
            });
          }
        }
      }

      foreach (var address in orderDto.Addresses)
      {
        var addressModel = Mapper.Map<CustomerAddress>(address);

        order.OrderAddresses.Add(new OrderAddress
        {
          Type = Mapper.Map<CustomerAddressType>(address.Type),
          Address = addressModel
        });
      }
      return order;
    }

    private int AddHistoryStatus(string statusCode, DateTime timestamp, SqlConnection connection,
      SqlTransaction transaction, int orderID)
    {
      var orderStatusID = OrderStatusesRepository.GetStatusIDByCode(statusCode, connection, transaction);
      if (orderStatusID > 0)
      {
        return OrderStatusHistoryRepository.Create(orderID, orderStatusID, timestamp, connection, transaction);
      }
      return 0;
    }

    public Order GetFullByID(int id)
    {
      using (var sqlConnection = GetNewConnection())
      {
        var orders = ServiceLocator.Current.GetInstance<IOrderRepository>();
        return orders.GetFullByID(id, sqlConnection);
      }
    }

    public Order GetFullByExternalID(string identifier)
    {
      using (var sqlConnection = GetNewConnection())
      {
        var orders = ServiceLocator.Current.GetInstance<IOrderRepository>();
        return orders.GetFullByExternalID(identifier, sqlConnection);
      }
    }


    public SearchResult<OrderSearchResult> Search(OrderPagingFilter filter)
    {
      using (var sqlConnection = GetNewConnection())
      {
        var orders = ServiceLocator.Current.GetInstance<IOrderRepository>();
        var result = orders.Search(filter, sqlConnection);

        return result;
      }
    }

    public DashboardData GetDashboardData()
    {
      using (var sqlConnection = GetNewConnection())
      {
        var orders = ServiceLocator.Current.GetInstance<IOrderRepository>();
        var result = orders.GetDashboardData(sqlConnection);

        return result;
      }
    }

    public List<Order> GetNotDispatchedOrders()
    {
      using (var connection = GetNewConnection())
      {
        var orders = ServiceLocator.Current.GetInstance<IOrderRepository>();
        return orders.GetNotDispatched(connection).ToList();
      }
    }

    public void Hold(int orderID, bool holdStatus)
    {
      using (var connection = GetNewConnection())
      {
        var orders = ServiceLocator.Current.GetInstance<IOrderRepository>();
        orders.UpdateHold(orderID, holdStatus, connection);
      }
    }

    public void SetDispatched(int orderID, DateTime dispatchedAt)
    {
      using (var connection = GetNewConnection())
      {
        var orders = ServiceLocator.Current.GetInstance<IOrderRepository>();
        orders.SetOrderDispatched(orderID, dispatchedAt, connection);
      }
    }

    public int AddStatus(int orderID, string statusCode, DateTime timestamp)
    {
      using (var connection = GetNewConnection())
      {
        var order = OrderRepository.GetByID(orderID, connection);

        if (order == null)
          throw new NotSupportedException($"Order with id {orderID} doesn't exist");

        return AddHistoryStatus(statusCode, timestamp, connection, null, orderID);
      }
    }

    public IEnumerable<OrderStatusHistory> GetStatusHistoryByOrderID(int orderID)
    {
      using (var connection = GetNewConnection())
      {
        var histories = OrderStatusHistoryRepository.GetStatusHistoryByOrderID(orderID, connection);

        return histories;
      }
    }

    public IEnumerable<OrderStatus> GetAllStatuses()
    {
      using (var connection = GetNewConnection())
      {
        return OrderStatusesRepository.GetAll(connection);
      }
    }

    public IEnumerable<OrderType> GetAllTypes()
    {
      using (var connection = GetNewConnection())
      {
        return OrderTypesRepository.GetAll(connection);
      }
    }

    public IEnumerable<int> GetOrderIDsByLatestStatus(string statusCode, string orderTypeCode)
    {
      using (var connection = GetNewConnection())
      {

        var statusID = OrderStatusesRepository.GetStatusIDByCode(statusCode, connection, null);

        if (statusID == 0)
          throw new NotSupportedException($"Status with code {statusCode} not found");

        int orderTypeID = 0;
        if (!string.IsNullOrEmpty(orderTypeCode))
        {
          var orderTypeRepo = ServiceLocator.Current.GetInstance<IOrderTypesRepository>();
          orderTypeID = orderTypeRepo.GetByName(orderTypeCode, connection, null);
        }

        return OrderRepository.GetByLatestStatus(connection, statusID, orderTypeID);
      }
    }

    /// <summary>
    /// Lookup orders by latest status
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public IEnumerable<int> GetOrderIDsByLatestStatus(string statusCode)
    {
      return GetOrderIDsByLatestStatus(statusCode, string.Empty);
    }

    /// <summary>
    /// Returns all orders that between two statuses
    /// Inclusive with, exclusive without
    /// </summary>
    /// <param name="statusCodeWith">Status code inclusive with</param>
    /// <param name="statusCodeWithout">Status code exclusive without</param>
    /// <returns></returns>
    public IEnumerable<int> GetOrderIDsBetweenStatuses(string statusCodeWith, string statusCodeWithout)
    {
      using (var connection = GetNewConnection())
      {
        var statusIDWith = OrderStatusesRepository.GetStatusIDByCode(statusCodeWith, connection, null);
        var statusIDWithout = OrderStatusesRepository.GetStatusIDByCode(statusCodeWithout, connection, null);

        if (statusIDWith == 0)
          throw new NotSupportedException($"Status with code {statusCodeWith} not found");

        if (statusIDWithout == 0)
          throw new NotSupportedException($"Status with code {statusCodeWithout} not found");

        return OrderRepository.GetBetweenStatuses(connection, statusIDWith, statusIDWithout);

      }
    }

    public Order UpdateOrder(Order order)
    {
      return GetFullByID(order.ID);
    }

    public void AddAttribute(int orderID, string attributeCode, string attributeName, string attributeValue)
    {
      using (var connection = GetNewConnection())
      {
        AddAttribute(orderID, attributeCode, attributeName, attributeValue, connection, null);
      }
    }

    public void AddAttribute(int orderID, string attributeCode, string attributeName, string attributeValue, SqlConnection connection, SqlTransaction transaction)
    {
      if (string.IsNullOrEmpty(attributeCode))
        throw new ArgumentNullException(nameof(attributeCode));

      if (string.IsNullOrEmpty(attributeName))
        throw new ArgumentNullException(nameof(attributeName));

      var attributeID = OrderAttributesRepository.GetIDByCode(attributeCode, connection, transaction);

      if (attributeID <= 0)
      {
        attributeID = OrderAttributesRepository.Create(new OrderAttribute { Name = attributeName, Code = attributeCode }, connection, transaction);
      }

      OrderAttributesRepository.CreateValue(orderID, attributeID, attributeValue, connection, transaction);
    }
  }
}