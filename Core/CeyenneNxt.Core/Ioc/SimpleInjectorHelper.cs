using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Types;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;

namespace CeyenneNxt.Core.Ioc
{
  public static class SimpleInjectorHelper
  {

    public static Container CreateKernel(ScopedLifestyle lifestyle = null)
    {
      var kernel = new Container();
      kernel.Options.AllowOverridingRegistrations = true;
      kernel.Options.DefaultScopedLifestyle = lifestyle;
      return kernel;
    }

    public static void LoadFromDirectory(Container kernel,ApplicationType applicationType, string directory, string searchPattern)
    {
      string[] dependencies = Directory.GetFiles(directory, searchPattern);

      foreach (string fileName in dependencies)
      {
        try
        {
          string assemblyName = Path.GetFileNameWithoutExtension(fileName);
          if (assemblyName != null)
          {
            Assembly pluginAssembly = Assembly.Load(assemblyName);

            var types = pluginAssembly.GetTypes();

            var bindingContainers =
              from type in types
              where type.IsSubclassOf(typeof(BindingContainer))
              select type;

            foreach (var bindingContainer in bindingContainers)
            {
              var methodInfo = bindingContainer.GetMethod("AddBindings", new Type[] { typeof(Container), typeof(ApplicationType) });
              if (methodInfo != null) // the method doesn't exist
              {
                var instance = Activator.CreateInstance(bindingContainer);
                methodInfo.Invoke(instance, new object[] { kernel, applicationType });
              }
            }
          }
        }
        catch (Exception ex)
        {
          
          throw;
        }
      }
    }
  }
}
