using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeyenneNxt.Core.Interfaces
{
  public interface IRepository<T>
  {
    int Insert(T item);
    List<T> Select(string sql);
  }
}
