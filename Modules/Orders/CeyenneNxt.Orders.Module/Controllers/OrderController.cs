using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using CeyenneNxt.Orders.Module.Modules;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Dtos.Messages;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNxt.Orders.Shared.Objects;
using CeyenneNXT.Orders.ApiContracts.Models.CreateModels;

namespace CeyenneNxt.Orders.Module.Controllers
{
  public class OrdersController : ApiController, IOrdersController
  {
    public IOrderModule OrderModule { get; private set; }

    public OrdersController(IOrderModule orderModule)
    {
      OrderModule = orderModule;
    }


    /// <summary>
    /// Retrieves an order by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/orders/5
    [HttpGet]
    public OrderDto Get(int id)
    {
      var order = OrderModule.GetFullByID(id);
      var orderContract = MapOrderToOrderContract(order);

      return orderContract;
    }

    public OrderDto MapOrderToOrderContract(Order order)
    {
      var orderContract = Mapper.Map<OrderDto>(order);
      if (order?.OrderAddresses != null)
      {
        foreach (var address in order.OrderAddresses)
        {
          if (address.Address == null)
          {
            continue;
          }
          var addressContract = Mapper.Map<AddressDto>(address.Address);
          if (address.Type == null)
          {
            orderContract.Addresses.Add(addressContract);
            continue;
          }
          addressContract.Type = Mapper.Map<AddressTypeDto>(address.Type);
          orderContract.Addresses.Add(addressContract);
        }
      }

      return orderContract;
    }

    /// <summary>  
    /// Create order with order lines and customers
    /// </summary>
    /// <param name="order"></param>
    // POST: api/orders
    [HttpPost]
    public OrderDto Post([FromBody] OrderDto order)
    {
      if (order == null)
      {
        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
          Content = new StringContent("order == null"),
          ReasonPhrase = "Error saving order"
        };

        throw new HttpResponseException(resp);
      }


      var newOrderFull = OrderModule.CreateOrder(order);
      var orderResult = Mapper.Map<Order, OrderDto>(newOrderFull);

      return orderResult;
    }


    /// <summary>
    /// Retrieves an order by the external identifier
    /// </summary>
    /// <param name="externalID"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("api/orders/getByExternalIdentifier")]
    public OrderDto GetByExternalIdentifier(string externalID)
    {
      var order = OrderModule.GetFullByExternalID(externalID);

      var orderContract = MapOrderToOrderContract(order);

      return orderContract;
    }

    /// <summary>
    /// Search orders
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("api/orders/search")]
    public SearchResultDto<OrderSearchResultDto> Search([FromUri] OrderPagingFilterDto filter)
    {
      return OrderModule.Search(filter);
    }

    [HttpGet]
    [Route("api/orders/getNew")]
    public List<OrderDto> GetNotDispatchedOrders()
    {
      var orders = OrderModule.GetNotDispatchedOrders();
      var orderResult = Mapper.Map<List<Order>, List<OrderDto>>(orders);

      return orderResult;

    }

    [HttpPut]
    [Route("api/orders/dispatch")]
    public void UpdateOrderDispatched([FromBody] SetOrderDispatchedDto model)
    {
      OrderModule.SetDispatched(model.OrderID, model.DispatchedAt);
    }

    [HttpGet]
    [Route("api/orders/getDashboardData")]
    public DashboardDataDto GetDashboardData()
    {
      return OrderModule.GetDashboardData();
    }

    [HttpGet]
    [Route("api/orders/getAllTypes")]
    public IEnumerable<OrderTypeDto> GetAllTypes()
    {
      return OrderModule.GetAllTypes();
    }

    [HttpGet]
    [Route("api/orders/getBetweenStatuses")]
    public IEnumerable<int> GetOrdersBetweenStatuses(string statusCodeWith, string statusCodeWithout)
    {
      return OrderModule.GetOrderIDsBetweenStatuses(statusCodeWith, statusCodeWithout);
    }

    /// <summary>
    /// Get orders by latest status
    /// </summary>
    /// <param name="statusCode"></param>    
    /// <returns></returns>
    [HttpGet]
    [Route("api/orders/getByLatestStatus")]
    public IEnumerable<int> GetOrderByLatestStatus(string statusCode)
    {
      if (Request.Headers.Contains("OrderTypeCode"))
      {
        var orderTypeCode = Request.Headers.GetValues("OrderTypeCode").FirstOrDefault();
        return OrderModule.GetOrderIDsByLatestStatus(statusCode, orderTypeCode);
      }
      return OrderModule.GetOrderIDsByLatestStatus(statusCode);
    }

    [HttpPost]
    [Route("api/orders/createAttribute")]
    public IHttpActionResult AddOrderLineAttribute(OrderAttributeDto model)
    {
      try
      {
        OrderModule.AddAttribute(model.OrderID, model.AttributeCode, model.AttributeName, model.AttributeValue);

        return Ok();
      }
      catch (Exception e)
      {
        var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
          Content = new StringContent(e.Message),
          ReasonPhrase = "Error adding an order line attribute"
        };

        throw new HttpResponseException(resp);
      }
    }

  }
}

