using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class CustomerAddressesRepository : BaseRepository, ICustomerAddressesRepository
  {
    public CustomerAddressesRepository() : base(SchemaConstants.Orders)
    {
    }

    public int Create(CustomerAddress model, int customerID, SqlConnection connection,
      SqlTransaction transaction)
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

      connection.Execute(GetStoredProcedureName(Constants.StoredProcedures.Address.Create), p,
        commandType: CommandType.StoredProcedure, transaction: transaction);
      var id = p.Get<int>("ID");

      return id;
    }

    public CustomerAddress Get(int id, SqlConnection connection, SqlTransaction transaction)
    {
      return
        connection.Query<CustomerAddress>(
          GetStoredProcedureName(Constants.StoredProcedures.Address.GetByID), new {ID = id}, transaction,
          commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    public int GetByCustomerAndBackendID(int customerID, string backendID, SqlConnection connection,
      SqlTransaction transaction)
    {
      var p = new DynamicParameters();
      p.Add("@CustomerID", customerID, DbType.Int32);
      p.Add("@BackendID", backendID, DbType.String);

      return
        connection.Query<int>(GetStoredProcedureName(Constants.StoredProcedures.Address.GetByBackendID), p,
          transaction: transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
    }
  }
}