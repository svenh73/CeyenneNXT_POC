using System;
using IdentityModel.Client;

namespace CeyenneNxt.Core.Helpers
{
  public class APITokenManager
  {
    private readonly string _url;
    private readonly string _clientID;
    private readonly string _secret;

    public APITokenManager(string url, string clientid, string secret)
    {
      _url = url;
      _clientID = clientid;
      _secret = secret;
    }


    private DateTime _expirationTime;

    private string _accessToken;

    public string GetAccessToken()
    {
      if (string.IsNullOrEmpty(_accessToken) || _expirationTime <= DateTime.Now)
      {
        Refresh();
      }

      return _accessToken;
    }

    private void Refresh()
    {
      var tokenClient = new TokenClient(
       _url,
       _clientID,
       _secret
     );

      var tokenResult = tokenClient.RequestClientCredentialsAsync("api").Result;
      _accessToken = tokenResult.AccessToken;
      _expirationTime = DateTime.Now.Add(TimeSpan.FromSeconds(tokenResult.ExpiresIn));
    }
  }
}
