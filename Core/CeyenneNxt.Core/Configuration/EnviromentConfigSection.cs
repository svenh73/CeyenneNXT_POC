using System.Configuration;

namespace CeyenneNxt.Core.Configuration
{
  internal class EnvironmentConfigSection : ConfigurationSection
  {
    [ConfigurationProperty("Current", DefaultValue = "false", IsRequired = false)]
    public string Current
    {
      get
      {
        return (string)this["Current"];
      }
      set
      {
        this["Current"] = value;
      }
    }

    [ConfigurationProperty("Environments", IsDefaultCollection = true)]
    [ConfigurationCollection(typeof(EnvironmentElementCollection),
        AddItemName = "Env",
        ClearItemsName = "Clear",
        RemoveItemName = "Remove")]
    public EnvironmentElementCollection Environments
    {
      get { return (EnvironmentElementCollection)this["Environments"]; }
    }
  }

  internal class EnvironmentElement : ConfigurationElement
  {
    [ConfigurationProperty("Name", IsRequired = true)]
    [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 0, MaxLength = 35)]
    public string Name
    {
      get
      {
        return (string)this["Name"];
      }
      set
      {
        this["Name"] = value;
      }
    }

    [ConfigurationProperty("WebApiBaseUrl", IsRequired = true)]
    [StringValidator(MinLength = 0, MaxLength = 35)]
    public string WebApiBaseUrl
    {
      get
      {
        return (string)this["WebApiBaseUrl"];
      }
      set
      {
        this["WebApiBaseUrl"] = value;
      }
    }

    [ConfigurationProperty("CustomerName", IsRequired = true)]
    [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 0, MaxLength = 20)]
    public string CustomerName
    {
      get
      {
        return (string)this["CustomerName"];
      }
      set
      {
        this["CustomerName"] = value;
      }
    }

    [ConfigurationProperty("AssemblyRootFolder", IsRequired = true)]
    [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'", MinLength = 0, MaxLength = 1024)]
    public string AssemblyRootFolder
    {
      get
      {
        return (string)this["AssemblyRootFolder"];
      }
      set
      {
        this["AssemblyRootFolder"] = value;
      }
    }

    [ConfigurationProperty("EnvironmentType", DefaultValue = "development", IsRequired = true)]
    [RegexStringValidator(@"^(?i)(development|test|staging|production)$")]
    public string EnvironmentType
    {
      get
      {
        return (string)this["EnvironmentType"];
      }
      set
      {
        this["EnvironmentType"] = value;
      }
    }

    [ConfigurationProperty("MachineName", IsRequired = false)]
    public string MachineName
    {
      get
      {
        return (string)this["MachineName"];
      }
      set
      {
        this["MachineName"] = value;
      }
    }

    [ConfigurationProperty("MaximumWorkerCount", IsRequired = false)]
    public int MaximumWorkerCount
    {
      get
      {
        return (int)this["MaximumWorkerCount"];
      }
      set
      {
        this["MaximumWorkerCount"] = value;
      }
    }

    [ConfigurationProperty("Connection", IsRequired = true)]
    public string Connection
    {
      get
      {
        return (string)this["Connection"];
      }
      set
      {
        this["Connection"] = value;
      }
    }

    [ConfigurationProperty("ServiceBusConnectionString", IsRequired = true)]
    public string ServiceBusConnectionString
    {
      get
      {
        return (string)this["ServiceBusConnectionString"];
      }
      set
      {
        this["ServiceBusConnectionString"] = value;
      }
    }
  }

  internal class EnvironmentElementCollection : ConfigurationElementCollection
  {
    protected override ConfigurationElement CreateNewElement()
    {
      return new EnvironmentElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((EnvironmentElement)element).Name;
    }

    public EnvironmentElement this[int index]
    {
      get
      {
        return (EnvironmentElement)BaseGet(index);
      }
    }

    public new EnvironmentElement this[string Name]
    {
      get
      {
        return (EnvironmentElement)BaseGet(Name);
      }
    }

    public int IndexOf(EnvironmentElement environment)
    {
      return BaseIndexOf(environment);
    }


  }
}
