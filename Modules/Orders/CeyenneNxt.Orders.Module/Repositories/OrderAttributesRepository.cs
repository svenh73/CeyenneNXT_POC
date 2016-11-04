using System.Data;
using System.Linq;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderAttributesRepository : BaseRepository<OrderAttribute>, IOrderAttributesRepository
  {
    public OrderAttributesRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }

    public int GetIDByCode(IOrderModuleSession session,string code)
    {
      var p = new DynamicParameters();
      p.Add("@Code", code, dbType: DbType.String);

      return session.Connection.Query<int>(
        GetStoredProcedureName(Constants.StoredProcedures.OrderAttributes.GetIDByCode), p, session.Transaction,
        commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    public int Create(IOrderModuleSession session,OrderAttribute attribute)
    {
      var p = new DynamicParameters();
      p.Add("@Code", attribute.Code, dbType: DbType.String);
      p.Add("@Name", attribute.Name, dbType: DbType.String);
      p.Add("@ID", DbType.Int32, direction: ParameterDirection.Output);

      session.Connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderAttributes.Create), p, session.Transaction,
        commandType: CommandType.StoredProcedure);

      return p.Get<int>("ID");
    }

    public void CreateValue(IOrderModuleSession session,int orderID, int attributeID, string value)
    {
      var p = new DynamicParameters();
      p.Add("@OrderID", orderID, dbType: DbType.Int32);
      p.Add("@OrderAttributeID", attributeID, dbType: DbType.Int32);
      p.Add("@Value", value, dbType: DbType.String);

      session.Connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderAttributeValue.Create), p,
        session.Transaction, commandType: CommandType.StoredProcedure);
    }
  }
}