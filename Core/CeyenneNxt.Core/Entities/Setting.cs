using System.Collections.Generic;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Core.Entities
{
  public partial class Setting : BaseEntity
  {
    public string Name { get; set; }
    public string Domain { get; set; }
    public SettingDataType DataType { get; set; }
    public bool Active { get; set; }
    public SettingType SettingType { get; set; }
    public virtual ICollection<SettingValue> SettingValues { get; set; } = new List<SettingValue>();
  }
}