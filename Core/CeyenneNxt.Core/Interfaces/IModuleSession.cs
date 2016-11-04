using System.Data.Common;
using System.Data.SqlClient;

namespace CeyenneNxt.Core.Interfaces
{
  public interface IModuleSession
  {
    DbConnection Connection { get; }

    DbTransaction Transaction { get; }

    bool IsInTransaction { get; }
    void BeginTransaction();
    void Commit();
    void Dispose();
    void Rollback();
  }
}