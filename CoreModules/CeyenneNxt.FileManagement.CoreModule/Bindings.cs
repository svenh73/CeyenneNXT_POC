using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Interfaces.CoreModules;
using SimpleInjector;

namespace CeyenneNxt.FileManagement.CoreModule
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
        case ApplicationType.WebUI:
          {
            break;
          }
      }

      container.Register<IFileManagingModule, FileManagingModule>(lifeStyle);
    }
  }
}
