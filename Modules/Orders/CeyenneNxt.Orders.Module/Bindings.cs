using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Orders.Module.Modules;
using CeyenneNxt.Orders.Module.Processors;
using CeyenneNxt.Orders.Module.Repositories;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNxt.Products.Module.Modules;
using CeyenneNxt.Products.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using SimpleInjector;
using SimpleInjector.Advanced;

namespace CeyenneNxt.Orders.Module
{
  public class Bindings : BindingContainer
  {
    public override void AddBindings(Container container, ApplicationType applicationType)
    {
      var lifeStyle = ApplicationTypeToLifeStyle(applicationType);

      switch (applicationType)
      {
        case ApplicationType.Process:
          {
            container.Register<IOrderPublishProcessor, OrderPublishProcessor>(lifeStyle);
            container.Register<IOrderSubscribeProcessor, OrderSubscribeProcessor>(lifeStyle);
            break;
          }
        case ApplicationType.WebApi:
          {
            break;
          }
        case ApplicationType.WebUI:
          {
            break;
          }
      }

      container.Register<IOrderModule, OrderModule>(lifeStyle);
      container.Register<IOrderLineModule, OrderLineModule>(lifeStyle);

      container.Register<ICountryRepository, CountryRepository>(lifeStyle);
      container.Register<ICustomerAddressesRepository, CustomerAddressesRepository>(lifeStyle);
      container.Register<ICustomerAddressTypesRepository, CustomerAddressTypesRepository>(lifeStyle);
      container.Register<ICustomersRepository, CustomersRepository>(lifeStyle);
      container.Register<IOrderAddressesRepository, OrderAddressesRepository>(lifeStyle);
      container.Register<IOrderAttributesRepository, OrderAttributesRepository>(lifeStyle);
      container.Register<IOrderLinesRepository, OrderLinesRepository>(lifeStyle);
      container.Register<IOrderLineAttributesRepository, OrderLineAttributesRepository>(lifeStyle);
      container.Register<IOrderLineStatusesRepository, OrderLineStatusesRepository>(lifeStyle);
      container.Register<IOrderLineStatusHistoryRepository, OrderLineStatusHistoryRepository>(lifeStyle);
      container.Register<IOrderQuantityUnitsRepository, OrderQuantityUnitsRepository>(lifeStyle);
      container.Register<IOrderRepository, OrderRepository>(lifeStyle);
      container.Register<IOrderStatusesRepository, OrderStatusesRepository>(lifeStyle);
      container.Register<IOrderStatusHistoryRepository, OrderStatusHistoryRepository>(lifeStyle);
      container.Register<IOrderTypesRepository, OrderTypesRepository>(lifeStyle);
    }
  }
}
