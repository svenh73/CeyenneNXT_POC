using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Types;
using CeyenneNxt.Products.Shared.Interfaces;

namespace CeyenneNxt.Orders.Module
{
  public class ProductModuleSession : ModuleSession, IProductModuleSession
  {
    public ProductModuleSession() : base (new SqlConnection(CNXTEnvironments.Current.Connection))
    {
    }
  }
}
