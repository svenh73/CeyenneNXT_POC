using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CeyenneNxt.Core.Dtos.Settings;
using CeyenneNxt.Core.Entities;

namespace CeyenneNxt.Orders.Module
{
  public class SettingsMapper
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
            cfg.CreateMap<Setting, BaseSettingDto>();
            cfg.CreateMap<BaseSettingDto, Setting>();
            cfg.CreateMap<SettingValue, SettingValueDto>();
            cfg.CreateMap<SettingValueDto, SettingValue>();
          });
          instance = config.CreateMapper();
        }
        return instance;
      }
    }
  }
}
