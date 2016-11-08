using System;
using System.Collections.Generic;
using CeyenneNxt.Core.Configuration;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Dtos.Settings;
using CeyenneNxt.Core.Enums;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.Ioc;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CeyenneNxt.Settings.CoreModule.Test
{
  [TestClass]
  public class TestSettings
  {
    private ISettingModule _settingModule;

    public string Domain
    {
      get
      {
        return GetType().Name;
      }
    }

    public ISettingModule SettingModule
    {
      get
      {
        if (_settingModule == null)
        {
          _settingModule = ServiceLocator.Current.GetInstance<ISettingModule>();
        }
        return _settingModule;
      }
    }

    public SettingCollection GetSettingCollection(EnvironmentType environmentType)
    {

      using (var session = new SettingModuleSession())
      {
        var collection = new SettingCollection(Domain, environmentType)
          .Add(new GlobalSettingDto() { Name = Constants.SettingNames.CNXTRootPath, DataType = SettingDataType.String, Required = true })
          .Add(new GeneralSettingDto() { Domain = Domain, Name = Constants.SettingNames.TargetDirectory, DataType = SettingDataType.Int, Required = true, DefaultValue = @"c:\test" })
          .Add(new VendorSettingDto() { Domain = Domain, Name = Constants.SettingNames.SourceDirectory, DataType = SettingDataType.String, Required = true });

        return SettingModule.LoadSettings(session, collection);
      }
    }

    public TestSettings()
    {
      var container = IocBootstrapper.Start(ApplicationType.Process);
    }


    [TestMethod]
    public void InsertGlobalSetting()
    {
      using (var session = new SettingModuleSession())
      {
        var id = SettingModule.InsertOrUpdateSetting(session,
          new GlobalSettingDto()
          {
            Name = "TestSetting1Name",
            DataType = SettingDataType.String,
            Active = true,
            Required = true
          });

        Assert.IsTrue(id > 0);
      }
    }

    [TestMethod]
    public void DeleteGlobalSetting()
    {
      using (var session = new SettingModuleSession())
      {
        var results = SettingModule.DeleteSetting(session, new GlobalSettingDto()
        {
          Name = "TestSetting1Name"
        });

        Assert.IsTrue(results);
      }
    }

    [TestMethod]
    public void InsertSettingValues()
    {
      using (var session = new SettingModuleSession())
      {
        try
        {
          SettingModule.InsertOrUpdateGlobalSettingValue(session, Constants.SettingNames.CNXTRootPath, null, @"c:\temp");
          SettingModule.InsertOrUpdateGlobalSettingValue(session, Constants.SettingNames.CNXTRootPath, EnvironmentType.Development, @"d:\temp");

          SettingModule.InsertOrUpdateGeneralSettingValue(session, Domain, Constants.SettingNames.SourceDirectory, null, @"\test");
          SettingModule.InsertOrUpdateGeneralSettingValue(session, Domain, Constants.SettingNames.SourceDirectory, EnvironmentType.Development, @"\source");
          SettingModule.InsertOrUpdateGeneralSettingValue(session, Domain, Constants.SettingNames.SourceDirectory, EnvironmentType.Production, @"\production");

          SettingModule.InsertOrUpdateVendorSettingValue(session, Domain, Constants.SettingNames.TargetDirectory, null, 1, @"\vendor1target");
          SettingModule.InsertOrUpdateVendorSettingValue(session, Domain, Constants.SettingNames.TargetDirectory, null, 2, @"\vendor2target");
          SettingModule.InsertOrUpdateVendorSettingValue(session, Domain, Constants.SettingNames.TargetDirectory, EnvironmentType.Production, 1, @"\vendor1targetproduction");

          Assert.IsTrue(true);
        }
        catch (Exception ex)
        {
          Assert.Fail(ex.Message);
        }
      }

    }

    [TestMethod]
    public void TestGlobalSetting()
    {
      var collection = GetSettingCollection(EnvironmentType.Production);
      Assert.AreEqual(@"c:\temp", collection[Constants.SettingNames.CNXTRootPath]);

      collection = GetSettingCollection(EnvironmentType.Development);
      Assert.AreEqual(@"d:\temp", collection[Constants.SettingNames.CNXTRootPath]);
    }

    [TestMethod]
    public void TestGeneralSetting()
    {
      var collection = GetSettingCollection(EnvironmentType.Development);

      Assert.AreEqual(@"\source", collection[Constants.SettingNames.SourceDirectory]);

      collection = GetSettingCollection(EnvironmentType.Staging);
      Assert.AreEqual(@"\test", collection[Constants.SettingNames.SourceDirectory]);

      collection = GetSettingCollection(EnvironmentType.Production);
      Assert.AreEqual(@"\production", collection[Constants.SettingNames.SourceDirectory]);
    }

    [TestMethod]
    public void TestVendorSetting()
    {
      var collection = GetSettingCollection(EnvironmentType.Development);

      Assert.AreEqual(@"\vendor1target", collection[Constants.SettingNames.TargetDirectory, 1]);
      Assert.AreEqual(@"\vendor2target", collection[Constants.SettingNames.TargetDirectory, 2]);

      collection = GetSettingCollection(EnvironmentType.Production);

      Assert.AreEqual(@"\vendor1targetproduction", collection[Constants.SettingNames.TargetDirectory, 1]);
      Assert.AreEqual(@"\vendor2target", collection[Constants.SettingNames.TargetDirectory, 2]);
    }
  }
}
