using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Interfaces.Controllers;
using CeyenneNxt.Web.WebUI.Controllers;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace CeyenneNxt.Web.WebUI
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
            container.Register<IHomeController, HomeController>(lifeStyle);
            //container.Register<IAccountController, AccountController>(lifeStyle);
            //container.Register<IManageController, ManageController>(lifeStyle);
            break;
          }
      }
    }
  }
}
