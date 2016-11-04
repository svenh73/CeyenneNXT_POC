using System;
using System.Collections.Generic;
using System.Linq;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Dtos.Settings;
using CeyenneNxt.Core.Entities;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Module;
using CeyenneNxt.Orders.Module.Repositories;

namespace CeyenneNxt.Settings.CoreModule
{

  public class SettingModule : BaseModule, ISettingModule
  {
    public ISettingRepository SettingRepository { get; private set; }
    public ISettingValueRepository SettingValueRepository { get; private set; }

    private readonly Dictionary<string, SettingCollection> _settingsDictionary;

    public SettingModule(ISettingRepository settingRepository,ISettingValueRepository settingValueRepository)
    {
      _settingsDictionary = new Dictionary<string, SettingCollection>();

      SettingRepository = settingRepository;
      SettingValueRepository = settingValueRepository;
    }

    public SettingCollection LoadSettings(ISettingsModuleSession session, SettingCollection settingsdefinition)
    {
      var key = String.Format("{0}-{1}", settingsdefinition.Domain, settingsdefinition.EnvironmentType);

      if (!_settingsDictionary.ContainsKey(key))
      {
        var missingSettings = new List<BaseSettingDto>();

        var tempSettings = SettingRepository.LoadDomainSettings(session, settingsdefinition.Domain, settingsdefinition.EnvironmentType);

        var settings = new SettingCollection(settingsdefinition.Domain, settingsdefinition.EnvironmentType);

        foreach (var settingDto in settingsdefinition.Collection)
        {
          var result = tempSettings.OrderByDescending(p => p.Domain).FirstOrDefault(p => p.Name == settingDto.Name);
          if (result != null)
          {
            var newsettingDto = BaseSettingDto.LoadSetting(result);
            if (newsettingDto is VendorSettingDto || newsettingDto is ChannelSettingDto)
            {
              if (settingDto.Required && newsettingDto.Count(p => !String.IsNullOrEmpty(p.Value)) == 0)
              {
                missingSettings.Add(settingDto);
                InsertOrUpdateSetting(session, settingDto);
              }
              else
              {
                settings.Collection.Add(newsettingDto);
              }
            }
            else
            {
              if (settingDto.Required && newsettingDto[null] == null)
              {
                missingSettings.Add(settingDto);
                InsertOrUpdateSetting(session, settingDto);
              }
              else
              {
                settings.Collection.Add(newsettingDto);
              }
            }
            
          }
          else
          {
            if (settingDto.Required)
            {
              var id = InsertOrUpdateSetting(session,settingDto);

              if (String.IsNullOrEmpty(settingDto.DefaultValue))
              {
                missingSettings.Add(settingDto);
              }
              else
              {
                var newSetting = new Setting { Domain = settingDto.Domain, Active = true, DataType = settingDto.DataType, Name = settingDto.Name, SettingType = settingDto.SettingType, ID = id.GetValueOrDefault(),
                  SettingValues = new List<SettingValue>()
                  {
                    new SettingValue() {
                      SettingID = id.GetValueOrDefault(),
                      EnvironmentType = settingsdefinition.EnvironmentType,
                      Value = settingDto.DefaultValue
                  }}
                };
                settings.Collection.Add(BaseSettingDto.LoadSetting(newSetting));
              }
            }
          }
        }
        if (missingSettings.Count > 0)
        {
          throw new MissingSettingException(missingSettings);
        }

        _settingsDictionary[key] = settings;
      }
      return _settingsDictionary[key];
    }

    public int? InsertOrUpdateSetting(ISettingsModuleSession session, BaseSettingDto settingDto)
    {
      var setting =
          SettingRepository.Select(session, "where name = @name and (domain = @domain or domain is null) and (settingType = @settingtype or settingType is null)",
            new { name = settingDto.Name, domain = settingDto.Domain, settingtype = settingDto.SettingType })
            .FirstOrDefault();

      if (setting == null)
      {
        var newsetting = SettingsMapper.Mapper.Map<Setting>(settingDto);
        newsetting.Active = true;
        var id = SettingRepository.Insert(session, newsetting);
        SettingValueRepository.Insert(session, new SettingValue()
        {
          SettingID = id.Value,
          Value = settingDto.DefaultValue ?? String.Empty
        });
        return id;
      }
      else
      {
        var newsetting = SettingsMapper.Mapper.Map<Setting>(settingDto);
        newsetting.ID = setting.ID;
        return SettingRepository.Update(session, newsetting);
      }
    }

    public bool DeleteSetting(ISettingsModuleSession session, BaseSettingDto settingDto)
    {
      var existingSetting =
        SettingRepository.Select(session, "where name = @name and (domain = @domain or domain is null) and (settingType = @settingtype or settingType is null)",
          new {name = settingDto.Name, domain = settingDto.Domain, settingtype = settingDto.SettingType})
          .FirstOrDefault();

      if (existingSetting != null)
      {
        return SettingRepository.Delete(session,existingSetting) > 0;
      }
      return false;
    }

    public bool DeleteGlobalSettingValue(ISettingsModuleSession session, string settingName, EnvironmentType? environmentType, int? vendorOrChannelID)
    {
      return DeleteSettingValue(session, null, settingName, SettingType.Global, environmentType, null);
    }

    public bool DeleteVendorSettingValue(ISettingsModuleSession session, string domain, string settingName, EnvironmentType? environmentType, int? vendorOrChannelID)
    {
      return DeleteSettingValue(session, domain, settingName, SettingType.Vendor, environmentType, vendorOrChannelID);
    }

    public bool DeleteChannelSettingValue(ISettingsModuleSession session, string domain, string settingName, EnvironmentType? environmentType, int? vendorOrChannelID)
    {
      return DeleteSettingValue(session, domain, settingName, SettingType.Channel, environmentType, vendorOrChannelID);
    }

    public bool DeleteGeneralSettingValue(ISettingsModuleSession session, string domain, string settingName, EnvironmentType? environmentType, int? vendorOrChannelID)
    {
      return DeleteSettingValue(session, domain, settingName, SettingType.General, environmentType, null);
    }

    private bool DeleteSettingValue(ISettingsModuleSession session, string domain, string settingName, SettingType settingType, EnvironmentType? environmentType, int? vendorOrChannelID)
    {
      Setting setting = null;
      if (settingType == SettingType.Global)
      {
        domain = null;

        setting = SettingRepository.Select(session,
          "where name = @name and domain is null and (settingType = @settingtype or settingType is null)",
          new { name = settingName, settingtype = settingType }).FirstOrDefault();
      }
      else
      {
        setting = SettingRepository.Select(session,
          "where name = @name and (domain = @domain or domain is null) and (settingType = @settingtype or settingType is null)",
          new { name = settingName, domain = domain, settingtype = settingType }).FirstOrDefault();
      }

      if (setting != null)
      {
        var settingvalue = SettingValueRepository.Select(session, "where settingid = @settingid and EnvironmentType = @environmenttype",
          new { settingid = setting.ID, environmenttype = settingType }).FirstOrDefault();
        if (settingvalue != null)
        {
          SettingValueRepository.Delete(session, settingvalue);
          if (!SettingValueRepository.Any(session, "where settingid = @settingid", new {settingid = setting.ID}))
          {
            SettingRepository.Delete(session, setting);
          }
        }

        return true;
      }
      else
      {
        return false;
      }
    }


    public bool InsertOrUpdateGlobalSettingValue(ISettingsModuleSession session, string settingName,
      EnvironmentType? environmentType, string value)
    {
      return InsertOrUpdateSettingValue(session, null, settingName, SettingType.Global, environmentType,null, value);
    }

    public bool InsertOrUpdateVendorSettingValue(ISettingsModuleSession session, string domain, string settingName,
      EnvironmentType? environmentType,int vendorID, string value)
    {
      return InsertOrUpdateSettingValue(session, domain, settingName, SettingType.Vendor, environmentType, vendorID, value);
    }

    public bool InsertOrUpdateChannelSettingValue(ISettingsModuleSession session, string domain, string settingName,
      EnvironmentType? environmentType, int channelID, string value)
    {
      return InsertOrUpdateSettingValue(session, domain, settingName, SettingType.Channel, environmentType, channelID, value);
    }

    public bool InsertOrUpdateGeneralSettingValue(ISettingsModuleSession session, string domain, string settingName,
      EnvironmentType? environmentType, string value)
    {
      return InsertOrUpdateSettingValue(session, domain, settingName, SettingType.General, environmentType, null, value);
    }

    private bool InsertOrUpdateSettingValue(ISettingsModuleSession session, string domain, string settingName, SettingType settingType,
      EnvironmentType? environmentType, int? vendorOrChannelID, string value)
    {
      Setting setting = null;
      if (settingType == SettingType.Global)
      {
        domain = null;

        setting = SettingRepository.Select(session,
          "where name = @name and domain is null and (settingType = @settingtype or settingType is null)",
          new { name = settingName, settingtype = settingType }).FirstOrDefault();
      }
      else
      {
        setting = SettingRepository.Select(session,
          "where name = @name and (domain = @domain or domain is null) and (settingType = @settingtype or settingType is null)",
          new { name = settingName, domain = domain, settingtype = settingType }).FirstOrDefault();
      }
        
      if (setting == null)
      {
        var id = SettingRepository.Insert(session, new Setting() {Domain = domain, Name = settingName, SettingType = settingType,Active = true});
        setting = SettingRepository.SelectByID(session, id.Value);
      }

      SettingValue settingvalue = QuerySettingValue(session, setting.ID, environmentType, settingType, vendorOrChannelID);
      if (settingvalue != null)
      {
        settingvalue.Value = value;
        return SettingValueRepository.Update(session, settingvalue) > 0;
      }
      else
      {
        return SettingValueRepository.Insert(session, new SettingValue()
        {
          SettingID = setting.ID,
          EnvironmentType = environmentType,
          Value = value,
          ChannelID = settingType == SettingType.Channel ? vendorOrChannelID : null,
          VendorID = settingType == SettingType.Vendor ? vendorOrChannelID : null
        }) > 0;
      }
    }

    private SettingValue QuerySettingValue(IModuleSession session,int settingId, EnvironmentType? environment, SettingType settingType, int? id = null)
    {
      if (environment == null)
      {
        if (settingType == SettingType.Vendor && id.HasValue)
        {
          return SettingValueRepository.Select(session, "where settingid = @settingid and EnvironmentType is null and VendorID=@VendorID",
          new { settingid = settingId, VendorID = id }).FirstOrDefault();
        }
        else if (settingType == SettingType.Channel && id.HasValue)
        {
          return SettingValueRepository.Select(session, "where settingid = @settingid and EnvironmentType is null and ChannelID=@ChannelID",
          new { settingid = settingId, ChannelID = id}).FirstOrDefault();
        }
        else
        {
          return SettingValueRepository.Select(session, "where settingid = @settingid and EnvironmentType is null",
          new { settingid = settingId }).FirstOrDefault();
        }
        
      }
      else
      {
        if (settingType == SettingType.Channel && id.HasValue)
        {
          return SettingValueRepository.Select(session, "where settingid = @settingid and EnvironmentType = @environmenttype and VendorID=@VendorID",
          new { settingid = settingId, environmenttype = environment, VendorID = id }).FirstOrDefault();
        }
        else if (settingType == SettingType.Channel && id.HasValue)
        {
          return SettingValueRepository.Select(session, "where settingid = @settingid and EnvironmentType = @environmenttype and ChannelID=@ChannelID",
          new { settingid = settingId, environmenttype = environment, ChannelID = id }).FirstOrDefault();
        }
        else
        {
          return SettingValueRepository.Select(session, "where settingid = @settingid and EnvironmentType = @environmenttype",
          new { settingid = settingId, environmenttype = environment }).FirstOrDefault();
        }
      }
    }


    public bool InsertOrUpdateSettingValue(ISettingsModuleSession session, string settingName, SettingType settingType, EnvironmentType? environmentType, int? vendorOrChannelID,string value)
    {
      var setting = SettingRepository.LoadSetting(session,settingName, settingType,environmentType.Value);
      if (setting != null)
      {

        Func<SettingValue, bool> query = null;
        if (settingType == SettingType.Vendor)
        {
          query =
            settingValue =>
              settingValue.VendorID == vendorOrChannelID;
        }
        else if (settingType == SettingType.Channel)
        {
          query =
            settingValue =>
              settingValue.VendorID == vendorOrChannelID;
        }

        var settingvalue = setting.SettingValues.FirstOrDefault(query);
        if (settingvalue != null)
        {
          settingvalue.Value = value;
          SettingValueRepository.Update(session, settingvalue);
        }
        else
        {
          settingvalue = new SettingValue()
          {
            SettingID = setting.ID,
            EnvironmentType = environmentType,
            Value = value,
            VendorID = settingType == SettingType.Vendor ? vendorOrChannelID : null,
            ChannelID = settingType == SettingType.Channel ? vendorOrChannelID : null,
          };
          
          
          setting.SettingValues.Add(settingvalue);

          SettingValueRepository.Insert(session, settingvalue);

        }

        return true;
      }
      return false;
    }
  }
}
