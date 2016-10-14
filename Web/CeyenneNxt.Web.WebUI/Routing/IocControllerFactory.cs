using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WimBosman.Web.WebUI
{
  public class IocControllerFactory : DefaultControllerFactory
  {
    public SimpleInjector.Container Kernel { get; private set; }

    public IocControllerFactory(SimpleInjector.Container container)
    {

      this.Kernel = container;
    }

    protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
    {
      var controller = (IController) Kernel.GetInstance(controllerType);
      if (controller == null)
      {
        return base.GetControllerInstance(requestContext, controllerType);
      }
      else
      {
        return controller;
      }
    }

    protected override Type GetControllerType(RequestContext requestContext, string controllerName)
    {
      var registrations = Kernel.GetCurrentRegistrations();
      var registration =
        registrations.FirstOrDefault(
          p =>
            p.Registration.ImplementationType.Name.StartsWith(controllerName + "controller",
              StringComparison.InvariantCultureIgnoreCase));
      if (registration == null)
      {
        return base.GetControllerType(requestContext, controllerName);
      }
      else
      {
        return registration.Registration.ImplementationType;
      }
    }

  }
}