using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace CeyenneNxt.Core.Types
{
  public abstract class BaseRepository
  {
    public string Schema { get; private set; }

    public BaseRepository(string schema)
    {
      Schema = schema;
    }

    protected string GetStoredProcedureName(string storedProcedureName)
    {
      if (string.IsNullOrEmpty(storedProcedureName))
        throw new NotSupportedException("Must specify a Stored Procedure name");

      return string.Format("{0}.{1}", Schema, storedProcedureName);
    }


    protected T GetItem<T>(DynamicParameters parameters, string spName, SqlConnection connection,
      SqlTransaction transaction)
    {
      return
        connection.Query<T>(GetStoredProcedureName(spName), parameters, transaction,
          commandType: CommandType.StoredProcedure).FirstOrDefault();
    }


    protected DynamicParameters Execute(DynamicParameters parameters, string spName, SqlConnection connection,
      SqlTransaction transaction)
    {
      connection.Execute(GetStoredProcedureName(spName), parameters, transaction,
        commandType: CommandType.StoredProcedure);

      return parameters;
    }
  }
}