#region



#endregion

using System;
using CeyenneNxt.Core.Enums;

namespace CeyenneNxt.Core.Dtos.Settings
{
  public class SettingValueDto
  {

    public EnvironmentType? EnvironmentType { get; set; }

    public int? ChannelID { get; set; }

    public int? VendorID { get; set; }

    public string Value { get; set; }

    public int ToInt()
    {
      return int.Parse(Value);
    }

    public string ToString()
    {
      return Value;
    }

    public bool ToBoolean()
    {
      return Value == "1" || (Value != null && Value.ToLower() == "true") ? true : false;
    }



  }
}