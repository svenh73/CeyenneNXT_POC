using System;

namespace CeyenneNxt.Core.Interfaces
{
  public interface IFileHandler
  {
    string FileName { get; }

    string FilePath { get; }

    string Content { get; }

    DateTime DateTime { get; }

    void MoveToErrorFolder(string exception = null);

    void MoveToSuccessFolder();

    void Delete();
  }
}