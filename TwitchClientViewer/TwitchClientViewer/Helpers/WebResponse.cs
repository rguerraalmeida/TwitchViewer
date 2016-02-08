using System;
using System.Net;

namespace TwitchClientViewer
{
    public class WebResponse
    {
        public HttpStatusCode StatusCode;
        public string ResponseString = string.Empty;
        public Uri RedirectUri;

        public WebResponse(HttpStatusCode statusCode,  string responseString, Uri redirectUri)
        {
            this.StatusCode = statusCode;
            this.ResponseString = responseString;
            this.RedirectUri = redirectUri;
        }
    }

    public static class HttpWebResponseExtension
    {
        public static HttpWebResponse GetResponseWithoutThrowingException(this HttpWebRequest request)
        {
            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                var resp = we.Response as HttpWebResponse;
                if (resp == null)
                    throw;
                return resp;
            }
        }
    }
}
