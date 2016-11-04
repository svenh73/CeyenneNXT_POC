using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Settings.CoreModule
{
  public class SettingModuleSession : ModuleSession, ISettingsModuleSession
  {
    public SettingModuleSession() : base(new SqlConnection(CNXTEnvironments.Current.Connection))
    {
      
    }
  }
}
