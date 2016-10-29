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

    public override string this[int? channelID]
    {
      get
      {
        var value = this.FirstOrDefault(p => p.EnvironmentType != null && p.ChannelID == channelID);
        if (value == null)
        {
          value = this.FirstOrDefault(p => p.EnvironmentType == null && p.ChannelID == channelID);
        }
        if (value == null)
        {
          value = this.FirstOrDefault(p => p.EnvironmentType == null && p.ChannelID == null);
        }
        return value?.Value;
      }
    }

    public override string Value => this[null];
  }
}