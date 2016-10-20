using System.Data;
using System.Data.SqlClient;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderAddressesRepository : BaseRepository, IOrderAddressesRepository
  {
    public OrderAddressesRepository() : base(SchemaConstants.Orders)
    {
    }

    public void Create(int orderID, int customerAddressID, int addressTypeID, SqlConnection connection,
      SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@OrderID", orderID, dbType: DbType.Int32);
      p.Add("@CustomerAddressID", customerAddressID, dbType: DbType.Int32);
      p.Add("@CustomerAddressTypeID", addressTypeID, dbType: DbType.Int32);

      connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderAddress.Create), p, transaction,
        commandType: CommandType.StoredProcedure);
    }


  }
}