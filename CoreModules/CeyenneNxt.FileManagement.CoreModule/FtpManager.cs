//using AuditLog4Net.Adapter;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Security;
//using System.Security.Cryptography.X509Certificates;

//namespace Concentrator.Objects.Ftp
//{
//  public class FtpManager : IEnumerable<FtpManager.RemoteFile>
//  {
//    private const string errorExtension = ".err";
//    private const string completeExtension = ".comp";
//    private const string searchPattern = ".";
//    private const int standardTimeout = 600000; // Milliseconds
//    private Uri _baseUri;
//    private string _userName;
//    private string _password;
//    private bool _useSSL;
//    private bool _usePassive;
//    private int _timeout;
//    private AuditLog4Net.Adapter.IAuditLogAdapter _log;

//    public FtpManager(string ftpUrl, AuditLog4Net.Adapter.IAuditLogAdapter log, int timeout = standardTimeout)
//      : this(ftpUrl, string.Empty, log)
//    { }

//    public FtpManager(string ftpUrl, string pathOnServer, AuditLog4Net.Adapter.IAuditLogAdapter log, int timeout = standardTimeout)
//      : this(ftpUrl, pathOnServer, string.Empty, string.Empty, log, timeout) { }

//    public FtpManager(string ftpUrl, string pathOnServer, string userName, string password, AuditLog4Net.Adapter.IAuditLogAdapter log, int timeout = standardTimeout)
//      : this(ftpUrl, pathOnServer, userName, password, true, true, log, timeout) { }

//    public FtpManager(string ftpUrl, string pathOnServer, string userName, string password, bool useSSL, bool usePassive, AuditLog4Net.Adapter.IAuditLogAdapter log, int timeout = standardTimeout)
//      : this(ftpUrl, pathOnServer, userName, password, useSSL, usePassive, false, log, timeout) { }

//    public FtpManager(string ftpUrl, string pathOnServer, string userName, string password, bool useSSL, bool usePassive, bool isIP, AuditLog4Net.Adapter.IAuditLogAdapter log, int timeout = standardTimeout)
//    {
//      pathOnServer = pathOnServer ?? String.Empty;
//      _baseUri = new Uri(new Uri(ftpUrl), pathOnServer);
//      _userName = userName;
//      _password = password;
//      _usePassive = usePassive;
//      _useSSL = useSSL;
//      _log = log;
//      _timeout = timeout;
//    }

//    private FtpWebRequest CreateRequest(Uri uri)
//    {
//      var request = (FtpWebRequest)FtpWebRequest.Create(uri);
//      request.Credentials = new NetworkCredential(_userName, _password);
//      request.EnableSsl = _useSSL;
//      request.UsePassive = _usePassive;
//      request.ServicePoint.ConnectionLimit = 8;
//      request.KeepAlive = false;
//      request.UseBinary = true;
//      request.Timeout = _timeout;

//      ServicePointManager.ServerCertificateValidationCallback = delegate (Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
//      {
//        return true;
//      };

//      return request;
//    }

//    private IEnumerable<string> GetFilesToDownload(Uri uri)
//    {
//      if (_log != null)
//        _log.DebugFormat("Try get files uri {0}", uri.AbsoluteUri);

//      var request = CreateRequest(uri);
//      request.Method = WebRequestMethods.Ftp.ListDirectory;

//      WebResponse resp = null;

//      try
//      {
//        resp = request.GetResponse();
//      }
//      catch (WebException ex)
//      {
//        using (var response = (FtpWebResponse)ex.Response)
//        {
//          if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
//          {
//            _log.AuditError("File Directory is empty", "FtpManager", GetType().FullName);

//            yield break;
//          }
//          else
//          {
//            _log.AuditError(string.Format("{0}: ex: {1}, ST: {2}", response.StatusCode, ex.Message, ex.StackTrace), "FtpManager", GetType().FullName);
//          }
//        }
//      }
//      catch (InvalidOperationException ex)
//      {
//        _log.AuditError(string.Format("FTPManager: Invalid operation ex: {0}, InnerEx: {1}, ST: {2}", ex.Message, ex.InnerException, ex.StackTrace), "FtpManager", GetType().FullName);
//        resp.Close();
//        yield break;
//      }

//      using (resp)
//      {
//        if (resp == null) yield break;

//        if (_log != null)
//          _log.DebugFormat("ftp message {0}", ((FtpWebResponse)(resp)).WelcomeMessage);

//        using (var reader = new StreamReader(resp.GetResponseStream()))
//        {
//          string line;

//          while (!String.IsNullOrEmpty(line = reader.Try(x => x.ReadLine(), string.Empty)))
//          {
//            if (!line.EndsWith(errorExtension) && !line.EndsWith(completeExtension) && line.Contains(searchPattern))
//              yield return line;
//          }
//        }
//        resp.Close();
//      }

//    }

//    public IEnumerable<string> GetFolders(Uri uri)
//    {
//      if (_log != null)
//        _log.DebugFormat("Try get folders uri {0}", uri.AbsoluteUri);

//      var request = CreateRequest(uri);
//      request.Method = WebRequestMethods.Ftp.ListDirectory;

//      WebResponse resp = null;

//      try
//      {
//        resp = request.Try(x => x.GetResponse(), null);

//        if (resp == null)
//          yield break;

//      }
//      catch (WebException ex)
//      {
//        using (var response = (FtpWebResponse)ex.Response)
//        {
//          if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
//          {
//            _log.AuditError("File Directory is empty", "FtpManager", GetType().FullName);

//            yield break;
//          }
//        }
//      }

//      List<string> directories = new List<string>();
//      using (resp)
//      {
//        if (resp == null) yield break;

//        if (_log != null)
//          _log.DebugFormat("ftp message {0}", ((FtpWebResponse)(resp)).WelcomeMessage);

//        using (var reader = new StreamReader(resp.GetResponseStream()))
//        {
//          string line;

//          while (!String.IsNullOrEmpty(line = reader.Try(x => x.ReadLine(), string.Empty)))
//          {
//            yield return line;
//          }
//        }
//        resp.Close();
//      }
//    }

//    public string DownloadToDisk(string downloadDir, string fileName)
//    {
//      var savePath = Path.Combine(downloadDir, Path.GetFileName(fileName));

//      _log.Info("Downloading file: " + fileName);

//      try
//      {
//        var request = CreateRequest(new Uri(_baseUri, fileName));
//        request.Method = WebRequestMethods.Ftp.DownloadFile;

//        using (var resp = request.GetResponse())
//        {
//          using (Stream stream = resp.GetResponseStream())
//          {
//            using (FileStream file = File.Create(savePath))
//            {
//              byte[] buf = new byte[8 * 1024];

//              int len;
//              while (stream != null && (len = stream.Read(buf, 0, buf.Length)) > 0)
//              {
//                file.Write(buf, 0, len);
//              }
//            }

//            _log.Info("Done downloading file: " + fileName);
//          }
//        }
//      }
//      catch (Exception e)
//      {
//        _log.Error(e.Message);
//      }

//      return savePath;
//    }

//    public RemoteFile OpenFile(string fileName)
//    {
//      var request = CreateRequest(new Uri(_baseUri, fileName));
//      request.Method = WebRequestMethods.Ftp.DownloadFile;

//      try
//      {
//        byte[] b;
//        using (var resp = request.Try(x => x.GetResponse(), null))
//        {
//          if (resp == null) { _log.Info("Could not open file because doesn't exist or invalid"); return null; }

//          using (Stream stream = resp.GetResponseStream())
//          using (MemoryStream ms = new MemoryStream())
//          {
//            stream.CopyTo(ms);
//            b = ms.ToArray();
//          }
//        }

//        MemoryStream str = new MemoryStream(b);
//        str.Position = 0;
//        return new RemoteFile(fileName, str);
//      }
//      catch (Exception ex)
//      {
//        //not a valid file
//        return null;
//      }
//    }

//    #region IEnumerable<Stream> Members
//    public List<string> GetFiles()
//    {
//      return GetFilesToDownload(_baseUri).ToList();
//    }

//    public bool Testconnection()
//    {
//      var request = CreateRequest(_baseUri);
//      request.Method = WebRequestMethods.Ftp.ListDirectory;

//      WebResponse resp = null;
//      resp = request.Try(x => x.GetResponse(), null);
//      if (resp != null)
//      {
//        return true;
//      }
//      return false;
//    }


//    public IEnumerator<RemoteFile> GetEnumerator()
//    {
//      var sourceFiles = GetFilesToDownload(_baseUri);

//      foreach (var sourceFile in sourceFiles)
//      {
//        if (_log != null)
//          _log.DebugFormat("Try get file {0}", new Uri(_baseUri, sourceFile));

//        using (var remoteFile = OpenFile(sourceFile))
//        {
//          if (remoteFile != null)
//            yield return remoteFile;
//        }
//      }
//    }

//    #endregion

//    #region IEnumerable Members

//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//    {
//      return GetEnumerator();
//    }

//    #endregion

//    public void Delete(string relativeFileUrl)
//    {
//      var request = CreateRequest(new Uri(_baseUri, relativeFileUrl));
//      request.Method = WebRequestMethods.Ftp.DeleteFile;

//      using (request.GetResponse())
//      { }
//    }

//    /// <summary>
//    /// Make sure to delete the files in the folder first
//    /// </summary>
//    /// <param name="relativeFolderUrl"></param>
//    public void DeleteFolder(string relativeFolderUrl)
//    {
//      //DeleteFilesInFolder(relativeFolderUrl);
//      //var request = CreateRequest(new Uri(_baseUri, relativeFolderUrl));
//      //request.Method = WebRequestMethods.Ftp.RemoveDirectory;

//      //using (request.GetResponse())
//      //{ }
//    }

//    private void DeleteFilesInFolder(string relativeFolderUrl)
//    {
//      var folderUri = new Uri(_baseUri, relativeFolderUrl);
//      var filesToDelete = GetFilesToDownload(folderUri);

//      foreach (var file in filesToDelete)
//      {
//        var tempBaseUri = new Uri(_baseUri, relativeFolderUrl);
//        var request = CreateRequest(new Uri(tempBaseUri.ToString() + "/" + file));
//        request.Method = WebRequestMethods.Ftp.DeleteFile;

//        using (request.GetResponse())
//        { }
//      }
//    }

//    public void MarkAsNew(string relativeFileUrl)
//    {
//      var request = CreateRequest(new Uri(_baseUri, relativeFileUrl));
//      request.Method = WebRequestMethods.Ftp.Rename;
//      request.RenameTo = Path.GetFileName(Path.GetFileNameWithoutExtension(relativeFileUrl));
//      using (request.GetResponse())
//      { }
//    }

//    public void MarkAsError(string relativeFileUrl)
//    {
//      var request = CreateRequest(new Uri(_baseUri, relativeFileUrl));
//      request.Method = WebRequestMethods.Ftp.Rename;
//      request.RenameTo = Path.GetFileName(relativeFileUrl) + errorExtension;
//      using (request.GetResponse())
//      { }
//    }

//    public void MarkAsComplete(string relativeFileUrl)
//    {
//      var request = CreateRequest(new Uri(_baseUri, relativeFileUrl));
//      request.Method = WebRequestMethods.Ftp.Rename;
//      request.RenameTo = Path.GetFileName(relativeFileUrl) + completeExtension;
//      using (request.GetResponse())
//      { }
//    }

//    /// <summary>
//    /// Uploads a file to the ftp
//    /// </summary>
//    /// <param name="data"></param>
//    /// <param name="fileName"></param>
//    /// <param name="uploadDir">Supply if the path is different than the base url </param>
//    public void Upload(Stream data, string fileName, string uploadDir = null)
//    {

//      var request = CreateRequest(new Uri(string.IsNullOrEmpty(uploadDir) ? _baseUri : new Uri(_baseUri, uploadDir), fileName));
//      request.Method = WebRequestMethods.Ftp.UploadFile;

//      //10 minutes
//      request.Timeout = _timeout;

//      //byte[] bytes = new byte[data.Length];
//      data.Position = 0;
//      request.Proxy = null;
//      _log.InfoFormat("Try upload {0} to {1}", fileName, request.RequestUri);

//      using (Stream s = request.GetRequestStream())
//      {
//        data.CopyTo(s);
//      }
//      _log.InfoFormat("Finished uploading file {0} to {1}", fileName, request.RequestUri);
//    }

//    public bool DirectoryExists(string path)
//    {
//      bool directoryExists = false;
//      var pathRequest = new Uri(_baseUri, path);
//      var request = CreateRequest(pathRequest);
//      request.UseBinary = true;
//      request.KeepAlive = true;
//      request.Timeout = _timeout;
//      request.ServicePoint.ConnectionLimit = 6;
//      request.ReadWriteTimeout = _timeout;
//      request.Method = WebRequestMethods.Ftp.ListDirectory;
//      try
//      {
//        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
//        {
//          directoryExists = true;
//        }
//      }
//      catch (WebException)
//      {
//        directoryExists = false;
//      }
//      return directoryExists;
//    }

//    public void UploadFileInDir(string fileUri, Stream data, string fileName, IAuditLogAdapter log)
//    {
//      string path = string.Empty;
//      foreach (var part in fileUri.Split('\\'))
//      {
//        if (!string.IsNullOrEmpty(part))
//        {
//          if (!string.IsNullOrEmpty(path))
//            path += "/" + part;
//          else
//            path = part;
//          try
//          {
//            var pathRequest = new Uri(_baseUri, path);
//            var request = CreateRequest(pathRequest);
//            request.Method = WebRequestMethods.Ftp.MakeDirectory;
//            using (FtpWebResponse ftpResp = request.GetResponse() as FtpWebResponse)
//            { }
//            request.Abort();
//          }
//          catch
//          {
//          }
//        }
//      }
//      if (!string.IsNullOrEmpty(path))
//      {
//        Upload(data, fileName, path);
//      }
//      else
//      {
//        Upload(data, fileName);
//      }
//    }

//    public class RemoteFile : IDisposable
//    {
//      public readonly string FileName;
//      public readonly Stream Data;

//      public RemoteFile(string fileName, Stream data)
//      {
//        FileName = fileName;
//        Data = data;
//      }

//      #region IDisposable Members

//      public void Dispose()
//      {
//        if (Data != null)
//          Data.Dispose();

//      }

//      #endregion
//    }

//  }
//}
