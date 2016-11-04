using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Configuration;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace CeyenneNxt.Tools.DatabaseMigrator
{
  public static class Runner
  {
    public class MigrationOptions : IMigrationProcessorOptions
    {
      public bool PreviewOnly { get; set; }
      public string ProviderSwitches { get; set; }
      public int Timeout { get; set; }
    }

    public static void MigrateToLatest(string connectionString)
    {
      // var announcer = new NullAnnouncer();
      var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
      
      var migrationContext = new RunnerContext(announcer)
      {
        //Namespace = "CeyenneNxt.Settings.CoreModule.Migrations"
      };

      var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
      var factory =
          new FluentMigrator.Runner.Processors.SqlServer.SqlServer2014ProcessorFactory();

      var files = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CeyenneNxt.*.dll");
      foreach (var file in files)
      {
        using (var processor = factory.Create(connectionString, announcer, options))
        {
          var assembly = Assembly.LoadFile(file);
          var runner = new MigrationRunner(assembly, migrationContext, processor);
          runner.MigrateUp(true);
        }
      }
      
    }
  }
}
