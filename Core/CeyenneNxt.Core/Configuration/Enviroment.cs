using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Exceptions;

namespace CeyenneNxt.Core.Configuration
{
  
  public class CNXTEnvironments
  {
    private CNXTEnvironments()
    { }

    public string Name
    {
      get;
      private set;
    }

    public string CustomerName
    {
      get;
      private set;
    }

    public string AssemblyRootFolder
    {
      get;
      private set;
    }

    public string WebApiBaseUrl
    {
      get;
      private set;
    }

    public string Connection { get; private set; }

    public string ServiceBusConnectionString { get; set; }  

    public string MachineName
    {
      get;
      private set;
    }

    public int MaximumWorkerCount
    {
      get;
      private set;
    }

    public EnvironmentType EnvironmentType { get; set; }

    public static CNXTEnvironments Current
    {
      get
      {
        if (!String.IsNullOrEmpty(ConfigSection.Current))
        {
          var enviroment = AllEnvironments.FirstOrDefault(e => e.Name == ConfigSection.Current);

          if (enviroment.MachineName != "." && enviroment.MachineName != Environment.MachineName)
          {
            throw new EnvironmentException(Environment.MachineName,enviroment.MachineName);
          }
          return enviroment;
        }
        else
        {
          return null;
        }
      }
    }

    private static List<CNXTEnvironments> _allEnvironments = null;
    private static volatile string lockString = "THISISMYLOCK,KEEP AWAY";

    public static List<CNXTEnvironments> AllEnvironments
    {
      get
      {

        if (_allEnvironments == null)
        {
          lock (lockString)
          {
            _allEnvironments = new List<CNXTEnvironments>(ConfigSection.Environments.Count);

            foreach (EnvironmentElement el in ConfigSection.Environments)
            {
              _allEnvironments.Add(new CNXTEnvironments
              {
                Name = el.Name,
                WebApiBaseUrl = el.WebApiBaseUrl,
                Connection = el.Connection,
                ServiceBusConnectionString = el.ServiceBusConnectionString,
                MachineName = el.MachineName,
                EnvironmentType = (EnvironmentType)Enum.Parse(typeof(EnvironmentType), el.EnvironmentType, true),
                CustomerName = el.CustomerName,
                MaximumWorkerCount = el.MaximumWorkerCount,
                AssemblyRootFolder = el.AssemblyRootFolder
              });
            }
          }
        }

        return _allEnvironments;
      }
    }

    private static EnvironmentConfigSection _configSection = null;
    private static EnvironmentConfigSection ConfigSection
    {
      get
      {

//#if DEBUG
//        // The package manager console reads the config files from the root of the project folder and not from the 'bin' directory.
//        // To be sure the 'environment.generic.config' can be loaded,it will force load the config file (in debug mode) from the 'bin' directory
//        ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
//        var filename = Path.GetFileName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
//        configMap.ExeConfigFilename = String.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, filename);
//        var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

//        return (EnvironmentConfigSection) config.GetSection("Environment");
//#endif

        return _configSection ?? (_configSection = (EnvironmentConfigSection)ConfigurationManager.GetSection("Environment"));
      }
    }
  }
}
