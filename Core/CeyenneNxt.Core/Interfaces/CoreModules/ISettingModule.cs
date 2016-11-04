using System.Collections.Generic;
using CeyenneNxt.Core.Dtos.Settings;
using CeyenneNxt.Core.Entities;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Core.Interfaces.CoreModules
{
  public interface ISettingModule
  {
    ISettingRepository SettingRepository { get; }
    ISettingValueRepository SettingValueRepository { get; }

    bool DeleteChannelSettingValue(ISettingsModuleSession session, string domain, string settingName, EnvironmentType? environmentType, int? vendorOrChannelID);
    bool DeleteGeneralSettingValue(ISettingsModuleSession session, string domain, string settingName, EnvironmentType? environmentType, int? vendorOrChannelID);
    bool DeleteGlobalSettingValue(ISettingsModuleSession session, string settingName, EnvironmentType? environmentType, int? vendorOrChannelID);
    bool DeleteSetting(ISettingsModuleSession session, BaseSettingDto settingDto);
    bool DeleteVendorSettingValue(ISettingsModuleSession session, string domain, string settingName, EnvironmentType? environmentType, int? vendorOrChannelID);
    int? InsertOrUpdateSetting(ISettingsModuleSession session, BaseSettingDto settingDto);
    bool InsertOrUpdateChannelSettingValue(ISettingsModuleSession session, string domain, string settingName, EnvironmentType? environmentType, int channelID, string value);
    bool InsertOrUpdateGeneralSettingValue(ISettingsModuleSession session, string domain, string settingName, EnvironmentType? environmentType, string value);
    bool InsertOrUpdateGlobalSettingValue(ISettingsModuleSession session, string settingName, EnvironmentType? environmentType, string value);
    bool InsertOrUpdateSettingValue(ISettingsModuleSession session, string settingName, SettingType settingType, EnvironmentType? environmentType, int? vendorOrChannelID, string value);
    bool InsertOrUpdateVendorSettingValue(ISettingsModuleSession session, string domain, string settingName, EnvironmentType? environmentType, int vendorID, string value);
    SettingCollection LoadSettings(ISettingsModuleSession session, SettingCollection settingsdefinition);
  }
}
