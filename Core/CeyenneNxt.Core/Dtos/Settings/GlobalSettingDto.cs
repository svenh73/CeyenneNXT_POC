#region

using System.Linq;

#endregion

namespace CeyenneNxt.Core.Dtos.Settings
{
  /// <summary>
  /// A settings that's application wide defined, like the global root path etc.
  /// </summary>
  public class GlobalSettingDto : BaseSettingDto
  {
    public override string this[int? id]
    {
      get { return Value; }
    }

    public override string Value
    {
      get
      {
        var value = this.FirstOrDefault(p => p.EnvironmentType != null && p.ChannelID == null && p.VendorID == null);
        if (value == null)
        {
          value = this.FirstOrDefault(p => p.EnvironmentType == null && p.ChannelID == null && p.VendorID == null);
        }
        return value?.Value;
      }
    }
  }
}