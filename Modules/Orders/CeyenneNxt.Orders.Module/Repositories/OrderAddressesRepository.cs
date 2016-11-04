using System.Data;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class OrderAddressesRepository : BaseRepository<OrderAddress>, IOrderAddressesRepository
  {
    public OrderAddressesRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }

    public void Create(IOrderModuleSession session,int orderID, int customerAddressID, int addressTypeID)
    {
      var p = new DynamicParameters();
      p.Add("@OrderID", orderID, dbType: DbType.Int32);
      p.Add("@CustomerAddressID", customerAddressID, dbType: DbType.Int32);
      p.Add("@CustomerAddressTypeID", addressTypeID, dbType: DbType.Int32);

      session.Connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.OrderAddress.Create), p, session.Transaction,
        commandType: CommandType.StoredProcedure);
    }


  }
}