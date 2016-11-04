#region

using System.Linq;

#endregion

namespace CeyenneNxt.Core.Dtos.Settings
{
  /// <summary>
  /// A setting that's defined within a domain and is related to a channel
  /// </summary>
  public class ChannelSettingDto : BaseSettingDto
  {

    public override SettingValueDto this[int? id]
    {
      get
      {
        SettingValueDto settingValue = null;

        if (id.HasValue)
        {
          settingValue = this.FirstOrDefault(p => p.EnvironmentType != null && p.ChannelID == id);
          if (settingValue == null)
          {
            settingValue = this.FirstOrDefault(p => p.EnvironmentType == null && p.ChannelID == id);
          }
          if (settingValue == null)
          {
            settingValue = this.FirstOrDefault(p => p.EnvironmentType == null && p.ChannelID == null);
          }
        }
        else
        {
          settingValue = this.FirstOrDefault(p => p.EnvironmentType != null && p.ChannelID == null);
          if (settingValue == null)
          {
            settingValue = this.FirstOrDefault(p => p.EnvironmentType == null && p.ChannelID == null);
          }
        }
        return settingValue;
      }
    }

    public override SettingValueDto Value => this[null];
  }
}