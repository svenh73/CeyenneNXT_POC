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
    public override string this[int? vendorID]
    {
      get
      {
        var value = this.FirstOrDefault(p => p.EnvironmentType != null && p.VendorID == vendorID);
        if (value == null)
        {
          value = this.FirstOrDefault(p => p.EnvironmentType == null && p.VendorID == vendorID);
        }
        if (value == null)
        {
          value = this.FirstOrDefault(p => p.EnvironmentType == null && p.VendorID == null);
        }
        return value?.Value;
      }
    }

    public override string Value => this[null];
  }
}