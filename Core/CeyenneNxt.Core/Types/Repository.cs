using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Interfaces;

namespace CeyenneNxt.Core.Types
{
  public abstract class Repository<T>: IRepository<T>
  {
    public int Insert(T item)
    {
      return 0;
    }

    public List<T> Select(string sql)
    {
      return new List<T>();
    }
  }
}
