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
  public class OrderTypesRepository : BaseRepository<OrderType>, IOrderTypesRepository
  {
    public OrderTypesRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }

    public int GetByName(IOrderModuleSession session,string name)
    {
      var p = new DynamicParameters();
      p.Add("@Name", name, DbType.String);

      return GetItem<int>(session, p, Constants.StoredProcedures.OrderType.GetByName);
    }

    public int Create(IOrderModuleSession session,string orderTypeName)
    {
      var p = new DynamicParameters();
      p.Add("@Name", orderTypeName, DbType.String);
      p.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

      return Execute(session, p, Constants.StoredProcedures.OrderType.Create).Get<int>("ID");
    }

    public IEnumerable<OrderType> GetAll(IOrderModuleSession session)
    {
      return session.Connection.Query<OrderType>(GetStoredProcedureName(Constants.StoredProcedures.Orders.GetAllOrderTypes),
        commandType: CommandType.StoredProcedure);
    }


  }
}