using System;

namespace CeyenneNxt.Core.Exceptions
{
  public class EnvironmentException : Exception
  {
    public EnvironmentException(string currentMachine,string requiredMachineName)
    {
      CurrentMachineName = currentMachine;
      RequiredMachineName = requiredMachineName;
    }

    public string CurrentMachineName { get; private set; }

    public string RequiredMachineName { get; private set; }

    public override string Message
    {
      get
      {
        return
          String.Format(
            @"The environment set in the config file doesn't correspond with the machine '{0}' it currently running on. This configuration may only run on '{1}'",
            CurrentMachineName, RequiredMachineName);
      }
    }
  }
}
