namespace CeyenneNxt.Core.Constants
{
  public class Constants
  {
    public class BrokeredMessageProperties
    {
      public const string VendorId = "VendorId";
      public const string VendorName = "VendorName";
      public const string ChannelId = "ChannelId";
      public const string ChannelName = "ChannelName";
      public const string DataSource = "DataSource";
    }

    public class SettingNames
    {
      public const string CNXTRootPath = "CNXTRootPath";
      public const string SourceDirectory = "SourceDirectory";
      public const string SearchPattern = "SearchPattern";
      public const string TargetDirectory = "TargetDirectory";
      public const string ProcessedDirectory = "ProcessedDirectory";
      public const string ErrorDirectory = "ErrorDirectory";

      public const string VendorID = "VendorID";

      public const string ChannelID = "ChannelID";


      public const string MaximumWaitSecondsForReceiveFromBus = "MaximumWaitSecondsForReceiveFromBus";
    }

    public class General
    { 
      public const string AssemblyPrefix = "Concentrator.";
    }


  }
}
