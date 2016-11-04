using System.Collections.Generic;

namespace CeyenneNxt.Core.Interfaces
{
  public interface IBaseRepository<T>
  {
    string Schema { get; }

    int Delete(IModuleSession session, int id);
    int Delete(IModuleSession session, T obj);
    IEnumerable<T> Select(IModuleSession session, string where, object parameters);
    bool Any(IModuleSession session, string where, object parameters);
    T SelectByID(IModuleSession session, int id);
    int Update(IModuleSession session, T obj);
    int? Insert(IModuleSession session, T obj);
  }
}