using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;

namespace WebApplication1
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      GlobalConfiguration.Configure(WebApiConfig.Register);

      

      //var container = IocBootstrapper.Start(ApplicationType.WebApi);

      //container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
      //GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

      //var assemblyResolver = new CustomAssembliesResolver();
      //assemblyResolver.CustomerName = CNXTEnvironments.Current.CustomerName;
      //GlobalConfiguration.Configuration.Services.Replace(typeof(IAssembliesResolver), assemblyResolver);

      //GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(GlobalConfiguration.Configuration));
    }
  }
}
