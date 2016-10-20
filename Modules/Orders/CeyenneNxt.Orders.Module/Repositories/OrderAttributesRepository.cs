using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderAttributesRepository : BaseRepository, IOrderAttributesRepository
  {
    public OrderAttributesRepository() : base(SchemaConstants.Orders)
    {
    }

    public int GetIDByCode(string code, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@Code", code, dbType: DbType.String);

      return connection.Query<int>(
        GetStoredProcedureName(Constants.StoredProcedures.OrderAttributes.GetIDByCode), p, transaction,
        commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    public int Create(OrderAttribute attribute, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@Code", attribute.Code, dbType: DbType.String);
      p.Add("@Name", attribute.Name, dbType: DbType.String);
      p.Add("@ID", DbType.Int32, direction: ParameterDirection.Output);

      connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderAttributes.Create), p, transaction,
        commandType: CommandType.StoredProcedure);

      return p.Get<int>("ID");
    }

    public void CreateValue(int orderID, int attributeID, string value, SqlConnection connection,
      SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@OrderID", orderID, dbType: DbType.Int32);
      p.Add("@OrderAttributeID", attributeID, dbType: DbType.Int32);
      p.Add("@Value", value, dbType: DbType.String);

      connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderAttributeValue.Create), p,
        transaction, commandType: CommandType.StoredProcedure);
    }
  }
}