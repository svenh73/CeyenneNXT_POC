using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using CeyenneNxt.Core.Configuration;

namespace CeyenneNxt.Core.Factories
{
  public class NamespaceHttpControllerSelector : DefaultHttpControllerSelector
  {
    private const string ControllerKey = "controller";
    private readonly HttpConfiguration _configuration;
    private readonly Lazy<HashSet<NamespacedHttpControllerMetadata>> _duplicateControllerTypes;

    public NamespaceHttpControllerSelector(HttpConfiguration configuration)
        : base(configuration)
    {
      _configuration = configuration;
      _duplicateControllerTypes = new Lazy<HashSet<NamespacedHttpControllerMetadata>>(InitializeNamespacedHttpControllerMetadata);
    }

    public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
    {
      var routeData = request.GetRouteData();

      object controllerName;
      routeData.Values.TryGetValue(ControllerKey, out controllerName);
      var controllerNameAsString = controllerName as string;
      if (controllerNameAsString == null)
        return base.SelectController(request);

      var map = base.GetControllerMapping();
      if (map.ContainsKey(controllerNameAsString))
        return base.SelectController(request);

      //see if this is in our cache
      var found = _duplicateControllerTypes.Value.FirstOrDefault(x => string.Equals(x.ControllerName, controllerNameAsString, StringComparison.InvariantCultureIgnoreCase) && x.ControllerNamespace.StartsWith(CNXTEnvironments.Current.CustomerName));
      if (found != null)
      {
        return found.Descriptor;
      }
      found = _duplicateControllerTypes.Value.FirstOrDefault(x => string.Equals(x.ControllerName, controllerNameAsString, StringComparison.InvariantCultureIgnoreCase) && x.ControllerNamespace.StartsWith("CeyenneNxt"));
      if (found != null)
      {
        return found.Descriptor;
      }
      return base.SelectController(request);
    }

    private HashSet<NamespacedHttpControllerMetadata> InitializeNamespacedHttpControllerMetadata()
    {

      var assembliesResolver = _configuration.Services.GetAssembliesResolver();
      var controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();
      var controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);


      var groupedByName = controllerTypes.GroupBy(
          t => t.Name.Substring(0, t.Name.Length - ControllerSuffix.Length),
          StringComparer.OrdinalIgnoreCase).Where(x => x.Count() > 1);

      var duplicateControllers = groupedByName.ToDictionary(
          g => g.Key,
          g => g.ToLookup(t => t.Namespace ?? String.Empty, StringComparer.OrdinalIgnoreCase),
          StringComparer.OrdinalIgnoreCase);

      var result = new HashSet<NamespacedHttpControllerMetadata>();

      foreach (var controllerTypeGroup in duplicateControllers)
      {
        foreach (var controllerType in controllerTypeGroup.Value.SelectMany(controllerTypesGrouping => controllerTypesGrouping))
        {
          result.Add(new NamespacedHttpControllerMetadata(controllerTypeGroup.Key, controllerType.Namespace,
              new HttpControllerDescriptor(_configuration, controllerTypeGroup.Key, controllerType)));
        }
      }

      return result;
    }

    private class NamespacedHttpControllerMetadata
    {
      private readonly string _controllerName;
      private readonly string _controllerNamespace;
      private readonly HttpControllerDescriptor _descriptor;

      public NamespacedHttpControllerMetadata(string controllerName, string controllerNamespace, HttpControllerDescriptor descriptor)
      {
        _controllerName = controllerName;
        _controllerNamespace = controllerNamespace;
        _descriptor = descriptor;
      }

      public string ControllerName
      {
        get { return _controllerName; }
      }

      public string ControllerNamespace
      {
        get { return _controllerNamespace; }
      }

      public HttpControllerDescriptor Descriptor
      {
        get { return _descriptor; }
      }
    }
  }

}