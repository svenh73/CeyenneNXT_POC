using System.Data;
using System.Data.SqlClient;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class SettingsRepository : BaseRepository, ISettingsRepository
  {
    public SettingsRepository() : base(SchemaConstants.Default)
    {
    }

  }
}