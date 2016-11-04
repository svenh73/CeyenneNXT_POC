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
  public class OrderStatusesRepository : BaseRepository<OrderStatus>, IOrderStatusesRepository
  {
    public OrderStatusesRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }
    public IEnumerable<OrderStatus> GetAll(IOrderModuleSession session)
    {
      return session.Connection.Query<OrderStatus>(GetStoredProcedureName(Constants.StoredProcedures.OrderStatus.SelectAll),
        commandType: CommandType.StoredProcedure);
    }

    public int GetStatusIDByCode(IOrderModuleSession session,string code)
    {
      var p = new DynamicParameters();
      p.Add("@Code", code, DbType.String);

      return GetItem<int>(session,p, Constants.StoredProcedures.OrderStatus.GetIDByCode);
    }
  }
}

