using System.Web.Http;
using AutoMapper;
using Country = CeyenneNXT.Orders.Core.Entities.Country;
using CustomerAddressSelect = CeyenneNXT.Orders.Core.Objects.CustomerAddressSelect;
using OrderSearchResult = CeyenneNXT.Orders.Core.Objects.OrderSearchResult;

namespace CeyenneNxt.Orders.Module.Controllers
{
  public class CustomersController : BaseController
  {
    private readonly CustomerModule _customerModule;
    private readonly MapperConfiguration _config;

    public CustomersController()
    {
      _customerModule = new CustomerModule();
      _config = new MapperConfiguration(cfg => 
      {
        cfg.CreateMap<Core.Objects.CustomerSearchResult, CustomerSearchResult>();
        cfg.CreateMap<OrderSearchResult, CeyenneNXT.Orders.ApiContracts.Models.OrderSearchResult>();
        cfg.CreateMap<Country, CeyenneNXT.Orders.ApiContracts.Models.Country>();
        cfg.CreateMap(typeof(Core.Objects.SearchResult<>), typeof(SearchResult<>));
        cfg.CreateMap<CustomerAddress, Address>();
        cfg.CreateMap<CustomerAddressSelect, CeyenneNXT.Orders.ApiContracts.Models.CustomerAddressSelect>();
        cfg.CreateMap<CustomerPagingFilter, Core.Objects.CustomerPagingFilter>();
      });
    }

    public CustomerSearchResult Get(int id)
    {
      var customer = _customerModule.GetCustomerWithAddressesAndOrders(id);
      var mapper = _config.CreateMapper();
      var customerContract = mapper.Map<Core.Objects.CustomerSearchResult, CustomerSearchResult>(customer);
      return customerContract;
    }

    [HttpGet]
    [Route("api/customers/search")]
    public SearchResult<CustomerSearchResult> List([FromUri] CustomerPagingFilter filterContract)
    {
      var mapper = _config.CreateMapper();
      var filter = mapper.Map<Core.Objects.CustomerPagingFilter>(filterContract);
      var customersSearchResult = _customerModule.Search(filter);
      var customersSearchResultContract =
        mapper.Map<SearchResult<CustomerSearchResult>>(customersSearchResult);

      return customersSearchResultContract;
    }
  }
}