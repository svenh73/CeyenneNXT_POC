using CeyenneNxt.Core.Attributes;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Core.Entities
{

  [Table("SettingValue", SchemaConstants.Default)]
  public partial class SettingValue : BaseEntity
  {
    public int SettingID { get; set; }
    public int? VendorID { get; set; }
    public int? ChannelID { get; set; }
    public EnvironmentType? EnvironmentType { get; set; }
    public string Value { get; set; }
    //public virtual Channel Channel { get; set; }
    //public virtual Setting Setting { get; set; }
    //public virtual Vendor Vendor { get; set; }
  }
}