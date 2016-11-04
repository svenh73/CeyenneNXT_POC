using System.Collections.Generic;
using System.Data;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderLineStatusesRepository : BaseRepository<OrderLineStatus>, IOrderLineStatusesRepository
  {
    public OrderLineStatusesRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }

    public IEnumerable<OrderLineStatus> GetAllStatuses(IOrderModuleSession session)
    {
      return session.Connection.Query<OrderLineStatus>(GetStoredProcedureName(Constants.StoredProcedures.OrderLineStatus.GetAllStatuses),
        commandType: CommandType.StoredProcedure);
    }
  }
}
