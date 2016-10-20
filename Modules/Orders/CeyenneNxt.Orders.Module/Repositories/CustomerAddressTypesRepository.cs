using System.Data.SqlClient;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class CustomerAddressTypesRepository : BaseRepository, ICustomerAddressTypesRepository
  {
    public CustomerAddressTypesRepository() : base(SchemaConstants.Orders)
    {
    }

    public int GetIDByCode(string code, SqlConnection connection, SqlTransaction transaction)
    {
      var param = new DynamicParameters();
      param.Add("@Code", code);

      return GetItem<int>(param, Constants.StoredProcedures.AddressType.GetByCode, connection, transaction);
    }

    public CustomerAddressType GetByID(int id, SqlConnection connection, SqlTransaction transaction)
    {
      var param = new DynamicParameters();
      param.Add("@ID", id);

      return GetItem<CustomerAddressType>(param, Constants.StoredProcedures.AddressType.GetByID, connection, transaction);
    }

    public int Create(CustomerAddressType customerAddressType, SqlConnection connection, SqlTransaction transaction)
    {
      var param = new DynamicParameters();
      param.Add("@Code", customerAddressType.Code, dbType: System.Data.DbType.String);
      param.Add("@Name", customerAddressType.Name, dbType: System.Data.DbType.String);
      param.Add("@ID", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

      return Execute(param, Constants.StoredProcedures.AddressType.Insert, connection, transaction).Get<int>("ID");
    }


  }
}