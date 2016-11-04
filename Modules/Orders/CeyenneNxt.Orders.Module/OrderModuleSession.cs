using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Orders.Shared.Interfaces;

namespace CeyenneNxt.Orders.Module
{
  public class OrderModuleSession : ModuleSession, IOrderModuleSession
  {
    public OrderModuleSession() : base (new SqlConnection(CNXTEnvironments.Current.Connection))
    {
    }
  }
}
