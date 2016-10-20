#region

using System;
using System.IO;
using System.Text;
using CeyenneNxt.Core.Interfaces;

#endregion

namespace CeyenneNxt.FileManagement.CoreModule
{
  public class FileHandler : IFileHandler
  {
    private FileManagingModule _owner;
    private string _content;

    public FileHandler(FileManagingModule owner)
    {
      _owner = owner;
    }

    public string FilePath { get; set; }

    public string FileName => Path.GetFileName(FilePath);

    public string Content {
      get
      {
        if (_content == null)
        {
          if (!File.Exists(FilePath))
          {
            throw new FileNotFoundException(FilePath);
          }

          using (var stream = new FileStream(FilePath, FileMode.Open))
          {
            TextReader reader = new StreamReader(stream, Encoding.UTF8);
            _content = reader.ReadToEnd();
            stream.Close();
          }
        }
        return _content;
      }
    }

    string IFileHandler.FileName
    {
      get { return FileName; }
    }

    string IFileHandler.FilePath
    {
      get { return FilePath; }
    }

    public DateTime DateTime
    {
      get { return File.GetCreationTime(FilePath); }
    }

    public void Delete()
    {
      if (File.Exists(FilePath))
      {
        File.Delete(FilePath);
      }
    }

    public void MoveToErrorFolder(string exception = null)
    {
      if (!File.Exists(FilePath))
      {
        throw new FileNotFoundException(FilePath);
      }

        _owner.EnsureDirectories();
      var newfilename = Path.Combine(_owner.ErrorDirectory, 
        MakeUnqiueFileName(_owner.ErrorDirectory, Path.GetFileName(FileName)));
      File.Move(FilePath, newfilename);

      if (exception != null)
      {
        var errorFileName = Path.GetFileNameWithoutExtension(newfilename) + ".err";
        WriteStringToFile(errorFileName, exception);
      }
    }

    public void WriteStringToFile(string filePath, string content)
    {
      System.IO.File.WriteAllText(filePath, content);
    }

    public void MoveToSuccessFolder()
    {
      if (!File.Exists(FilePath))
      {
        throw new FileNotFoundException(FilePath);
      }

      _owner.EnsureDirectories();
      var newfilename = Path.Combine(_owner.SucceededDirectory, 
        MakeUnqiueFileName(_owner.SucceededDirectory,Path.GetFileName(FileName)));
      File.Move(FilePath, newfilename);
    }

    private string MakeUnqiueFileName(string path, string filename)
    {
      var newfilename = filename;
      var newpath = Path.Combine(path, newfilename);
      var index = 0;
      while (File.Exists(newpath))
      {
        index ++;
        var tempname = Path.GetFileNameWithoutExtension(newfilename);
        newfilename = String.Format("{0}_{1}{2}", tempname, index, Path.GetExtension(filename));
        newpath = Path.Combine(path, newfilename);

      }
      return newfilename;
    }
  }
}