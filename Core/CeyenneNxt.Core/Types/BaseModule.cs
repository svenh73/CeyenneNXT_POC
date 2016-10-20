using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Configuration;

namespace CeyenneNxt.Core.Types
{
  public class BaseModule
  {
    public SqlConnection GetNewConnection()
    {
      return new SqlConnection(CNXTEnvironments.Current.Connection);
    }
  }
}
