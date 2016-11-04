using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Dtos;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Objects;

namespace CeyenneNxt.Orders.Module
{
  public class OrderMapper
  {
    private static IMapper instance;

    public static IMapper Mapper
    {
      get
      {
        if (instance == null)
        {
          var config = new MapperConfiguration(cfg =>
          {
            cfg.CreateMap<OrderDto, Order>();
            cfg.CreateMap<Order,OrderDto>();
            cfg.CreateMap<OrderLine, OrderLineDto>();
            cfg.CreateMap<OrderLineDto,OrderLine>();
          });
          instance = config.CreateMapper();
        }
        return instance;
      }
    }
  }
}
