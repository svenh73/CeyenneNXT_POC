using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNxt.Orders.Shared.Objects;
using Microsoft.Practices.ServiceLocation;

namespace CeyenneNxt.Orders.Module.Modules
{
  public class BaseOrderModule : BaseModule
  {

    private IMapper _mapper;

    public IMapper Mapper
    {
      get
      {
        if (_mapper == null)
        {
          var config = new MapperConfiguration(cfg =>
          {
            cfg.CreateMap<OrderDto, Order>().ForMember(c => c.OrderType, c => c.Ignore())
              .AfterMap((vm, order) => order.OrderType = new OrderType
              {
                Name = vm.OrderType
              });
            cfg.CreateMap<Order, OrderDto>()
              .ForMember(c => c.OrderType, c => c.Ignore())
              .ForMember(c => c.AttributeValues, opt => opt.MapFrom(c => c.Attributes))
              .AfterMap((c, order) =>
              {
                order.OrderType = c.OrderType?.Name;
              });

            cfg.CreateMap<OrderLineDto, OrderLine>()
              .ForMember(c => c.QuantityUnit, opt => opt.MapFrom(c => c.OrderLineQuantityUnit));


            cfg.CreateMap<AddressTypeDto, CustomerAddressType>();
            cfg.CreateMap<CustomerAddressType, AddressTypeDto>();

            cfg.CreateMap<AddressDto, CustomerAddress>();
            cfg.CreateMap<CustomerAddress, AddressDto>();

            cfg.CreateMap<CountryDto, Country>();
            cfg.CreateMap<Country, CountryDto>();

            cfg.CreateMap(typeof(SearchResultDto<>), typeof(SearchResult<>));
            cfg.CreateMap(typeof(SearchResult<>), typeof(SearchResultDto<>));
            //cfg.CreateMap(typeof(SearchResult<OrderSearchResult>), typeof(SearchResultDto<OrderSearchResultDto>));
            cfg.CreateMap<OrderPagingFilterDto, OrderPagingFilter>();
            cfg.CreateMap<OrderSearchResultDto, OrderSearchResult>();
            cfg.CreateMap<OrderSearchResult, OrderSearchResultDto>();
            cfg.CreateMap<OrderLineDto, OrderLine>()
              .ForMember(c => c.QuantityUnit, opt => opt.MapFrom(c => c.OrderLineQuantityUnit));

            cfg.CreateMap<OrderLine, OrderLineDto>()
              .ForMember(c => c.AttributeValues, opt => opt.MapFrom(c => c.Attributes));

            cfg.CreateMap<CustomerDto, Customer>();
            cfg.CreateMap<Customer, CustomerDto>();

            cfg.CreateMap<AddressTypeDto, CustomerAddressType>();
            cfg.CreateMap<AddressDto, CustomerAddress>();


            cfg.CreateMap<CountryDto, Country>();

            cfg.CreateMap<OrderLineQuantityUnitDto, OrderLineQuantityUnit>();

            cfg.CreateMap<DashboardData, DashboardDataDto>();
            cfg.CreateMap<DayCount, DayCountDto>();
            cfg.CreateMap<OrderStatusHistory, OrderStatusHistoryDto>();
            cfg.CreateMap<OrderStatus, OrderStatusDto>();

            cfg.CreateMap<OrderAttributeValue, AttributeValueDto>()
              .AfterMap((src, dst) =>
              {
                dst.Name = src.Attribute.Name;
                dst.Code = src.Attribute.Code;
              });

            cfg.CreateMap<OrderLineAttributeValue, AttributeValueDto>()
              .AfterMap((src, dst) =>
              {
                dst.Name = src.Attribute.Name;
                dst.Code = src.Attribute.Code;
              });

            cfg.CreateMap<OrderType, OrderTypeDto>();
            cfg.CreateMap<OrderLineStatusHistory, OrderLineStatusHistoryDto>();
            cfg.CreateMap<OrderLineStatus, OrderLineStatusDto>();
          });
          _mapper = config.CreateMapper();
        }
        return _mapper;
      }
    }

    protected IOrderRepository _orderrepository;
    public IOrderRepository OrderRepository
    {
      get
      {
        if (_orderrepository == null)
        {
          _orderrepository = ServiceLocator.Current.GetInstance<IOrderRepository>();
        }
        return _orderrepository;
      }
    }

    protected ICustomersRepository _customers;
    public ICustomersRepository CustomersRepository
    {
      get
      {
        if (_customers == null)
        {
          _customers = ServiceLocator.Current.GetInstance<ICustomersRepository>();
        }
        return _customers;
      }
    }

    protected ICustomerAddressTypesRepository _customeraddresstypesrepository;
    public ICustomerAddressTypesRepository CustomerAddressTypesRepository
    {
      get
      {
        if (_customeraddresstypesrepository == null)
        {
          _customeraddresstypesrepository = ServiceLocator.Current.GetInstance<ICustomerAddressTypesRepository>();
        }
        return _customeraddresstypesrepository;
      }
    }

    protected ICustomerAddressesRepository _customeraddressesrepository;
    public ICustomerAddressesRepository CustomerAddressesRepository
    {
      get
      {
        if (_customeraddressesrepository == null)
        {
          _customeraddressesrepository = ServiceLocator.Current.GetInstance<ICustomerAddressesRepository>();
        }
        return _customeraddressesrepository;
      }
    }

    protected ICountryRepository _countryrepository;
    public ICountryRepository CountryRepository
    {
      get
      {
        if (_countryrepository == null)
        {
          _countryrepository = ServiceLocator.Current.GetInstance<ICountryRepository>();
        }
        return _countryrepository;
      }
    }

    protected IOrderTypesRepository _ordertypesrepository;
    public IOrderTypesRepository OrderTypesRepository
    {
      get
      {
        if (_ordertypesrepository == null)
        {
          _ordertypesrepository = ServiceLocator.Current.GetInstance<IOrderTypesRepository>();
        }
        return _ordertypesrepository;
      }
    }

    protected IOrderQuantityUnitsRepository _orderquantityunitsrepository;
    public IOrderQuantityUnitsRepository OrderQuantityUnitsRepository
    {
      get
      {
        if (_orderquantityunitsrepository == null)
        {
          _orderquantityunitsrepository = ServiceLocator.Current.GetInstance<IOrderQuantityUnitsRepository>();
        }
        return _orderquantityunitsrepository;
      }
    }

    protected IOrderLinesRepository _orderlinesrepository;
    public IOrderLinesRepository OrderLinesRepository
    {
      get
      {
        if (_orderlinesrepository == null)
        {
          _orderlinesrepository = ServiceLocator.Current.GetInstance<IOrderLinesRepository>();
        }
        return _orderlinesrepository;
      }
    }

    protected IOrderAddressesRepository _orderaddressesrepository;
    public IOrderAddressesRepository OrderAddressesRepository
    {
      get
      {
        if (_orderaddressesrepository == null)
        {
          _orderaddressesrepository = ServiceLocator.Current.GetInstance<IOrderAddressesRepository>();
        }
        return _orderaddressesrepository;
      }
    }

    protected IOrderAttributesRepository _orderattributesrepository;
    public IOrderAttributesRepository OrderAttributesRepository
    {
      get
      {
        if (_orderattributesrepository == null)
        {
          _orderattributesrepository = ServiceLocator.Current.GetInstance<IOrderAttributesRepository>();
        }
        return _orderattributesrepository;
      }
    }

    protected IOrderLineModule _orderlinemodule;
    public IOrderLineModule OrderLineModule
    {
      get
      {
        if (_orderlinemodule == null)
        {
          _orderlinemodule = ServiceLocator.Current.GetInstance<IOrderLineModule>();
        }
        return _orderlinemodule;
      }
    }

    protected IOrderStatusesRepository _orderstatusesrepository;
    public IOrderStatusesRepository OrderStatusesRepository
    {
      get
      {
        if (_orderstatusesrepository == null)
        {
          _orderstatusesrepository = ServiceLocator.Current.GetInstance<IOrderStatusesRepository>();
        }
        return _orderstatusesrepository;
      }
    }

    protected IOrderStatusHistoryRepository _orderStatusHistoryRepository;
    public IOrderStatusHistoryRepository OrderStatusHistoryRepository
    {
      get
      {
        if (_orderStatusHistoryRepository == null)
        {
          _orderStatusHistoryRepository = ServiceLocator.Current.GetInstance<IOrderStatusHistoryRepository>();
        }
        return _orderStatusHistoryRepository;
      }
    }

    protected IOrderLineStatusesRepository _orderlinestatusesrepository;
    public IOrderLineStatusesRepository OrderLineStatusesRepository
    {
      get
      {
        if (_orderlinestatusesrepository == null)
        {
          _orderlinestatusesrepository = ServiceLocator.Current.GetInstance<IOrderLineStatusesRepository>();
        }
        return _orderlinestatusesrepository;
      }
    }

    protected IOrderLineStatusHistoryRepository _orderlinestatushistoryrepository;
    public IOrderLineStatusHistoryRepository OrderLineStatusHistoryRepository
    {
      get
      {
        if (_orderlinestatushistoryrepository == null)
        {
          _orderlinestatushistoryrepository = ServiceLocator.Current.GetInstance<IOrderLineStatusHistoryRepository>();
        }
        return _orderlinestatushistoryrepository;
      }
    }

    protected IOrderLineAttributesRepository _orderlineattributesrepository;
    public IOrderLineAttributesRepository OrderLineAttributesRepository
    {
      get
      {
        if (_orderlineattributesrepository == null)
        {
          _orderlineattributesrepository = ServiceLocator.Current.GetInstance<IOrderLineAttributesRepository>();
        }
        return _orderlineattributesrepository;
      }
    }

    

  }
}
