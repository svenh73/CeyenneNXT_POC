using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using CeyenneNXT.Orders.ApiContracts.Models;
using CeyenneNXT.Orders.DataAccess;
using RefundStatus = CeyenneNXT.Orders.Core.Entities.RefundStatus;

namespace CeyenneNXT.Orders.WebApi.Controllers
{
  public class RefundStatusesController : BaseController
  {
    private readonly RefundStatusHistoriesModule _refundStatusHistoriesModule;
    private readonly MapperConfiguration _config;

    public RefundStatusesController()
    {
      _refundStatusHistoriesModule = new RefundStatusHistoriesModule();
      _config = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Core.Entities.RefundStatusHistory, RefundStatusHistory>();
        cfg.CreateMap<RefundStatusHistory, Core.Entities.RefundStatusHistory>();
        cfg.CreateMap<RefundStatus, ApiContracts.Models.RefundStatus>();
        cfg.CreateMap<ApiContracts.Models.RefundStatus, RefundStatus>();
      });
    }

    /// <summary>
    /// Create new Refund Status State
    /// /api/refundStatuses/
    /// </summary>
    /// <param name="refundStatusHistoryContract"></param>
    [HttpPost]
    public void Post([FromBody] RefundStatusHistory refundStatusHistoryContract)
    {
      refundStatusHistoryContract.Timestamp = DateTime.Now;
      var mapper = _config.CreateMapper();
      var refundStatusHistory = mapper.Map<Core.Entities.RefundStatusHistory>(refundStatusHistoryContract);
      _refundStatusHistoriesModule.Create(refundStatusHistory);
    }

    [HttpGet]
    [Route("api/refundStatuses/getByRefundID/{refundID}")]
    public List<RefundStatusHistory> GetByRefundID(int refundID)
    {
      var mapper = _config.CreateMapper();
      var refundStatuses = _refundStatusHistoriesModule.GetByRefundID(refundID);
      var refundStatusesContract = mapper.Map<List<RefundStatusHistory>>(refundStatuses);

      return refundStatusesContract;
    }
  }
}
