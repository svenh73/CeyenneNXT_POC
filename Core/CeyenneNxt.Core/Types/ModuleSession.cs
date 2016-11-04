using System;
using System.Data.Common;
using System.Data.SqlClient;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Interfaces;

namespace CeyenneNxt.Core.Types
{
  public class ModuleSession : IDisposable, IModuleSession
  {
    public ModuleSession(DbConnection connection = null)
    {
      Connection = connection;
      Connection.Open();
    }

    public DbConnection Connection { get; private set; }

    public DbTransaction Transaction { get; private set; }

    public bool IsInTransaction {
      get { return Transaction != null; }
    }

    public void BeginTransaction()
    {
      Transaction?.Rollback();
      Transaction = Connection.BeginTransaction();
    }

    public void Commit()
    {
      Transaction?.Commit();
    }

    public void Rollback()
    {
      Transaction?.Rollback();
    }

    public void Dispose()
    {
      Transaction?.Rollback();
      Connection?.Dispose();
    }
  }
}
