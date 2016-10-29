using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CeyenneNxt.Core.Constants;
using CeyenneNxt.Core.Diagnostics;
using CeyenneNxt.Core.Interfaces;
using CeyenneNxt.Core.Interfaces.CoreModules;
using CeyenneNxt.Core.Types;

namespace CeyenneNxt.Settings.CoreModule
{
  public class LoggingModule : BaseModule, ILoggingModule
  {
    public string DataSource => GetType().Name;

    public LoggingModule()
    {
      InitTrace("Test");
    }

    protected TraceSource TraceSource { get; set; }

    private readonly Dictionary<string, object> _contextProperties = new Dictionary<string, object>(); //list to set scope variables which will be serialized and logged automatically

    public void Log(TraceEventType eventType, string message, params object[] arguments)
    {
      //TODO: log everything also the context data to the database, but only log messages based on log settings to logfile. Let tracelistener handle this
      if (_contextProperties.Any())
      {
        TraceSource.TraceData(eventType, 0, _contextProperties);
      }

      TraceSource.TraceEvent(eventType, 0, message, arguments);
    }

    public void LogFatal(string processid, string message, string stacktrace, string entityname = null, int? entityid = null, object objectToLog = null)
    {
      throw new NotImplementedException();
    }

    public void LogInfo(string processid, string message)
    {
      var trace = new TraceModel() { ProcessID = processid, Message = message };
      TraceSource.TraceData(TraceEventType.Information, 0, trace);
    }

    public void LogWarning(string processid, string message)
    {
      var trace = new TraceModel() { ProcessID = processid, Message = message };
      TraceSource.TraceData(TraceEventType.Warning, 0, trace);
    }

    public void LogWarning(string processid, string message, string entityname, int? entityid = null, object objectToLog = null)
    {
      TraceModel trace = null;
      if (!String.IsNullOrEmpty(entityname))
      {
        trace = new EntityTraceModel() { ProcessID = processid, Message = message, EntityName = entityname, EntityID = entityid, ObjectToLog = objectToLog };
      }
      else
      {
        trace = new TraceModel() { ProcessID = processid, Message = message };
      }

      TraceSource.TraceData(TraceEventType.Warning, 0, trace);
    }

    public void LogError(string processid, string message, string stacktrace)
    {
      var trace = new TraceModel() { DataSource = DataSource, ProcessID = processid, Message = message, StackTrace = stacktrace };
      TraceSource.TraceData(TraceEventType.Error, 0, trace);
    }

    public void LogError(string processid, string message, string stacktrace, string entityname, int? entityid, object objectToLog = null)
    {
      TraceModel trace = null;
      if (!String.IsNullOrEmpty(entityname))
      {
        trace = new EntityTraceModel() { DataSource = DataSource, ProcessID = processid, Message = message, EntityName = entityname, EntityID = entityid, ObjectToLog = objectToLog };
      }
      else
      {
        trace = new TraceModel() { DataSource = DataSource, ProcessID = processid, Message = message };
      }
      TraceSource.TraceData(TraceEventType.Error, 0, trace);
    }

    public void LogFatal(string processid, string message, string stacktrace)
    {
      var trace = new TraceModel() { DataSource = DataSource, ProcessID = processid, Message = message, StackTrace = stacktrace };
      TraceSource.TraceData(TraceEventType.Critical, 0, trace);
    }

    public void LogFatal(string processid, string message, string stacktrace, string entityname, int entityid, object objectToLog = null)
    {
      TraceModel trace = null;
      if (!String.IsNullOrEmpty(entityname))
      {
        trace = new EntityTraceModel() { DataSource = DataSource, ProcessID = processid, Message = message, EntityName = entityname, EntityID = entityid, ObjectToLog = objectToLog };
      }
      else
      {
        trace = new TraceModel() { ProcessID = processid, Message = message };
      }

      TraceSource.TraceData(TraceEventType.Critical, 0, trace);
    }

    public void CreateOrSetContextProperty(string key, object value)
    {
      if (_contextProperties.ContainsKey(key))
      {
        _contextProperties[key] = value;
      }
      else
      {
        _contextProperties.Add(key, value);
      }
    }

    public void DeleteContextProperty(string key)
    {
      if (_contextProperties.ContainsKey(key))
        _contextProperties.Remove(key);
    }

    public void ClearContextData()
    {
      _contextProperties.Clear();
    }

    private int RegisterProcess(string name)
    {
      return -1;
      //TODO: register process in database and set ID
      //throw new NotImplementedException(name);

    }

    public void InitTrace(string name)
    {
      if (TraceSource == null)
      {
        TraceSource = new TraceSource(name, SourceLevels.All);

        // When a trace source is initialized, but is not configured in the application configuration only the System.Diagnostics.DefaultTraceListener will exist             
        if (TraceSource.Listeners.Count == 1 && TraceSource.Listeners[0] is DefaultTraceListener)
        {
          TraceSource.Listeners.Clear();
          TraceSource.Listeners.AddRange(Trace.Listeners);
        }

        //TODO: write or steal databasetracelistener, add to config
        foreach (var traceListener in TraceSource.Listeners)
        {
          if (traceListener is TraceListenerBase)
          {
            var listener = traceListener as TraceListenerBase;
            var processID = RegisterProcess(name);
            listener.SetProcessID(processID);
          }
        }
      }
    }
  }
}
