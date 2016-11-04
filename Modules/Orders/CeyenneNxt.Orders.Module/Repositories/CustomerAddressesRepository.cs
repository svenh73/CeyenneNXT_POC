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
  public class CustomerAddressesRepository : BaseRepository<CustomerAddress>, ICustomerAddressesRepository
  {
    public CustomerAddressesRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }

    public int Create(IOrderModuleSession session,CustomerAddress model, int customerID)
    {
      var p = new DynamicParameters();
      p.Add("@BackendID", dbType: DbType.String, value: model.BackendID);
      p.Add("@CustomerID", dbType: DbType.Int32, value: customerID);
      p.Add("@Company", dbType: DbType.String, value: model.Company);
      p.Add("@Att", dbType: DbType.String, value: model.Att);
      p.Add("@Street", dbType: DbType.String, value: model.Street);
      p.Add("@HouseNumber", dbType: DbType.String, value: model.HouseNumber);
      p.Add("@HouseNumberExt", dbType: DbType.String, value: model.HouseNumberExt);
      p.Add("@ZIPCode", dbType: DbType.String, value: model.ZIPCode);
      p.Add("@City", dbType: DbType.String, value: model.City);
      p.Add("@CountryID", dbType: DbType.Int32, value: model.Country.ID);
      p.Add("@Remark", dbType: DbType.String, value: model.Remark);
      p.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

      session.Connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.Address.Create), p,
        commandType: CommandType.StoredProcedure, transaction: session.Transaction);
      var id = p.Get<int>("ID");

      return id;
    }

    public CustomerAddress Get(IOrderModuleSession session,int id)
    {
      return
        session.Connection.Query<CustomerAddress>(
          GetStoredProcedureName(Constants.StoredProcedures.Address.GetByID), new {ID = id}, session.Transaction,
          commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    public int GetByCustomerAndBackendID(IOrderModuleSession session,int customerID, string backendID)
    {
      var p = new DynamicParameters();
      p.Add("@CustomerID", customerID, DbType.Int32);
      p.Add("@BackendID", backendID, DbType.String);

      return
        session.Connection.Query<int>(GetStoredProcedureName(Constants.StoredProcedures.Address.GetByBackendID), p,
          transaction: session.Transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
    }
  }
}