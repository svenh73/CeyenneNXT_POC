using System;
using System.Collections.Generic;
using CeyenneNxt.Core.Dtos.Settings;
using CeyenneNxt.Core.Entities;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Settings.CoreModule
{

  public class SettingModule : BaseModule, ISettingModule
  {
    //  public ISettingsRepository SettingsRepository { get; private set; } 

    //  public SettingModule(ISettingsRepository settingsRepository)
    //  {
    //    SettingsRepository = settingsRepository;
    //  }

    //  public SettingCollection LoadSettings(SettingCollection settingsdefinition)
    //  {
    //    if (!_settingsDictionary.ContainsKey(settingsdefinition.Domain))
    //    {
    //      var missingSettings = new List<BaseSettingDto>();

    //      var tempSettings = _settingUnitOfWork.SettingRepository.GetItems(setting => setting.Active && (setting.Domain == settingsdefinition.Domain || setting.Domain == null));

    //      foreach (var settingDto in settingsdefinition.Collection)
    //      {
    //        var result = tempSettings.OrderByDescending(p => p.Domain).FirstOrDefault(p => p.Name == settingDto.Name);
    //        if (result != null)
    //        {
    //          settingDto.LoadSetting(result);
    //          if (settingDto.Required && settingDto[null] == null)
    //          {
    //            missingSettings.Add(settingDto);
    //            InsertOrUpdateSetting(settingDto);
    //          }
    //        }
    //        else
    //        {
    //          if (settingDto.Required)
    //          {
    //            missingSettings.Add(settingDto);
    //            InsertOrUpdateSetting(settingDto);
    //          }
    //        }
    //      }
    //      if (missingSettings.Count > 0)
    //      {
    //        throw new MissingSettingException(missingSettings);
    //      }

    //      _settingsDictionary[settingsdefinition.Domain] = settingsdefinition;
    //    }
    //    return _settingsDictionary[settingsdefinition.Domain];
    //  }

    //  public bool InsertOrUpdateSetting(BaseSettingDto settingDto)
    //  {
    //    if (
    //      !_settingUnitOfWork.SettingRepository.GetItems(s => s.Name == settingDto.Name && s.Domain == settingDto.Domain && s.SettingType == settingDto.SettingType)
    //        .Any())
    //    {
    //      var setting = _mapper.Map<Setting>(settingDto);
    //      _settingUnitOfWork.SettingRepository.Insert(setting);
    //    }
    //    return true;
    //  }

    //  public bool DeleteSetting(BaseSettingDto settingDto)
    //  {
    //    var existingSetting = _settingUnitOfWork.SettingRepository.GetItems(s => s.Name == settingDto.Name && s.Domain == settingDto.Domain && s.SettingType == settingDto.SettingType).FirstOrDefault();
    //    if (existingSetting != null)
    //    {
    //      var result = _settingUnitOfWork.SettingRepository.Delete(existingSetting);
    //      return result.Success;
    //    }
    //    return false;
    //  }

    //  public bool InsertOrUpdateSettingValue(string domain, string settingName, SettingType settingType,
    //    EnvironmentType? environmentType, int? vendorOrChannelID, string value)
    //  {
    //    throw new NotImplementedException();
    //  }

    //  public bool DeleteSettingValue(string domain, string settingName, SettingType settingType, EnvironmentType? environmentType,
    //    int? vendorOrChannelID)
    //  {
    //    throw new NotImplementedException();
    //  }

    //  public bool DeleteSettingValue(string settingName, SettingType settingType, EnvironmentType? environmentType, int? vendorOrChannelID)
    //  {
    //    var result = SettingsRepository.GetItems(setting => setting.Name == settingName && setting.SettingType == settingType).FirstOrDefault();
    //    if (result != null)
    //    {
    //      Func<SettingValue, bool> query = null;
    //      if (settingType == SettingType.Vendor)
    //      {
    //        query =
    //          settingValue =>
    //            settingValue.EnviromentType == environmentType && settingValue.VendorID == vendorOrChannelID;
    //      }
    //      else if (settingType == SettingType.Channel)
    //      {
    //        query =
    //          settingValue =>
    //            settingValue.EnviromentType == environmentType && settingValue.VendorID == vendorOrChannelID;
    //      }
    //      else
    //      {
    //        query =
    //          settingValue =>
    //            settingValue.EnviromentType == environmentType;
    //      }

    //      var settingvalue = result.SettingValues.FirstOrDefault(query);
    //      if (settingvalue != null)
    //      {
    //        return _settingUnitOfWork.SettingRepository.Delete(settingvalue);
    //      }
    //    }
    //    return false;
    //  }

    //  public bool InsertOrUpdateSettingValue(string settingName, SettingType settingType, EnvironmentType? environmentType, int? vendorOrChannelID,
    //    string value)
    //  {
    //    var result = _settingUnitOfWork.SettingRepository.GetItems(setting => setting.Name == settingName && setting.SettingType == settingType).FirstOrDefault();
    //    if (result != null)
    //    {

    //      Func<SettingValue, bool> query = null;
    //      if (settingType == SettingType.Vendor)
    //      {
    //        query =
    //          settingValue =>
    //            settingValue.EnviromentType == environmentType && settingValue.VendorID == vendorOrChannelID;
    //      }
    //      else if (settingType == SettingType.Channel)
    //      {
    //        query =
    //          settingValue =>
    //            settingValue.EnviromentType == environmentType && settingValue.VendorID == vendorOrChannelID;
    //      }
    //      else
    //      {
    //        query =
    //          settingValue =>
    //            settingValue.EnviromentType == environmentType;
    //      }

    //      var settingvalue = result.SettingValues.FirstOrDefault(query);
    //      if (settingvalue != null)
    //      {
    //        settingvalue.Value = value;
    //      }
    //      else
    //      {
    //        result.SettingValues.Add(new SettingValue()
    //        {
    //          EnviromentType = environmentType,
    //          Value = value,
    //          VendorID = settingType == SettingType.Vendor ? vendorOrChannelID : null,
    //          ChannelID = settingType == SettingType.Channel ? vendorOrChannelID : null,
    //        });
    //      }
    //      return _settingUnitOfWork.SettingRepository.Update(result).Success;
    //    }
    //    return false;
    //  }
  }
}
