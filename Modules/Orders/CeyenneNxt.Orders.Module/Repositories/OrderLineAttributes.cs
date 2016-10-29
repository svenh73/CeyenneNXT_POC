using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Interfaces;
using Dapper;

namespace CeyenneNXT.Orders.DataAccess.Repositories
{
  public class OrderLineAttributesRepository : BaseRepository, IOrderLineAttributesRepository
  {
    public OrderLineAttributesRepository() : base(SchemaConstants.Orders)
    {
    }

    public int GetIDByCode(string code, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@Code", code, dbType: DbType.String);

      return connection.Query<int>(
        GetStoredProcedureName(Constants.StoredProcedures.OrderLineAttributes.GetIDByCode), p, transaction,
        commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    public int Create(string code, string name, SqlConnection connection, SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@Code", code, dbType: DbType.String);
      p.Add("@Name", name, dbType: DbType.String);
      p.Add("@ID", DbType.Int32, direction: ParameterDirection.Output);

      connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderLineAttributes.Create), p, transaction,
        commandType: CommandType.StoredProcedure);

      return p.Get<int>("ID");
    }

    public void CreateValue(int orderID, int attributeID, string value, SqlConnection connection,
      SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@OrderLineID", orderID, dbType: DbType.Int32);
      p.Add("@OrderLineAttributeID", attributeID, dbType: DbType.Int32);
      p.Add("@Value", value, dbType: DbType.String);

      connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderLineAttributeValue.Create), p,
        transaction, commandType: CommandType.StoredProcedure);
    }


  }
}