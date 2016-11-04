using System.Collections.Generic;
using CeyenneNxt.Core.Entities;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Core.Dtos.Settings
{
  public abstract class BaseSettingDto : List<SettingValueDto>
  {

    public string Name { get; set; }

    public string Domain { get; set; }

    public bool Required { get; set; }

    public SettingDataType DataType { get; set; }

    public bool Active { get; set; } = true;

    public SettingType SettingType
    {
      get
      {
         if (this is GeneralSettingDto)
        {
          return SettingType.General;
        }
        else if (this is VendorSettingDto)
        {
          return SettingType.Vendor;
        }
        else if (this is ChannelSettingDto)
        {
          return SettingType.Channel;
        }
        else
        {
          return SettingType.Global;
        }
      }
    }

    public abstract SettingValueDto this[int? id] { get; }

    public abstract SettingValueDto Value { get; }

    public string DefaultValue { get; set; }

    public static BaseSettingDto LoadSetting(Setting setting)
    {
      BaseSettingDto newsetting = null;

      if (setting.SettingType == SettingType.General)
      {
        newsetting = new GeneralSettingDto()
        {
          Name = setting.Name,
          DataType = setting.DataType,
          Active = setting.Active,
          Domain = setting.Domain
        };
      }
      else if (setting.SettingType == SettingType.Vendor)
      {
        newsetting = new VendorSettingDto()
        {
          Name = setting.Name,
          DataType = setting.DataType,
          Active = setting.Active,
          Domain = setting.Domain
        };
      }
      else if (setting.SettingType == SettingType.Channel)
      {
        newsetting = new ChannelSettingDto()
        {
          Name = setting.Name,
          DataType = setting.DataType,
          Active = setting.Active,
          Domain = setting.Domain
        };
      }
      else //setting.SettingType == SettingType.Global
      {
        newsetting = new GlobalSettingDto()
        {
          Name = setting.Name,
          DataType = setting.DataType,
          Active = setting.Active
        };
      }

      foreach (var value in setting.SettingValues)
      {
        newsetting.Add(new SettingValueDto()
        {
          Value = value.Value,
          VendorID = value.VendorID,
          ChannelID = value.ChannelID,
          EnvironmentType = value.EnvironmentType
        });
      }
      return newsetting;
    }

    public ChannelSettingDto AsChannelSetting()
    {
      return (ChannelSettingDto) this;
    }

    public VendorSettingDto AsVendorSetting()
    {
      return (VendorSettingDto)this;
    }

    public GeneralSettingDto AsGeneralSetting()
    {
      return (GeneralSettingDto)this;
    }

    public GlobalSettingDto AsGlobalSetting()
    {
      return (GlobalSettingDto)this;
    }
  }
}
