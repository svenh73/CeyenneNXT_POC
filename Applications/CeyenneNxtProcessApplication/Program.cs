using System;
using System.Reflection;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Ioc;
using CommonServiceLocator.SimpleInjectorAdapter;
using Microsoft.Practices.ServiceLocation;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;

namespace CeyenneNxt.Process.Application
{
  class Program
  {
    static void Main(string[] args)
    {
      var container = SimpleInjectorHelper.CreateKernel(new ExecutionContextScopeLifestyle());

      var path = AppDomain.CurrentDomain.BaseDirectory;

      SimpleInjectorHelper.LoadFromDirectory(container, DependencyTypes.Modules, path, "CeyenneNxt.*dll");
      SimpleInjectorHelper.LoadFromDirectory(container, DependencyTypes.Modules, path, CNXTEnvironments.Current.CustomerName + ".*dll");

      var adapter = new SimpleInjectorServiceLocatorAdapter(container);

      ServiceLocator.SetLocatorProvider(() => adapter);

      //var processes = ServiceLocator.Current.GetAllInstances(typeof(IProcessor));
      //foreach (IProcessor process in processes)
      //{
      //  process.Execute();
      //}

    }
  }
}
