using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer
{
    public class HttpWebRequestWrapper
    {
        private static HttpWebRequest CreateRequest(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = false;
            return request;
        }

        private static WebResponse ReadResponse(HttpWebRequest request)
        {
            var response = (HttpWebResponse)request.GetResponseWithoutThrowingException();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var responseUri = response.ResponseUri;

            var webResponse = new WebResponse(response.StatusCode, responseString, responseUri);

            response.Close();
            return webResponse;
        }


        public static WebResponse ExecuteWebRequest(string uri)
        {
            HttpWebRequest request = CreateRequest(uri);

            WebResponse webResponse = ReadResponse(request);

            return webResponse;
        }

        public static WebResponse ExecuteWebRequest(string uri, string token)
        {
            HttpWebRequest request = CreateRequest(uri);

            request.Headers.Add("Authorization", "OAuth " + token);

            WebResponse webResponse = ReadResponse(request);

            return webResponse;
        }

        public static WebResponse ExecuteWebRequest(string uri, string token, string clientId)
        {
            HttpWebRequest request = CreateRequest(uri);

            request.Headers.Add("Authorization", "OAuth " + token);

            request.Headers.Add("Client-ID", clientId);

            WebResponse webResponse = ReadResponse(request);

            return webResponse;
        }


        public static T ParseResponse<T>(string json)
        {
            var deserializedObject = JsonConvert.DeserializeObject<T>(json);
            return deserializedObject;
        }
    }
}
