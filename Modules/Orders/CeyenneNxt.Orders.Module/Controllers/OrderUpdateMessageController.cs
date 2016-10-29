using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using CeyenneNXT.Orders.Core.Entities;
using CeyenneNXT.Orders.DataAccess;
using OrderUpdateMessage = CeyenneNXT.Orders.ApiContracts.Models.CreateModels.OrderUpdateMessage;

namespace CeyenneNXT.Orders.WebApi.Controllers
{
  public class OrderUpdateMessageController : BaseController
  {
    private readonly MapperConfiguration _config;
    public OrderUpdateMessageController()
    {
      _config = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Core.Entities.OrderUpdateMessage, ApiContracts.Models.OrderUpdateMessage>();

        cfg.CreateMap<OrderUpdateMessageLine, ApiContracts.Models.OrderUpdateMessageLine>()
          .ForMember(c => c.StatusHistory, opt => opt.MapFrom(l => l.OrderLineStatusHistory));

        cfg.CreateMap<OrderStatus, ApiContracts.Models.OrderStatus>();

        cfg.CreateMap<OrderStatusHistory, ApiContracts.Models.OrderStatusHistory>();

        cfg.CreateMap<OrderLine, ApiContracts.Models.OrderLine>();

        cfg.CreateMap<OrderLineStatusHistory, ApiContracts.Models.OrderLineStatusHistory>();

        cfg.CreateMap<OrderLineStatus, ApiContracts.Models.OrderLineStatus>();

        //vice versa
        cfg.CreateMap<ApiContracts.Models.OrderUpdateMessage, Core.Entities.OrderUpdateMessage>();

        cfg.CreateMap<ApiContracts.Models.OrderUpdateMessageLine, OrderUpdateMessageLine>()
          .ForMember(c => c.OrderLineStatusHistory, opt => opt.MapFrom(l => l.StatusHistory));

        cfg.CreateMap<ApiContracts.Models.OrderStatus, OrderStatus>();

        cfg.CreateMap<ApiContracts.Models.OrderStatusHistory, OrderStatusHistory>();

        cfg.CreateMap<ApiContracts.Models.OrderLine, OrderLine>();

        cfg.CreateMap<ApiContracts.Models.OrderLineStatusHistory, OrderLineStatusHistory>();

        cfg.CreateMap<ApiContracts.Models.OrderLineStatus, OrderLineStatus>();
      });
    }

    [HttpPost]
    [Route("api/orderUpdateMessage")]
    public IHttpActionResult Create([FromBody] OrderUpdateMessage model)
    {
      try
      {
        var messageModule = new OrderUpdateMessageModule();
        messageModule.CreateUpdateMessage(model.OrderID, model.OrderStatusHistoryID, false, model.Timestamp, model.OrderLineStatusHistoryIDs);
      }
      catch (Exception ex)
      {
        var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
          Content = new StringContent(ex.Message),
          ReasonPhrase = "Error adding an Order Update Message"
        };

        throw new HttpResponseException(resp);
      }

      return Ok();
    }

    [HttpGet]
    [Route("api/orderUpdateMessage/open")]
    public IHttpActionResult GetNotProcessed()
    {
      try
      {
        var messageModule = new OrderUpdateMessageModule();

        var messages = messageModule.GetNotProcessed();
        var mapper = _config.CreateMapper();

        var apiMessages = mapper.Map<List<ApiContracts.Models.OrderUpdateMessage>>(messages);
        return Ok(apiMessages);
      }
      catch (Exception ex)
      {
        var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
          Content = new StringContent(ex.Message),
          ReasonPhrase = "Error adding an Order Update Message"
        };

        throw new HttpResponseException(resp);
      }
    }

    [HttpPost]
    [Route("api/orderUpdateMessage/update")]
    public IHttpActionResult Update([FromBody]ApiContracts.Models.UpdateModels.OrderUpdateMessage model)
    {
      try
      {
        var messageModule = new OrderUpdateMessageModule();
        messageModule.Toggle(model.OrderUpdateMessageID, model.Processed);
        
        return Ok();
      }
      catch (Exception ex)
      {
        var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
          Content = new StringContent(ex.Message),
          ReasonPhrase = "Error adding an Order Update Message"
        };

        throw new HttpResponseException(resp);
      }
    }
  }
}
