using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Dtos.Settings;

namespace CeyenneNxt.Settings.CoreModule
{
  public class MissingSettingException : Exception
  {
    public MissingSettingException(List<BaseSettingDto> settings)
    {
      Settings = settings;
    }

    public List<BaseSettingDto> Settings { get; private set; }

    public override string Message
    {
      get
      {
        return String.Format("The following required settings are missing: {0}",
          String.Join(",", Settings.Where(o => o.Required).Select(p => p.Name)));
      }
    }
  }
}
