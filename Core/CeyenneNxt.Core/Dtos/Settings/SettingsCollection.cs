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

    public SettingCollection(string domain, EnvironmentType environmentType, List<BaseSettingDto> settings) : this(domain,environmentType)
    {
      foreach (var setting in settings)
      {
        Collection.Add(setting);
      }
    }

    public BaseSettingDto this[string name]
    {
      get
      {
        return Collection.FirstOrDefault(p => p.Name == name);
      }
    }
  }
}
