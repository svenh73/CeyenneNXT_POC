using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class User: AuditEntity
  {
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string OrganizationCode { get; set; }
  }
}
