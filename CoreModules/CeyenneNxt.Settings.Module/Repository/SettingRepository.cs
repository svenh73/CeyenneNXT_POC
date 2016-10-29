using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concentrator.Core.Types.Entities.Main;
using Concentrator.Modules.Settings.Shared.Entities;
using Concentrator.Modules.Settings.Shared.Interfaces;

namespace Concentrator.Modules.Settings.Repository
{

  public class SettingRepository : Repository<Setting>, ISettingRepository
  {
  }
}
