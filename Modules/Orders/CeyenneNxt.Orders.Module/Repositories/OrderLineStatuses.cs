using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderLineStatusesRepository : BaseRepository, IOrderLineStatusesRepository
  {
    public OrderLineStatusesRepository() : base(SchemaConstants.Orders)
    {
    }

    public IEnumerable<OrderLineStatus> GetAllStatuses(SqlConnection connection)
    {
      return connection.Query<OrderLineStatus>(GetStoredProcedureName(Constants.StoredProcedures.OrderLineStatus.GetAllStatuses),
        commandType: CommandType.StoredProcedure);
    }
  }
}
