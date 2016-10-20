using System.Collections.Generic;
using System.Data.SqlClient;
using CeyenneNxt.Orders.Shared.Entities;

namespace CeyenneNxt.Orders.Shared.Interfaces
{
  public interface IOrderLineStatusesRepository
  {
    IEnumerable<OrderLineStatus> GetAllStatuses(SqlConnection connection);
  }
}