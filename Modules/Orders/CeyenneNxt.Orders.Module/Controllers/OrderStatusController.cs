using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Dtos.Messages;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Exceptions;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.WebApi.Controllers;

namespace CeyenneNxt.Orders.Module.Controllers
{
  public class OrderStatusController : ApiController, IOrderStatusController
  {
    private readonly IOrderModule _orderModule;
    private readonly MapperConfiguration _config;

    public OrderStatusController(IOrderModule orderModule)
    {
      _orderModule = orderModule;
      _config = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<OrderStatus, OrderStatusDto>();
        cfg.CreateMap<OrderStatusHistory, OrderStatusHistoryDto>();
      });
    }

    /// <summary>
    /// Create a new OrderStatus
    /// /api/orders/orderstatus
    /// </summary>
    /// <param name="model"></param>
    [HttpPost]
    public IHttpActionResult Post([FromBody] OrderHistoryUpdateDto model)
    {
      using (var session = new OrderModuleSession())
      {
        try
        {
          var statusID = _orderModule.AddStatus(session, model.OrderID, model.StatusCode, model.Timestamp);

          return Ok(statusID);
        }
        catch (OrderStatusDuplicationException ex)
        {
          return BadRequest(ex.Message);
        }
      }
    }

    /// <summary>
    /// Create a new OrderStatus
    /// /api/orders/orderstatus
    /// </summary>
    /// <param name="model"></param>
    /// <param name="generateTimestamp"></param>
    [HttpPost]
    public IHttpActionResult Post([FromBody] OrderHistoryUpdateDto model, [FromUri] bool? generateTimestamp)
    {
      if (generateTimestamp.GetValueOrDefault())
      {
        model.Timestamp = DateTime.UtcNow;
      }
            
      return Post(model);
    }

    /// <summary>
    /// Get order status history
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("api/orderStatus/getStatusHistory/{orderID}")]
    public IEnumerable<OrderStatusHistory> GetStatusHistory([FromUri] int orderID)
    {
      using (var session = new OrderModuleSession())
      {
        var mapper = _config.CreateMapper();
        var history = _orderModule.GetStatusHistoryByOrderID(session,orderID)
          .Select(h => mapper.Map<OrderStatusHistory>(h));

        return history;
      }
    }

    /// <summary>
    /// Search order statuses
    /// </summary>
    /// <returns></returns>
    /// //GET: api/orderstatus
    [HttpGet]
    public IEnumerable<OrderStatusDto> Get()
    {
      using (var session = new OrderModuleSession())
      {
        return _orderModule.GetAllStatuses(session);
      }
    }
  }
}
