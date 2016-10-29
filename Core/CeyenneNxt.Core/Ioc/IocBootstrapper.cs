using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Web;
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
      ScopedLifestyle kernelLifestyle = applicationType == ApplicationType.WebApi ? new WebApiRequestLifestyle() : new ExecutionContextScopeLifestyle();

      var container = SimpleInjectorHelper.CreateKernel(kernelLifestyle);

      string path = null;

      if (applicationType == ApplicationType.Process)
      {
        path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      }
      else
      {
        path = HttpContext.Current.Server.MapPath("bin");
      }

      SimpleInjectorHelper.LoadFromDirectory(container, applicationType, path, "CeyenneNxt.*dll");
      SimpleInjectorHelper.LoadFromDirectory(container, applicationType, path, CNXTEnvironments.Current.CustomerName + ".*dll");

      var adapter = new SimpleInjectorServiceLocatorAdapter(container);

      ServiceLocator.SetLocatorProvider(() => adapter);

      //container.Verify();

      return container;
    }
  }
}
