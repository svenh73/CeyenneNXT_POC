using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeyenneNxt.Core.Types
{
  public class BaseProcessor
  {
    public string Domain
    {
      get { return GetType().FullName; }
    }
  }
}
