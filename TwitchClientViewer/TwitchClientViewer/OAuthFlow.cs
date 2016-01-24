using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Owin;
using Owin.Security.Providers.Twitch;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using TwitchClientViewer.Models;

namespace TwitchClientViewer
{
    public class OAuthFlow
    {
        string userAuthorizeUrl = "https://api.twitch.tv/kraken/oauth2/authorize?";
        //string userAuthorizeParams = "response_type=token&client_id=[client-id]&redirect_uri=[redirect-url]&scope=[space-separated-scopes]&state=[generated-token]&force_verify=[force_verify]";
        string userAuthorizeParams = "response_type=token&client_id=[client-id]&redirect_uri=[redirect-url]&scope=[space-separated-scopes]";

        public string GetAuthorizeUrl(string username)
        {
            var validUser = this.ValidateUsername(username);

            if (validUser.HasError)
            {
                throw new ArgumentException("The specified username doesn't exists");
            }

            var redirectUri = this.GetRedirectUrl();
            var scope = this.GetScopes();
            var state = this.GetState();

            var queryString = userAuthorizeParams
                .Replace("[client-id]", OAuthData.clientId)
                .Replace("[redirect-url]", HttpUtility.UrlEncode(redirectUri))
                .Replace("[space-separated-scopes]", scope);
                //.Replace("[generated-token]", state)
                //.Replace("[force_verify]", true.ToString().ToLower());

            return userAuthorizeUrl + queryString;
        }

        private TwitchData<User> ValidateUsername(string username)
        {
            var response = HttpWebRequestWrapper.ExecuteWebRequest("https://api.twitch.tv/kraken/users/" + username);

            while (response.StatusCode == HttpStatusCode.Redirect || response.StatusCode == HttpStatusCode.RedirectKeepVerb)
            {
                response = HttpWebRequestWrapper.ExecuteWebRequest(response.RedirectUri.AbsolutePath);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var responseError = HttpWebRequestWrapper.ParseResponse<User>(response.ResponseString);
                return new TwitchData<User>(responseError);
            }

            var responseData = HttpWebRequestWrapper.ParseResponse<User>(response.ResponseString);
            return new TwitchData<User>(responseData);
        }

        private string GetUserClientId(string username)
        {
            return string.Empty;
        }

        private string GetRedirectUrl()
        {
            return "http://localhost";
        }

        private string GetScopes()
        {
            return string.Join("+", Enum.GetNames(typeof(OAuthScopes)));
            //return Enum.GetNames(typeof(OAuthScopes)).Aggregate((current, next) => current + "+" + next);
        }

        private string GetState()
        {
            return this.GenerateToken();
        }

        private string GenerateToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        private DateTime GetTokenIssuedDate(string token)
        {
            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            return when;
        }
    }
}