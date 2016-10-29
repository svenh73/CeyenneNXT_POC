using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.ServiceBus;
using SimpleInjector;

namespace CeyenneNxt.ServiceBus.CoreModule
{
  public class Bindings : BindingContainer
  {
    public override void AddBindings(Container container, ApplicationType applicationType)
    {
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

      container.Register<IServiceBusModule, ServiceBusModule>(ApplicationTypeToLifeStyle(applicationType));
    }
  }
}
