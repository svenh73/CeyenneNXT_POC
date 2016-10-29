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

    public SettingDataType DateType { get; set; }

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

    public abstract string this [int? id] { get; }

    public abstract string Value { get; }

    public void LoadSetting(Setting setting)
    {
      foreach (var value in setting.SettingValues)
      {
        Add(new SettingValueDto()
        {
          Value = value.Value,
          VendorID = value.VendorID,
          ChannelID = value.ChannelID,
          EnvironmentType = value.EnviromentType
        });
      }
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
