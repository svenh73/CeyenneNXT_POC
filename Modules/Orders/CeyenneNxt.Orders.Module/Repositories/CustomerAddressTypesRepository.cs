
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class CustomerAddressTypesRepository : BaseRepository<CustomerAddressType>, ICustomerAddressTypesRepository
  {
    public CustomerAddressTypesRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }

    public int GetIDByCode(IOrderModuleSession session,string code)
    {
      var param = new DynamicParameters();
      param.Add("@Code", code);

      return GetItem<int>(session,param, Constants.StoredProcedures.AddressType.GetByCode);
    }

    public CustomerAddressType GetByID(IOrderModuleSession session,int id)
    {
      var param = new DynamicParameters();
      param.Add("@ID", id);

      return GetItem<CustomerAddressType>(session,param, Constants.StoredProcedures.AddressType.GetByID);
    }

    public int Create(IOrderModuleSession session,CustomerAddressType customerAddressType)
    {
      var param = new DynamicParameters();
      param.Add("@Code", customerAddressType.Code, dbType: System.Data.DbType.String);
      param.Add("@Name", customerAddressType.Name, dbType: System.Data.DbType.String);
      param.Add("@ID", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

      return Execute(session,param, Constants.StoredProcedures.AddressType.Insert).Get<int>("ID");
    }


  }
}