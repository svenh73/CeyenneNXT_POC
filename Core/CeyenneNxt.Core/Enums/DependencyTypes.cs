using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeyenneNxt.Core.Enums
{
  [Flags]
  public enum DependencyTypes
  {
    Modules,
    ApiControllers,
    Controllers
  }
}
