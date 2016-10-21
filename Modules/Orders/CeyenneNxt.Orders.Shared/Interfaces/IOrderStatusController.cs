using System.Collections.Generic;
using System.Web.Http;
using CeyenneNxt.Orders.Shared.Dtos.Messages;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNXT.Orders.WebApi.Controllers
{
  public interface IOrderStatusController
  {
    IEnumerable<OrderStatus> Get();
    IEnumerable<OrderStatusHistory> GetStatusHistory([FromUri] int orderID);
    IHttpActionResult Post([FromBody] OrderHistoryUpdateDto model);
    IHttpActionResult Post([FromBody] OrderHistoryUpdateDto model, [FromUri] bool? generateTimestamp);
  }
}