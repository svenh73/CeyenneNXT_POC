using System.Collections.Generic;
using CeyenneNxt.Core.Entities;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Core.Interfaces.CoreModules
{
  public interface ISettingRepository : IBaseRepository<Setting>
  {
    Setting LoadSetting(ISettingsModuleSession session, string name, SettingType settingType, EnvironmentType environment);

    List<Setting> LoadDomainSettings(ISettingsModuleSession session, string domain, EnvironmentType environment);
  }
}
