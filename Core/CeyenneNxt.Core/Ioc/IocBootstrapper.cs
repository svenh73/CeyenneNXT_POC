using System;
using System.IO;
using System.Reflection;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Enums;
using CommonServiceLocator.SimpleInjectorAdapter;
using Microsoft.Practices.ServiceLocation;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;

namespace CeyenneNxt.Core.Ioc
{
  public class IocBootstrapper
  {
    public static Container Start(ApplicationType applicationType)
    {
      // Default for processingt application
      ScopedLifestyle kernelLifestyle = new ExecutionContextScopeLifestyle(); ;
      Lifestyle lifestyle = Lifestyle.Transient;

      if (applicationType == ApplicationType.WebApi)
        {
          kernelLifestyle = new WebApiRequestLifestyle();
          lifestyle = new WebApiRequestLifestyle();
      }
      else if (applicationType == ApplicationType.WebUI)
      {
        kernelLifestyle = new WebRequestLifestyle();
        lifestyle = new WebRequestLifestyle();
      }

      var container = SimpleInjectorHelper.CreateKernel(kernelLifestyle);

      var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

      SimpleInjectorHelper.LoadFromDirectory(container, applicationType, path, "CeyenneNxt.*dll");
      SimpleInjectorHelper.LoadFromDirectory(container, applicationType, path, CNXTEnvironments.Current.CustomerName + ".*dll");

      var adapter = new SimpleInjectorServiceLocatorAdapter(container);

      ServiceLocator.SetLocatorProvider(() => adapter);

      //container.Verify();

      return container;
    }
  }
}
