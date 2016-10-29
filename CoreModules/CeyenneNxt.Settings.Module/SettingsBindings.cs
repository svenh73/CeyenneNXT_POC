using Concentrator.Core.Types.Interfaces.BusinessServices;
using Concentrator.Modules.Settings.Repository;
using Concentrator.Modules.Settings.Shared.Interfaces;
using Ninject.Modules;

namespace Concentrator.Modules.Settings
{
  public class SettingsBindings : NinjectModule
  {
    public override void Load()
    {
      Bind<ISettingModule>().To<SettingModule>();
      Bind<ISettingRepository>().To<SettingRepository>();
    }
  }
}
