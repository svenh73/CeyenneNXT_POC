using System.Data;
using System.Data.SqlClient;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Entities;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Orders.Module.Repositories
{
  public class SettingValueRepository : BaseRepository<SettingValue>, ISettingValueRepository
  {
    public SettingValueRepository() : base(SchemaConstants.Default)
    {

    }

  }
}