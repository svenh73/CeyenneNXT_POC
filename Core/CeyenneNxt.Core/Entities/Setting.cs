using System.Collections.Generic;
using CeyenneNxt.Core.Attributes;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Entities;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Core.Entities
{ 

  [Table("Setting", SchemaConstants.Default)]
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