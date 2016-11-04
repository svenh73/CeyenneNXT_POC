using System.Data;
using System.Linq;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Constants;
using CeyenneNxt.Orders.Shared.Entities;
using CeyenneNxt.Orders.Shared.Interfaces;
using CeyenneNxt.Orders.Shared.Objects;
using CeyenneNXT.Orders.DataAccess.Repositories;
using Dapper;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class CustomersRepository : BaseRepository<Customer>, ICustomersRepository
  {
    public CustomersRepository() : base(CeyenneNxt.Core.Constants.SchemaConstants.Orders)
    {
    }

    public Customer GetByID(IOrderModuleSession session,int id)
    {
      var param = new DynamicParameters();
      param.Add("@ID", dbType: DbType.Int32, value: id);

      return GetItem<Customer>(session, param, Constants.StoredProcedures.Customers.GetByID);
    }

    public SearchResult<CustomerSearchResult> Search(IOrderModuleSession session,CustomerPagingFilter filter)
    {
      if (!filter.PageSize.HasValue)
      {
        filter.PageSize = Constants.DefaultPageSize;
      }
      if (!filter.PageNumber.HasValue)
      {
        filter.PageNumber = 1;
      }
      var parameters = new DynamicParameters();
      parameters.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);
      parameters.Add("@PageNumber", filter.PageNumber.Value);
      parameters.Add("@PageSize", filter.PageSize.Value);
      
      if (!string.IsNullOrWhiteSpace(filter.Name))
      {
        parameters.Add("@Name", filter.Name);
      }
      if (!string.IsNullOrWhiteSpace(filter.Phone))
      {
        parameters.Add("@PhoneNumber", filter.Phone);
      }
      if (!string.IsNullOrEmpty(filter.Company))
      {
        parameters.Add("@Company", filter.Company);
      }
      if (!string.IsNullOrEmpty(filter.Email))
      {
        parameters.Add("@Email", filter.Email);
      }

      var customers =
        session.Connection.Query<CustomerSearchResult>(
          GetStoredProcedureName(Constants.StoredProcedures.Customers.CustomerSearch),
          parameters, commandType: CommandType.StoredProcedure).ToList();

      var totalRows = parameters.Get<int?>("@TotalRows");
      var result = new SearchResult<CustomerSearchResult>
      {
        PageNumber = filter.PageNumber.Value,
        PageSize = filter.PageSize.Value,
        TotalRows = totalRows ?? 0,
        Rows = customers
      };

      return result;
    }

    public CustomerSearchResult GetCustomerWithAddressesAndOrders(IOrderModuleSession session,int customerID)
    {
      CustomerSearchResult customer;

      using (
        var customerMultiple =
          session.Connection.QueryMultiple(
            GetStoredProcedureName(Constants.StoredProcedures.Customers.SelectWithAddressesAndOrders),
            new {CustomerId = customerID}, commandType: CommandType.StoredProcedure))
      {
        customer = customerMultiple.Read<CustomerSearchResult>().FirstOrDefault();
        if (customer == null)
        {
          return null;
        }

        customer.Addresses = customerMultiple.Read<CustomerAddressSelect>().ToList();
        customer.Orders = customerMultiple.Read<OrderSearchResult>().ToList();
      }

      return customer;
    }

    public Customer GetByBackendID(IOrderModuleSession session,string backendID)
    {
      var param = new DynamicParameters();
      param.Add("@BackendID", backendID);

      return GetItem<Customer>(session,param, Constants.StoredProcedures.Customers.GetByBackendID);
    }


    public Customer Create(IOrderModuleSession session,Customer customer)
    {
      var param = new DynamicParameters();
      param.Add("@FullName", dbType: DbType.String, value: customer.FullName);
      param.Add("@FirstName", dbType: DbType.String, value: customer.FirstName);
      param.Add("@LastName", dbType: DbType.String, value: customer.LastName);
      param.Add("@BackendID", dbType: DbType.String, value: customer.BackendID);
      param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

      var id = Execute(session,param, Constants.StoredProcedures.Customers.Create).Get<int>("ID");

      return GetByID(session,id);
    }
  }
}