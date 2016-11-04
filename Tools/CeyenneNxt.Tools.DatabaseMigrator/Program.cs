using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Configuration;

namespace CeyenneNxt.Tools.DatabaseMigrator
{
  class Program
  {
    static void Main(string[] args)
    {
      Runner.MigrateToLatest(CNXTEnvironments.Current.Connection);
    }
  }
}
