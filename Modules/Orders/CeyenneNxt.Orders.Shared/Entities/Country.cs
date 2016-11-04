using CeyenneNxt.Core.Entities;

namespace CeyenneNxt.Orders.Shared.Entities
{
  public class Country : BaseEntity
  {
    public string Code { get; set; }

    public string Name { get; set; }
  }
}