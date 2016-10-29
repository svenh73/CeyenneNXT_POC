#region

using System.Linq;

#endregion

namespace CeyenneNxt.Core.Dtos.Settings
{
  /// <summary>
  ///  A setting that is defined within a domain but it's not related to a vendor or a chennel
  /// </summary>
  public class GeneralSettingDto : BaseSettingDto
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