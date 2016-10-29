using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using OrderLine = CeyenneNXT.Orders.Core.Entities.OrderLine;
using OrderLineAttribute = CeyenneNXT.Orders.ApiContracts.Models.CreateModels.OrderLineAttribute;
using OrderLineQuantityUnit = CeyenneNXT.Orders.Core.Entities.OrderLineQuantityUnit;
using OrderLineStatus = CeyenneNXT.Orders.Core.Entities.OrderLineStatus;
using OrderLineStatusHistory = CeyenneNXT.Orders.Core.Entities.OrderLineStatusHistory;

namespace CeyenneNxt.Orders.Module.Controllers
{
  public class OrderLinesController : BaseController
  {
    private readonly OrderLineModule _orderLineModule;
    private readonly MapperConfiguration _config;

    public OrderLinesController()
    {
      _orderLineModule = new OrderLineModule();
      _config = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<OrderLine, CeyenneNXT.Orders.ApiContracts.Models.OrderLine>()
          .ForMember(c => c.OrderLineQuantityUnit, opt => opt.MapFrom(c => c.QuantityUnit))
          .ForMember(c => c.AttributeValues, opt => opt.MapFrom(c => c.Attributes));

        cfg.CreateMap<OrderLineQuantityUnit, CeyenneNXT.Orders.ApiContracts.Models.OrderLineQuantityUnit>();

        cfg.CreateMap<OrderLineAttributeValue, AttributeValue>()
          .AfterMap((src, dst) =>
          {
            dst.Name = src.Attribute.Name;
            dst.Code = src.Attribute.Code;
          });

        cfg.CreateMap<OrderLineStatusHistory, CeyenneNXT.Orders.ApiContracts.Models.OrderLineStatusHistory>();
        cfg.CreateMap<OrderLineStatus, CeyenneNXT.Orders.ApiContracts.Models.OrderLineStatus>();
      });
    }


    /// <summary>
    /// Retrieves an order line by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/orderLines/5
    [HttpGet]
    public IHttpActionResult Get(int id)
    {
      var orderLine = _orderLineModule.GetFullByID(id);
      var mapper = _config.CreateMapper();
      var orderLineContract = mapper.Map<CeyenneNXT.Orders.ApiContracts.Models.OrderLine>(orderLine);

      return Ok(orderLineContract);
    }

    /// <summary>
    /// Retrieves all order line statuses
    /// </summary>
    /// <returns></returns>
    // GET: api/orderLines/getAllStatuses
    [HttpGet]
    [Route("api/orderLines/getAllStatuses")]
    public IHttpActionResult GetAllStatuses()
    {
      var statuses = _orderLineModule.GetAllStatuses();
      var mapper = _config.CreateMapper();
      var statusesContract = statuses.Select(s => mapper.Map<CeyenneNXT.Orders.ApiContracts.Models.OrderLineStatus>(s));

      return Ok(statusesContract);
    }

    /// <summary>
    /// Creates new order line status
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    // POST: api/orderLines/createStatusHistory
    [HttpPost]
    [Route("api/orderLines/createStatusHistory")]
    public IHttpActionResult CreateStatusHistory(OrderLineHistoryUpdate model)
    {
      if (ModelState.IsValid)
      {
        var status = MemoryCache<IEnumerable<OrderLineStatus>>.GetData(_orderLineModule.GetAllStatuses)
          .FirstOrDefault(s => s.Code == model.StatusCode);

        if (status == null)
        {
          return BadRequest("Invalid status code.");
        }

        if (status.QuantityRequired && !model.QuantityChanged.HasValue)
        {
          return BadRequest("Invalid quantity value for this status.");
        }

        var result = _orderLineModule.AddStatusHistory(model.OrderLineID, model.StatusCode, model.QuantityChanged, model.Timestamp, model.Message);

        return Ok(result);
      }
      return BadRequest(ModelState);
    }

    /// <summary>
    /// Get order line status history
    /// </summary>
    /// <param name="orderLineID"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("api/orderLines/getStatusHistory/{orderLineID}")]
    public IHttpActionResult GetStatusHistory([FromUri] int orderLineID)
    {
      //IEnumerable<OrderStatusHistory>
      var mapper = _config.CreateMapper();
      var history = _orderLineModule.GetStatusHistoryByOrderLineID(orderLineID)
        .Select(h => mapper.Map<CeyenneNXT.Orders.ApiContracts.Models.OrderLineStatusHistory>(h));

      return Ok(history);
    }

    [HttpPost]
    [Route("api/orderLines/createAttribute")]
    public IHttpActionResult AddOrderLineAttribute(OrderLineAttribute model)
    {
      try
      {
        _orderLineModule.AddAttribute(model.OrderLineID, model.AttributeCode, model.AttributeName, model.AttributeValue);

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