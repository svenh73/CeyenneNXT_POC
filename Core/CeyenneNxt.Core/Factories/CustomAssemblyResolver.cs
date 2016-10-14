using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Dispatcher;

namespace CeyenneNxt.Core.Factories
{
  public class CustomAssembliesResolver : DefaultAssembliesResolver
  {
    public string CustomerName { get; set; }

    public override ICollection<Assembly> GetAssemblies()
    {
      List<Assembly> assemblies = new List<Assembly>(base.GetAssemblies());
      ArrayList al = new ArrayList();
      al.AddRange(Directory.GetFiles(HttpContext.Current.Server.MapPath("bin"), "CeyenneNxt.Modules.*.dll"));
      al.AddRange(Directory.GetFiles(HttpContext.Current.Server.MapPath("bin"), CustomerName + "*.Modules.*.dll"));

      foreach (string file in al.ToArray())
      {
        var controllersAssembly = Assembly.LoadFrom(file);
        assemblies.Add(controllersAssembly);
      }
      return assemblies;
    }
  }
}
