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
    public override SettingValueDto this[int? id]
    {
      get {

          SettingValueDto settingValue = null;

          settingValue = this.FirstOrDefault(p => p.EnvironmentType != null);
          if (settingValue == null)
          {
            settingValue = this.FirstOrDefault(p => p.EnvironmentType == null);
          }
          return settingValue;
      }
    }

    public override SettingValueDto Value
    {
      get { return this[null]; }
    }
  }
}