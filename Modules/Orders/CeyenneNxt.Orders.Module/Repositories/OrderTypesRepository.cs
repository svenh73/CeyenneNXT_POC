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
  public class OrderTypesRepository : BaseRepository, IOrderTypesRepository
  {
    public OrderTypesRepository() : base(SchemaConstants.Orders)
    {
    }

    public int GetByName(string name, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@Name", name, DbType.String);

      return GetItem<int>(p, Constants.StoredProcedures.OrderType.GetByName, connection, transaction);
    }

    public int Create(string orderTypeName, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@Name", orderTypeName, DbType.String);
      p.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

      return Execute(p, Constants.StoredProcedures.OrderType.Create, connection, transaction).Get<int>("ID");
    }

    public IEnumerable<OrderType> GetAll(SqlConnection connection)
    {
      return connection.Query<OrderType>(GetStoredProcedureName(Constants.StoredProcedures.Orders.GetAllOrderTypes),
        commandType: CommandType.StoredProcedure);
    }


  }
}