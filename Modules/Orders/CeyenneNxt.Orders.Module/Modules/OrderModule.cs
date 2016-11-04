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
   
    public OrderDto CreateOrder(IOrderModuleSession session,OrderDto orderDto)
    {
      var order = InitOrder(orderDto);

      session.BeginTransaction();

      try
      {
        //customer
        var customer = CustomersRepository.GetByBackendID(session,order.Customer.BackendID) ??
          CustomersRepository.Create(session,order.Customer);

        order.Customer.ID = customer.ID;

        foreach (var address in order.OrderAddresses)
        {
          //type 
          address.Type.ID = CustomerAddressTypesRepository.GetIDByCode(session,address.Type.Code);
          if (address.Type.ID <= 0)
          {
            address.Type.ID = CustomerAddressTypesRepository.Create(session,address.Type);
          }

          //country
          address.Address.Country.ID = CountryRepository.GetByCode(session,address.Address.Country.Code);
          if (address.Address.Country.ID <= 0)
          {
            address.Address.Country.ID = CountryRepository.Create(session,address.Address.Country);
          }


          //customer address
          address.Address.ID = CustomerAddressesRepository.GetByCustomerAndBackendID(session,customer.ID, address.Address.BackendID);
          if (address.Address.ID <= 0)
          {
            address.Address.ID = CustomerAddressesRepository.Create(session,address.Address, customer.ID);
          }
        }

        //order type
        order.OrderType.ID = OrderTypesRepository.GetByName(session,order.OrderType.Name);
        if (order.OrderType.ID <= 0)
        {
          order.OrderType.ID = OrderTypesRepository.Create(session,order.OrderType.Name);
        }

        var orderID = OrderRepository.GetIDByBackendID(session,order.BackendID);
        if (orderID == 0)
        {
          orderID = OrderRepository.Create(session,order);

          //order state
          var orderStatus = order.History.FirstOrDefault();
          if (orderStatus != null)
          {
            AddHistoryStatus(session,orderStatus.Status.Code, orderStatus.Timestamp, orderID);
          }


          //order addresses
          foreach (var customerAddress in order.OrderAddresses)
          {
            OrderAddressesRepository.Create(session,orderID, customerAddress.Address.ID, customerAddress.Type.ID);
          }

          //order quantity unit
          foreach (var orderLine in order.OrderLines)
          {
            orderLine.QuantityUnit.ID = OrderQuantityUnitsRepository.GetIDByCode(session,orderLine.QuantityUnit.Code);
            if (orderLine.QuantityUnit.ID <= 0)
            {
              orderLine.QuantityUnit.ID = OrderQuantityUnitsRepository.Create(session,orderLine.QuantityUnit);
            }

            orderLine.ID = OrderLinesRepository.Create(session,orderLine, orderID);

            //attributes
            foreach (var orderLineAttribute in (orderLine.Attributes ?? new List<OrderLineAttributeValue>()).Where(c => !string.IsNullOrEmpty(c.Value)))
            {
              OrderLineModule.AddAttribute(session,orderLine.ID, orderLineAttribute.Attribute.Code, orderLineAttribute.Attribute.Name, orderLineAttribute.Value);
            }
          }

          foreach (var orderAttributesAttribute in (order.Attributes ?? new List<OrderAttributeValue>()).Where(c => !string.IsNullOrEmpty(c.Value)))
          {
            orderAttributesAttribute.Attribute.ID =
              OrderAttributesRepository.GetIDByCode(session,orderAttributesAttribute.Attribute.Code);

            if (orderAttributesAttribute.Attribute.ID <= 0)
            {
              orderAttributesAttribute.Attribute.ID = OrderAttributesRepository.Create(session,orderAttributesAttribute.Attribute);
            }

            OrderAttributesRepository.CreateValue(session,orderID, orderAttributesAttribute.Attribute.ID, orderAttributesAttribute.Value);
          }


          session.Commit();
        }
        else
        {
          session.Rollback();
        }

        var neworder = OrderRepository.GetFullByID(session,orderID);
        return Mapper.Map<Order, OrderDto>(neworder);
      }
      catch (Exception ex)
      {
        session.Rollback();
        throw;
      }
      session.BeginTransaction();
      try
      {
        //customer
        var customer = CustomersRepository.GetByBackendID(session,order.Customer.BackendID) ??
          CustomersRepository.Create(session,order.Customer);

        order.Customer.ID = customer.ID;

        foreach (var address in order.OrderAddresses)
        {
          //type 
          address.Type.ID = CustomerAddressTypesRepository.GetIDByCode(session,address.Type.Code);
          if (address.Type.ID <= 0)
          {
            address.Type.ID = CustomerAddressTypesRepository.Create(session,address.Type);
          }

          //country
          address.Address.Country.ID = CountryRepository.GetByCode(session,address.Address.Country.Code);
          if (address.Address.Country.ID <= 0)
          {
            address.Address.Country.ID = CountryRepository.Create(session,address.Address.Country);
          }


          //customer address
          address.Address.ID = CustomerAddressesRepository.GetByCustomerAndBackendID(session,customer.ID, address.Address.BackendID);
          if (address.Address.ID <= 0)
          {
            address.Address.ID = CustomerAddressesRepository.Create(session,address.Address, customer.ID);
          }
        }

        //order type
        order.OrderType.ID = OrderTypesRepository.GetByName(session,order.OrderType.Name);
        if (order.OrderType.ID <= 0)
        {
          order.OrderType.ID = OrderTypesRepository.Create(session,order.OrderType.Name);
        }

        var orderID = OrderRepository.GetIDByBackendID(session,order.BackendID);
        if (orderID == 0)
        {
          orderID = OrderRepository.Create(session,order);

          //order state
          var orderStatus = order.History.FirstOrDefault();
          if (orderStatus != null)
          {
            AddHistoryStatus(session,orderStatus.Status.Code, orderStatus.Timestamp, orderID);
          }


          //order addresses
          foreach (var customerAddress in order.OrderAddresses)
          {
            OrderAddressesRepository.Create(session,orderID, customerAddress.Address.ID, customerAddress.Type.ID);
          }

          //order quantity unit
          foreach (var orderLine in order.OrderLines)
          {
            orderLine.QuantityUnit.ID = OrderQuantityUnitsRepository.GetIDByCode(session,orderLine.QuantityUnit.Code);
            if (orderLine.QuantityUnit.ID <= 0)
            {
              orderLine.QuantityUnit.ID = OrderQuantityUnitsRepository.Create(session,orderLine.QuantityUnit);
            }

            orderLine.ID = OrderLinesRepository.Create(session,orderLine, orderID);

            //attributes
            foreach (var orderLineAttribute in (orderLine.Attributes ?? new List<OrderLineAttributeValue>()).Where(c => !string.IsNullOrEmpty(c.Value)))
            {
              OrderLineModule.AddAttribute(session,orderLine.ID, orderLineAttribute.Attribute.Code, orderLineAttribute.Attribute.Name, orderLineAttribute.Value);
            }
          }

          foreach (var orderAttributesAttribute in (order.Attributes ?? new List<OrderAttributeValue>()).Where(c => !string.IsNullOrEmpty(c.Value)))
          {
            orderAttributesAttribute.Attribute.ID =
              OrderAttributesRepository.GetIDByCode(session,orderAttributesAttribute.Attribute.Code);

            if (orderAttributesAttribute.Attribute.ID <= 0)
            {
              orderAttributesAttribute.Attribute.ID = OrderAttributesRepository.Create(session,orderAttributesAttribute.Attribute);
            }

            OrderAttributesRepository.CreateValue(session,orderID, orderAttributesAttribute.Attribute.ID, orderAttributesAttribute.Value);
          }


          session.Commit();
        }
        else
        {
          session.Rollback();
        }

        var neworder = OrderRepository.GetFullByID(session, orderID);
        return Mapper.Map<Order, OrderDto>(neworder);
      }
      catch (Exception ex)
      {
        session.Rollback();
        throw;
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

    private int AddHistoryStatus(IOrderModuleSession session, string statusCode, DateTime timestamp, int orderID)
    {
      var orderStatusID = OrderStatusesRepository.GetStatusIDByCode(session,statusCode);
      if (orderStatusID > 0)
      {
        return OrderStatusHistoryRepository.Create(session,orderID, orderStatusID, timestamp);
      }
      return 0;
    }

    public OrderDto GetFullByID(IOrderModuleSession session,int id)
    {
      var order = OrderRepository.GetFullByID(session,id);
      return Mapper.Map<Order, OrderDto>(order);
    }

    public OrderDto GetFullByExternalID(IOrderModuleSession session,string identifier)
    {
      var order = OrderRepository.GetFullByExternalID(session,identifier);
      return Mapper.Map<Order, OrderDto>(order);
    }


    public SearchResultDto<OrderSearchResultDto> Search(IOrderModuleSession session, OrderPagingFilterDto filterDto)
    {
      var filter = Mapper.Map<OrderPagingFilterDto, OrderPagingFilter>(filterDto);
      var result = OrderRepository.Search(session, filter);
      return Mapper.Map<SearchResult<OrderSearchResult>, SearchResultDto<OrderSearchResultDto>>(result);
    }

    public DashboardDataDto GetDashboardData(IOrderModuleSession session)
    {
      var result = OrderRepository.GetDashboardData(session);
      return Mapper.Map<DashboardData, DashboardDataDto>(result);
    }

    public List<OrderDto> GetNotDispatchedOrders(IOrderModuleSession session)
    {
      var orders = ServiceLocator.Current.GetInstance<IOrderRepository>();
      return orders.GetNotDispatched(session).Select(p => Mapper.Map<Order,OrderDto>(p)).ToList();
    }

    public void Hold(IOrderModuleSession session,int orderID, bool holdStatus)
    {
      var orders = ServiceLocator.Current.GetInstance<IOrderRepository>();
      orders.UpdateHold(session,orderID, holdStatus);
    }

    public void SetDispatched(IOrderModuleSession session,int orderID, DateTime dispatchedAt)
    {
      var orders = ServiceLocator.Current.GetInstance<IOrderRepository>();
      orders.SetOrderDispatched(session,orderID, dispatchedAt);
    }

    public int AddStatus(IOrderModuleSession session,int orderID, string statusCode, DateTime timestamp)
    {
      var order = OrderRepository.GetByID(session,orderID);

      if (order == null)
        throw new NotSupportedException($"Order with id {orderID} doesn't exist");

      return AddHistoryStatus(session, statusCode, timestamp, orderID);
    }

    public IEnumerable<OrderStatusHistoryDto> GetStatusHistoryByOrderID(IOrderModuleSession session,int orderID)
    {
      var histories = OrderStatusHistoryRepository.GetStatusHistoryByOrderID(session, orderID);
      return histories.Select(p => Mapper.Map<OrderStatusHistory, OrderStatusHistoryDto>(p));
    }

    public IEnumerable<OrderStatusDto> GetAllStatuses(IOrderModuleSession session)
    {
      return OrderStatusesRepository.GetAll(session).Select(p => Mapper.Map<OrderStatus,OrderStatusDto>(p)).ToList();
    }

    public IEnumerable<OrderTypeDto> GetAllTypes(IOrderModuleSession session)
    {
      var types = OrderTypesRepository.GetAll(session);
      return types.Select(t => Mapper.Map<OrderType, OrderTypeDto>(t));
    }

    public IEnumerable<int> GetOrderIDsByLatestStatus(IOrderModuleSession session,string statusCode, string orderTypeCode)
    {
      var statusID = OrderStatusesRepository.GetStatusIDByCode(session,statusCode);

      if (statusID == 0)
        throw new NotSupportedException($"Status with code {statusCode} not found");

      int orderTypeID = 0;
      if (!string.IsNullOrEmpty(orderTypeCode))
      {
        var orderTypeRepo = ServiceLocator.Current.GetInstance<IOrderTypesRepository>();
        orderTypeID = orderTypeRepo.GetByName(session,orderTypeCode);
      }

      return OrderRepository.GetByLatestStatus(session, statusID, orderTypeID);
    }

    /// <summary>
    /// Lookup orders by latest status
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public IEnumerable<int> GetOrderIDsByLatestStatus(IOrderModuleSession session,string statusCode)
    {
      return GetOrderIDsByLatestStatus(session,statusCode, string.Empty);
    }

    /// <summary>
    /// Returns all orders that between two statuses
    /// Inclusive with, exclusive without
    /// </summary>
    /// <param name="statusCodeWith">Status code inclusive with</param>
    /// <param name="statusCodeWithout">Status code exclusive without</param>
    /// <returns></returns>
    public IEnumerable<int> GetOrderIDsBetweenStatuses(IOrderModuleSession session,string statusCodeWith, string statusCodeWithout)
    {
      var statusIDWith = OrderStatusesRepository.GetStatusIDByCode(session,statusCodeWith);
      var statusIDWithout = OrderStatusesRepository.GetStatusIDByCode(session,statusCodeWithout);

      if (statusIDWith == 0)
        throw new NotSupportedException($"Status with code {statusCodeWith} not found");

      if (statusIDWithout == 0)
        throw new NotSupportedException($"Status with code {statusCodeWithout} not found");

      return OrderRepository.GetBetweenStatuses(session, statusIDWith, statusIDWithout);
    }

    public OrderDto UpdateOrder(IOrderModuleSession session,Order order)
    {
      return GetFullByID(session,order.ID);
    }

    public void AddAttribute(IOrderModuleSession session,int orderID, string attributeCode, string attributeName, string attributeValue)
    {
      if (string.IsNullOrEmpty(attributeCode))
        throw new ArgumentNullException(nameof(attributeCode));

      if (string.IsNullOrEmpty(attributeName))
        throw new ArgumentNullException(nameof(attributeName));

      var attributeID = OrderAttributesRepository.GetIDByCode(session,attributeCode);

      if (attributeID <= 0)
      {
        attributeID = OrderAttributesRepository.Create(session,new OrderAttribute { Name = attributeName, Code = attributeCode });
      }

      OrderAttributesRepository.CreateValue(session,orderID, attributeID, attributeValue);
    }
  }
}