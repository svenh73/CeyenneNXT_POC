using System.Collections.Generic;
using System.Web.Http;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Dtos.Messages;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.ApiContracts.Models.CreateModels;

namespace CeyenneNxt.Orders.Module.Controllers
{
  public interface IOrdersController
  {
    IOrderModule OrderModule { get; }

    IHttpActionResult AddOrderLineAttribute(OrderAttributeDto model);
    OrderDto Get(int id);
    IEnumerable<OrderTypeDto> GetAllTypes();
    OrderDto GetByExternalIdentifier(string externalID);
    DashboardDataDto GetDashboardData();
    List<OrderDto> GetNotDispatchedOrders();
    IEnumerable<int> GetOrderByLatestStatus(string statusCode);
    IEnumerable<int> GetOrdersBetweenStatuses(string statusCodeWith, string statusCodeWithout);
    OrderDto MapOrderToOrderContract(Order order);
    OrderDto Post([FromBody] OrderDto order);
    SearchResultDto<OrderSearchResultDto> Search([FromUri] OrderPagingFilterDto filter);
    void UpdateOrderDispatched([FromBody] SetOrderDispatchedDto model);
  }
}