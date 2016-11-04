using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.ServiceBus;
using CeyenneNxt.Orders.Module.Repositories;
using CeyenneNxt.Settings.CoreModule;
using SimpleInjector;

namespace CeyenneNXT.ServiceBus
{
  public class Bindings : BindingContainer
  {
    public override void AddBindings(Container container, ApplicationType applicationType)
    {
      var lifeStyle = ApplicationTypeToLifeStyle(applicationType);

      switch (applicationType)
      {
        case ApplicationType.Process:
          {
            break;
          }
        case ApplicationType.WebApi:
          {
            break;
          }
      }

      container.Register<ISettingModule, SettingModule>(Lifestyle.Singleton);

      container.Register<ISettingRepository, SettingRepository>(Lifestyle.Singleton);
      container.Register<ISettingValueRepository, SettingValueRepository>(Lifestyle.Singleton);
    }
  }
}
