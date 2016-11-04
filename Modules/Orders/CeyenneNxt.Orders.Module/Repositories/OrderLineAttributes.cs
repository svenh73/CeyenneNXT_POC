using System.Data;
using System.Linq;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using Dapper;

namespace CeyenneNXT.Orders.DataAccess.Repositories
{
  public class OrderLineAttributesRepository : BaseRepository<OrderLineAttribute>, IOrderLineAttributesRepository
  {
    public OrderLineAttributesRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }

    public int GetIDByCode(IOrderModuleSession session,string code)
    {
      var p = new DynamicParameters();
      p.Add("@Code", code, dbType: DbType.String);

      return session.Connection.Query<int>(
        GetStoredProcedureName(Constants.StoredProcedures.OrderLineAttributes.GetIDByCode), p, session.Transaction,
        commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    public int Create(IOrderModuleSession session,string code, string name)
    {
      var p = new DynamicParameters();
      p.Add("@Code", code, dbType: DbType.String);
      p.Add("@Name", name, dbType: DbType.String);
      p.Add("@ID", DbType.Int32, direction: ParameterDirection.Output);

      session.Connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderLineAttributes.Create), p, session.Transaction,
        commandType: CommandType.StoredProcedure);

      return p.Get<int>("ID");
    }

    public void CreateValue(IOrderModuleSession session,int orderID, int attributeID, string value)
    {
      var p = new DynamicParameters();
      p.Add("@OrderLineID", orderID, dbType: DbType.Int32);
      p.Add("@OrderLineAttributeID", attributeID, dbType: DbType.Int32);
      p.Add("@Value", value, dbType: DbType.String);

      session.Connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderLineAttributeValue.Create), p,
        session.Transaction, commandType: CommandType.StoredProcedure);
    }


  }
}