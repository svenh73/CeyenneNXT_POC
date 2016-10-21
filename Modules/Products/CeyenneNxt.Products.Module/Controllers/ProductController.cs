using System.Web.Mvc;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Products.Shared.Interfaces;

namespace CeyenneNxt.Products.Module.Controllers
{
  public class ProductController : Controller, IProductController
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "About CeyenneNxt";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}