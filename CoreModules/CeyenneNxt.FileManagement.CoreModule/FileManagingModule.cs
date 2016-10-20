using System;
using System.Collections.Generic;
using System.IO;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Interfaces.CoreModules;

namespace CeyenneNxt.FileManagement.CoreModule
{
  public class FileManagingModule : IFileManagingModule
  {

    public ISettingModule SettingModule { get; set; }
    public ILoggingModule LoggingModule { get; set; }

    public FileManagingModule(ISettingModule settingModule, ILoggingModule logService)
    {
      SettingModule = settingModule;
      LoggingModule = logService;
    }

    public string SourceDirectory { get; set; }

    public string ErrorDirectory { get { return Path.Combine(SourceDirectory, "Error"); } }

    public string SucceededDirectory { get { return Path.Combine(SourceDirectory, "Succeeded"); } }

    public List<IFileHandler> GetFiles(string directory = null,string filter = null)
    {
      if (directory == null)
      {
        directory = SourceDirectory;
      }

      var files = new List<IFileHandler>();
      IEnumerable<string> filelist = null;
      if (filter != null)
      {
        filelist = Directory.EnumerateFiles(directory, filter);
      }
      else
      {
        filelist = Directory.EnumerateFiles(directory);
      }
      
      foreach (var file in filelist)
      {
        files.Add(new FileHandler(this)
        {
          FilePath = file
        });
      }
      return files;
    }

    public bool FileExists(string fileName)
    {
      return File.Exists(Path.Combine(SourceDirectory, fileName));
    }

    public void SaveTextFile(string fileName, string Content)
    {
      var path = Path.Combine(SourceDirectory, fileName);
      EnsureDirectories();
      using (StreamWriter stream = new StreamWriter(path))
      {
        stream.Write(Content);
      }
    }

    public void CleanupFiles(string directory, DateTime beforeDate)
    {
      var files = GetFiles(directory);
      foreach (var fileHandler in files)
      {
        if (fileHandler.DateTime < beforeDate)
        {
          fileHandler.Delete();
          //LogService.LogInfo(GetType().Name, String.Format("Deleted file {0}",fileHandler.FileName));
        }
      }
    }

    public void EnsureDirectories()
    {
      if (String.IsNullOrEmpty(SourceDirectory))
      {
        throw new Exception("No SourceDirectory set");
      }

      if (!Directory.Exists(SourceDirectory))
      {
        Directory.CreateDirectory(SourceDirectory);
      }
      if (!Directory.Exists(ErrorDirectory))
      {
        Directory.CreateDirectory(ErrorDirectory);
      }
      if (!Directory.Exists(SucceededDirectory))
      {
        Directory.CreateDirectory(SucceededDirectory);
      }
    }

  }
}
