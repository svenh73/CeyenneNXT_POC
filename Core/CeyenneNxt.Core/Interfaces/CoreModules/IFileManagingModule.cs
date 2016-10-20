using System;
using System.Collections.Generic;

namespace CeyenneNxt.Core.Interfaces.CoreModules
{
  public interface IFileManagingModule
  {
    string SourceDirectory { get; set; }

    string ErrorDirectory { get; }

    string SucceededDirectory { get; }

    //List<IFileHandler> GetFiles(string filter = null);

    List<IFileHandler> GetFiles(string directory = null, string filter = null);

    void CleanupFiles(string directory, DateTime beforeDate);

    void SaveTextFile(string fileName, string Content);

    bool FileExists(string fileName);
  }
}
