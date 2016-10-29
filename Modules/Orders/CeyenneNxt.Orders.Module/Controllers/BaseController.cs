using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace CeyenneNxt.Orders.Module.Controllers
{
  public class BaseController : ApiController
  {
    private const string Username = "preferred_username";

    protected string GetUserName()
    {
      var identity = (ClaimsIdentity)User.Identity;
      var userNameClaim = identity.Claims.FirstOrDefault(cl => cl.Type == Username);
      var userName = userNameClaim?.Value;

      return userName;
    }
  }
}