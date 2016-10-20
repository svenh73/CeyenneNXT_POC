using System;
using System.Linq;
using System.Reflection;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Helpers;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Ioc;
using CommonServiceLocator.SimpleInjectorAdapter;
using Microsoft.Practices.ServiceLocation;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;

namespace CeyenneNxt.Process.Application
{
  class Program
  {
    static void Main(string[] args)
    {
      var container = IocBootstrapper.Start(ApplicationType.Process);

      var processes = container.GetCurrentRegistrations()
        .Where(p => typeof(IProcessor).IsAssignableFrom(p.Registration.ImplementationType))
        .Select(p => p.Registration.ImplementationType).ToList();
      
      //processes = processes.Where(p => p.GetType().Name.StartsWith("Subscriber"));
      foreach (var process in processes)
      {
        var proc = (IProcessor)container.GetInstance(process);
        proc.Execute();
      }
    }
  }
}
