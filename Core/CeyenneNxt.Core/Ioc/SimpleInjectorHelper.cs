using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Enums;
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

    public static void LoadFromDirectory(Container kernel, DependencyTypes dependencyType, string directory, string searchPattern)
    {
      string[] dependencies = Directory.GetFiles(directory, searchPattern);

      //Load Dependency Assemblies
      foreach (string fileName in dependencies)
      {
        string assemblyName = Path.GetFileNameWithoutExtension(fileName);
        if (assemblyName != null)
        {
          Assembly pluginAssembly = Assembly.Load(assemblyName);

          try
          {
            var types = pluginAssembly.GetTypes();

            if ((dependencyType & DependencyTypes.Modules) == DependencyTypes.Modules)
            {
              var registrations =
                from type in types
                from interfaces in type.GetInterfaces()
                where type.IsPublic
                && !type.IsAbstract
                &&  interfaces.Name.EndsWith("Repository")
                select new { Service = interfaces, Implementation = type };

              foreach (var registration in registrations)
              {
                kernel.Register(registration.Service, registration.Implementation, Lifestyle.Scoped);
              }

              registrations =
                from type in types
                from interfaces in type.GetInterfaces()
                where type.IsPublic
                && !type.IsAbstract
                && interfaces.Name.EndsWith("Module")
                orderby interfaces.Name descending
                select new { Service = interfaces, Implementation = type };

              foreach (var registration in registrations)
              {
                kernel.Register(registration.Service, registration.Implementation, Lifestyle.Scoped);
              }
            }

            if ((dependencyType & DependencyTypes.ApiControllers) == DependencyTypes.ApiControllers)
            {
              var registrations =
                from type in types
                from interfaces in type.GetInterfaces()
                where type.IsPublic
                && !type.IsAbstract
                && (interfaces.Name.EndsWith("ApiController"))
                select new { Service = interfaces, Implementation = type };

              foreach (var registration in registrations)
              {
                kernel.Register(registration.Service, registration.Implementation, Lifestyle.Scoped);
              }
            }

            if ((dependencyType & DependencyTypes.Controllers) == DependencyTypes.Controllers)
            {
              var registrations =
                from type in types
                from interfaces in type.GetInterfaces()
                where type.IsPublic
                && !type.IsAbstract
                && (interfaces.Name.EndsWith("UIController"))
                select new { Service = interfaces, Implementation = type };

              foreach (var registration in registrations)
              {
                kernel.Register(registration.Service, registration.Implementation, Lifestyle.Scoped);
              }
            }

          }
          catch (Exception ex)
          {
            throw new Exception(ex.Message);
          }

        }
      }
    }
  }
}
