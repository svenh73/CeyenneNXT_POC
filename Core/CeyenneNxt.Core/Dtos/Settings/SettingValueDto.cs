#region



#endregion

using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Core.Dtos.Settings
{
  public class SettingValueDto
  {

    public EnvironmentType? EnvironmentType { get; set; }

    public int? ChannelID { get; set; }

    public int? VendorID { get; set; }

    public string Value { get; set; }
  }
}