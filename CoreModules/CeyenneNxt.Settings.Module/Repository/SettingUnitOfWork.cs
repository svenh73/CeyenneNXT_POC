using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concentrator.Core.Common.Core;
using Concentrator.Core.Common.Extensions;
using Concentrator.Core.Types;
using Concentrator.Modules.Settings.Shared.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace Concentrator.Modules.Settings.Repository
{

  public class SettingUnitOfWork : UnitOfWork, ISettingUnitOfWork
  {
    private ISettingRepository _settingRepository;
    public ISettingRepository SettingRepository
    {
      get
      {
        if (_settingRepository == null)
        {
          var connection = new Ninject.Parameters.ConstructorArgument("connection", DbConnection);
          _settingRepository = Core.Common.Ninject.Ninject.Kernel.Get<ISettingRepository>(connection);
        }
        return _settingRepository;
      }
    }
  }
}
