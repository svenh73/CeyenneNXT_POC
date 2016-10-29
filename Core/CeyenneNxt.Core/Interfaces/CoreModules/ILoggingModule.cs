using System.Diagnostics;

namespace CeyenneNxt.Core.Interfaces.CoreModules
{
  public interface ILoggingModule
  {
    void Log(TraceEventType eventType, string message, params object[] arguments);
    void LogError(string processid, string message, string stacktrace);
    void LogError(string processid, string message, string stacktrace, string entityname, int? entityid, object objectToLog = null);
    void LogFatal(string processid, string message, string stacktrace);
    void LogFatal(string processid, string message, string stacktrace, string entityname, int entityid, object objectToLog = null);
    void LogFatal(string processid, string message, string stacktrace, string entityname = null, int? entityid = default(int?), object objectToLog = null);
    void LogInfo(string processid, string message);
    void LogWarning(string processid, string message);
    void LogWarning(string processid, string message, string entityname, int? entityid = default(int?), object objectToLog = null);
  }
}