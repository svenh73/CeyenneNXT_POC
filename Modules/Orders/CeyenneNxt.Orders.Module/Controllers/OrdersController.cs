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
      using (var session = new OrderModuleSession())
      {
        return OrderModule.GetFullByID(session, id); ;
      }
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

      using (var session = new OrderModuleSession())
      {
        return OrderModule.CreateOrder(session, order); ;
      }
      
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
      using (var session = new OrderModuleSession())
      {
        return OrderModule.GetFullByExternalID(session, externalID); ;
      }
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
      using (var session = new OrderModuleSession())
      {
        return OrderModule.Search(session,filter);
      }
    }

    [HttpGet]
    [Route("api/orders/getNew")]
    public List<OrderDto> GetNotDispatchedOrders()
    {
      using (var session = new OrderModuleSession())
      { 
        return OrderModule.GetNotDispatchedOrders(session); ;
      }
    }

    [HttpPut]
    [Route("api/orders/dispatch")]
    public void UpdateOrderDispatched([FromBody] SetOrderDispatchedDto model)
    {
      using (var session = new OrderModuleSession())
      {
        OrderModule.SetDispatched(session,model.OrderID, model.DispatchedAt);
      }
    }

    [HttpGet]
    [Route("api/orders/getDashboardData")]
    public DashboardDataDto GetDashboardData()
    {
      using (var session = new OrderModuleSession())
      {
        return OrderModule.GetDashboardData(session);
      }
    }

    [HttpGet]
    [Route("api/orders/getAllTypes")]
    public IEnumerable<OrderTypeDto> GetAllTypes()
    {
      using (var session = new OrderModuleSession())
      {
        return OrderModule.GetAllTypes(session);
      }
    }

    [HttpGet]
    [Route("api/orders/getBetweenStatuses")]
    public IEnumerable<int> GetOrdersBetweenStatuses(string statusCodeWith, string statusCodeWithout)
    {
      using (var session = new OrderModuleSession())
      {
        return OrderModule.GetOrderIDsBetweenStatuses(session,statusCodeWith, statusCodeWithout);
      }
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
      using (var session = new OrderModuleSession())
      {
        if (Request.Headers.Contains("OrderTypeCode"))
        {
          var orderTypeCode = Request.Headers.GetValues("OrderTypeCode").FirstOrDefault();
          return OrderModule.GetOrderIDsByLatestStatus(session, statusCode, orderTypeCode);
        }
        return OrderModule.GetOrderIDsByLatestStatus(session,statusCode);
      }
    }

    [HttpPost]
    [Route("api/orders/createAttribute")]
    public IHttpActionResult AddOrderLineAttribute(OrderAttributeDto model)
    {
      using (var session = new OrderModuleSession())
      {
        try
        {
          OrderModule.AddAttribute(session, model.OrderID, model.AttributeCode, model.AttributeName, model.AttributeValue);

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
}

