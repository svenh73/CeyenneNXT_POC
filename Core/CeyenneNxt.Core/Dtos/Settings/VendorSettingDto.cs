#region

using System.Linq;

#endregion

namespace CeyenneNxt.Core.Dtos.Settings
{
  /// <summary>
  /// A setting that's defined within a domain and is related to a vendor
  /// </summary>
  public class VendorSettingDto : BaseSettingDto
  {
    public override SettingValueDto this[int? id]
    {
      get
      {
        SettingValueDto settingValue = null; 

        if (id.HasValue)
        {
          settingValue = this.FirstOrDefault(p => p.EnvironmentType != null && p.VendorID == id);
          if (settingValue == null)
          {
            settingValue = this.FirstOrDefault(p => p.EnvironmentType == null && p.VendorID == id);
          }
          if (settingValue == null)
          {
            settingValue = this.FirstOrDefault(p => p.EnvironmentType == null && p.VendorID == null);
          }
        }
        else
        {
          settingValue = this.FirstOrDefault(p => p.EnvironmentType != null && p.VendorID == null);
          if (settingValue == null)
          {
            settingValue = this.FirstOrDefault(p => p.EnvironmentType == null && p.VendorID == null);
          }
        }
        return settingValue;
      }
    }
    public override SettingValueDto Value => this[null];
  }
}