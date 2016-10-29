using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Helpers;
using Newtonsoft.Json;

namespace CeyenneNxt.Orders.Module.CommunicationModule
{
  public class OrderCommunicationModule
  {
    //public APITokenManager ApiTokenManager { get; set; } = new APITokenManager();
    public OrderCommunicationModule()
    {
    }

    private void PostRequest()
    {
      //var client = new HttpClient { BaseAddress = _baseUrl };
      //client.SetBearerToken(_tokenManager.GetAccessToken());
      //var json = JsonConvert.SerializeObject(communicationModel);

      //var response = client.PostAsync("orderLines/createStatusHistory", new StringContent(json, Encoding.UTF8, "application/json")).Result;

      //System.Diagnostics.Debug.WriteLine(response.Content);

      //var content = Task.Run(async () => await response.Content.ReadAsStringAsync()).Result;
      //int orderStatusHistoryID;
      //int.TryParse(content, out orderStatusHistoryID);

      //return orderStatusHistoryID;
    }
  }
}
