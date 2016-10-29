﻿using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Products.Module.Controllers;
using CeyenneNxt.Products.Module.Modules;
using CeyenneNxt.Products.Module.Repositories;
using CeyenneNxt.Products.Shared.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;

namespace CeyenneNxt.Products.Module
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
            container.Register<IProductApiController, ProductController>(lifeStyle);
            break;
          }
      }

      container.Register<IProductRepository, ProductRepository>(lifeStyle);

      container.Register<IProductModule, ProductModule>(lifeStyle);
    }
  }
}
