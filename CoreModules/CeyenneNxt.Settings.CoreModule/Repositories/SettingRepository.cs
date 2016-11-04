using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Entities;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.Types;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class SettingRepository : BaseRepository<Setting>, ISettingRepository
  {
    public SettingRepository() : base(SchemaConstants.Default)
    {

    }

    public Setting LoadSetting(ISettingsModuleSession session, string name, SettingType settingType, EnvironmentType environment)
    {
      var setting = Select(session, "where name = @name and settingtype = @settingtype", new {name = name, settingtype = settingType })
        .OrderBy(p => p.SettingType).FirstOrDefault();
      if (setting != null)
      {
        var values = session.Connection.Query<SettingValue>("select * from settingvalue where settingid = @settingid and (EnviromentType = @EnviromentType or EnviromentType is null)", new { settingid = setting.ID, EnviromentType = environment })
          .OrderByDescending(p => p.EnvironmentType).ToList();
        values.ForEach(p =>
        {
          if (setting.SettingValues.All(o => o.SettingID != p.SettingID))
          {
            setting.SettingValues.Add(p);
          }
        });
        
      }
      return setting;
    }

    public List<Setting> LoadDomainSettings(ISettingsModuleSession session, string domain, EnvironmentType environment)
    {
      var tempsettings = Select(session, "Where active = 1 and (domain = @domain or domain is null)", new { domain = domain })
        .OrderByDescending(p => p.Domain).ToList();
      var settings = new List<Setting>();

      tempsettings.ForEach(p =>
      {
        if (settings.All(o => o.Name != p.Name))
        {
          settings.Add(p);
        }
      });

      foreach (var setting in settings)
      {
        var values = session.Connection.Query<SettingValue>("select * from [def].[settingvalue] where settingid = @settingid and (EnvironmentType = @EnvironmentType or EnvironmentType is null)", 
          new { settingid = setting.ID, EnvironmentType = environment })
          .OrderByDescending(p => p.EnvironmentType).ToList();
        values.ForEach(p =>
        {
          setting.SettingValues.Add(p);
        });
      }
      return settings;
    }

  }
}