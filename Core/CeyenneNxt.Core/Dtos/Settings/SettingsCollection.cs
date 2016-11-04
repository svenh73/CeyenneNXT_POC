using System;
using System.Collections.Generic;
using System.Linq;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Core.Dtos.Settings
{
  public class SettingCollection
  {
    public List<BaseSettingDto> Collection { get; private set; }

    public string Domain { get; private set; }

    public EnvironmentType EnvironmentType { get; private set; }

    public SettingCollection(string domain, EnvironmentType environmentType)
    {
      Collection = new List<BaseSettingDto>();
      Domain = domain;
      EnvironmentType = environmentType;
    }

    public SettingCollection Add(BaseSettingDto setting)
    {
      this.Collection.Add(setting);
      return this;
    }

    public SettingValueDto this[string name,int? id = null]
    {
      get
      {
        var setting = Collection.FirstOrDefault(p => p.Name == name);
        if (setting != null)
        {
          return setting[id];
        }
        else
        {
          return new SettingValueDto();
        }
      }
    }
  }
}
