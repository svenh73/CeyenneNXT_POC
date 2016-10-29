using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using CeyenneNXT.Invoices.Core.Enums;
using CeyenneNXT.Invoices.Module;
using CeyenneNXT.Orders.ApiContracts.Models;
using CeyenneNXT.Orders.DataAccess;

namespace CeyenneNXT.Orders.WebApi.Controllers
{
  public class RefundsController : BaseController
  {
    private readonly RefundModule _refundModule;
    private readonly Invoice _invoiceModule;
    private readonly MapperConfiguration _config;

    public RefundsController()
    {
      _refundModule = new RefundModule();
      _invoiceModule = new Invoice();
      _config = new MapperConfiguration(cfg => 
      {
        cfg.CreateMap<Core.Objects.Refund, Refund>();
        cfg.CreateMap<Core.Objects.RefundSearchResult, RefundSearchResult>();
        cfg.CreateMap<Core.Entities.ReturnCode, ReturnCode>();
        cfg.CreateMap<Core.Entities.PaymentMethod, PaymentMethod>();
        cfg.CreateMap<Core.Entities.RefundStatus, RefundStatus>();
        cfg.CreateMap<Refund, Core.Objects.Refund>();
        cfg.CreateMap<RefundPagingFilter, Core.Objects.RefundPagingFilter>();
        cfg.CreateMap(typeof(Core.Objects.SearchResult<>), typeof(SearchResult<>));
      });
    }

    [HttpGet]
    [Route("api/refunds/getByOrderID/{orderID}")]
    public List<Refund> GetByOrderID(int orderID)
    {
      var mapper = _config.CreateMapper();
      var refunds = _refundModule.GetByOrderID(orderID);
      var refundContracts = mapper.Map<List<Refund>>(refunds);

      return refundContracts;
    }

    [HttpGet]
    [Route("api/refunds/GetRefundStatuses")]
    public List<RefundStatus> GetRefundStatuses()
    {
      var mapper = _config.CreateMapper();
      var refundStatuses = _refundModule.GetRefundStatuses();
      var refundStatusContracts = mapper.Map<List<RefundStatus>>(refundStatuses);

      return refundStatusContracts;
    }

    [HttpGet]
    [Route("api/refunds/GetPaymentMethods")]
    public List<PaymentMethod> GetPaymentMethods()
    {
      var mapper = _config.CreateMapper();
      var paymentMethods = _refundModule.GetPaymentMethods();
      var paymentMethodContracts = mapper.Map<List<PaymentMethod>>(paymentMethods);

      return paymentMethodContracts;
    }

    [HttpGet]
    [Route("api/refunds/GetReturnCodes")]
    public List<ReturnCode> GetReturnCodes()
    {
      var mapper = _config.CreateMapper();
      var returnCodes = _refundModule.GetReturnCodes();
      var returnCodeContracts = mapper.Map<List<ReturnCode>>(returnCodes);

      return returnCodeContracts;
    }

    [HttpPost]
    public void Post(Refund refundContract)
    {
      if (refundContract == null)
      {
        throw new NullReferenceException("refundContract is null");
      }
      if (refundContract.Amount.HasValue && refundContract.Amount.Value > 0 && string.IsNullOrWhiteSpace(refundContract.Invoice))
      {
        var invoice = _invoiceModule.GenerateInvoiceNumberForEntity(InvoiceEntityTypes.Refund, refundContract.ID);
        refundContract.Invoice = invoice;
      }
      var mapper = _config.CreateMapper();

      var refund = mapper.Map<Core.Objects.Refund>(refundContract);
      _refundModule.Create(refund);
    }

    [HttpPut]
    [Route("api/refunds/update")]
    public void Put(Refund refundContract)
    {
      if (refundContract == null)
      {
        throw new NullReferenceException("refundContract is null");
      }
      if (refundContract.Amount.HasValue && refundContract.Amount.Value > 0 && string.IsNullOrWhiteSpace(refundContract.Invoice))
      {
        var invoice = _invoiceModule.GenerateInvoiceNumberForEntity(InvoiceEntityTypes.Refund, refundContract.ID);
        refundContract.Invoice = invoice;
      }
      var mapper = _config.CreateMapper();

      var refund = mapper.Map<Core.Objects.Refund>(refundContract);
      _refundModule.Update(refund);
    }

    [HttpGet]
    [Route("api/refunds/search")]
    public SearchResult<RefundSearchResult> List([FromUri] RefundPagingFilter filterContract)
    {
      var mapper = _config.CreateMapper();
      var filter = mapper.Map<Core.Objects.RefundPagingFilter>(filterContract);
      var customersSearchResult = _refundModule.Search(filter);
      var customersSearchResultContract =
        mapper.Map<SearchResult<RefundSearchResult>>(customersSearchResult);

      return customersSearchResultContract;
    }
  }
}