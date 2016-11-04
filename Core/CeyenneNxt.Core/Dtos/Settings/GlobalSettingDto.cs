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
    public override SettingValueDto this[int? id]
    {
      get
      {
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