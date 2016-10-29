using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Enums;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;

namespace CeyenneNxt.Core.Interfaces
{
  public abstract class BindingContainer
  {
    public abstract void AddBindings(Container container, ApplicationType applicationType);

    public Lifestyle ApplicationTypeToLifeStyle(ApplicationType applicationType)
    {
      return applicationType == ApplicationType.WebApi ? new WebApiRequestLifestyle() : Lifestyle.Transient;
    }
  }
}
