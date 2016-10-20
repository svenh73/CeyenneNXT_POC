using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Ioc;
using CeyenneNxt.Core.Mvc;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;

namespace CeyenneNxt.Web.WebUI
{
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      ViewEngines.Engines.Clear();
      ViewEngines.Engines.Add(new CeyenneNxtViewEngine());

      var container = IocBootstrapper.Start(ApplicationType.WebUI);

      ControllerBuilder.Current.SetControllerFactory(new IocControllerFactory(container));
    }
  }
}
