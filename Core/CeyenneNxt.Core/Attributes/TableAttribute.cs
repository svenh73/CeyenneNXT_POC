using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeyenneNxt.Core.Attributes
{
  public class TableAttribute : Dapper.TableAttribute
  {
    public TableAttribute(string tableName, string schemaName) : base(tableName)
    {
      this.Schema = schemaName;
    }
  }
}
