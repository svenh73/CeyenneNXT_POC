using System.Web.Mvc;

namespace CeyenneNxt.Web.WebUI.Routing
{
  public class CeyenneNxtViewEngine : RazorViewEngine
  {
    public CeyenneNxtViewEngine()
    {
      // Define the location of the View file
      this.ViewLocationFormats = new string[] 
      {
          "~/Views/{1}/{0}.cshtml",
          "~/Views/Shared/{0}.cshtml",
          "~/Views/Base/{1}/{0}.cshtml",
          "~/Views/Base/Shared/{0}.cshtml"
      };

      this.PartialViewLocationFormats = new string[] 
      {
          "~/Views/{1}/{0}.cshtml",
          "~/Views/Shared/{0}.cshtml",
          "~/Views/Base/{1}/{0}.cshtml",
          "~/Views/Base/Shared/{0}.cshtml"
      };
    }
  }
}