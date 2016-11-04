using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using CeyenneNxt.Core.Entities;
using CeyenneNxt.Core.Interfaces;
using Dapper;

namespace CeyenneNxt.Core.Types
{
  public abstract class BaseRepository<T> : IBaseRepository<T>
  {
    public string Schema { get; private set; }

    public BaseRepository(string schema)
    {
      Schema = schema;
    }

    public bool Any(IModuleSession session, string @where, object parameters)
    {
      return session.Connection.RecordCount<T>(where, parameters) > 0;
    }

    public T SelectByID(IModuleSession session, int id)
    {
      return session.Connection.Get<T>(id);
    }

    public int Delete(IModuleSession session, T obj)
    {
      if (session.IsInTransaction)
      {
        return session.Connection.Delete(obj, session.Transaction);
      }
      else
      {
        return session.Connection.Delete(obj);
      }
    }

    public IEnumerable<T> Select(IModuleSession session, string where,object parameters)
    {
      return session.Connection.GetList<T>(where, parameters);
    }

    public int Update(IModuleSession session, T obj)
    {
      if (session.IsInTransaction)
      {
        return session.Connection.Update(obj,session.Transaction);
      }
      else
      {
        return session.Connection.Update(obj);
      }
    }

    public int? Insert(IModuleSession session, T obj)
    {
      if (session.IsInTransaction)
      {
        return session.Connection.Insert(obj, session.Transaction);
      }
      else
      {
        return session.Connection.Insert(obj);
      }
    }

    public int Delete(IModuleSession session, int id)
    {
      if (session.IsInTransaction)
      {
        return session.Connection.Delete(id,session.Transaction);
      }
      else
      {
        return session.Connection.Delete(id);
      }
    }

    protected string GetStoredProcedureName(string storedProcedureName)
    {
      if (string.IsNullOrEmpty(storedProcedureName))
        throw new NotSupportedException("Must specify a Stored Procedure name");

      return $"{Schema}.{storedProcedureName}";
    }



    protected T GetItem<T>(IModuleSession session,DynamicParameters parameters, string spName)
    {
      return
        session.Connection.Query<T>(GetStoredProcedureName(spName), parameters, session.Transaction,
          commandType: CommandType.StoredProcedure).FirstOrDefault();
    }


    protected DynamicParameters Execute(IModuleSession session,DynamicParameters parameters, string spName)
    {
      session.Connection.Execute(GetStoredProcedureName(spName), parameters, session.Transaction,
        commandType: CommandType.StoredProcedure);

      return parameters;
    }
  }
}