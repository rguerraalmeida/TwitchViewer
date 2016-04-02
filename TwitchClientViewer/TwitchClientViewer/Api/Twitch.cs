using System.Net;
using TwitchClientViewer.Models;
using TwitchClientViewer.Api.Models;
using TwitchClientViewer.OAuth;

namespace TwitchClientViewer.Api
{
    public class Twitch
    {
        public RequestData<FollowList> Follows(string username)
        {
            var requestUrl = "https://api.twitch.tv/kraken/users/"+ username + "/follows/channels";
            var response = HttpWebRequestWrapper.ExecuteWebRequest(requestUrl, OAuthData.token);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var responseError = HttpWebRequestWrapper.ParseResponse<RequestErrorMessage>(response.ResponseString);
                return new RequestData<FollowList>(responseError);
            }

            var responseData = HttpWebRequestWrapper.ParseResponse<FollowList>(response.ResponseString);
            return new RequestData<FollowList>(responseData);
        }

        public RequestData<StreamList> Streams(string username)
        {
            var requestUrl = "https://api.twitch.tv/kraken/streams/followed";
            var response = HttpWebRequestWrapper.ExecuteWebRequest(requestUrl, OAuthData.token);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var responseError = HttpWebRequestWrapper.ParseResponse<RequestErrorMessage>(response.ResponseString);
                return new RequestData<StreamList>(responseError);
            }

            var responseData = HttpWebRequestWrapper.ParseResponse<StreamList>(response.ResponseString);
            if (responseData != null && responseData.Total > 0)
            {
                var total = responseData.Total;
                var current = responseData.Streams.Count;
                var nexturl = responseData.Links.Next;

                do
                {
                    var nextLinks = NextStreams(nexturl);
                    responseData.Streams.AddRange(nextLinks.Data.Streams);

                    nexturl = nextLinks.Data.Links.Next;
                    current += nextLinks.Data.Streams.Count;

                } while (total > current);
            }

            return new RequestData<StreamList>(responseData);
        }

        public RequestData<StreamList> NextStreams(string url)
        {
            var requestUrl = url;
            var response = HttpWebRequestWrapper.ExecuteWebRequest(requestUrl, OAuthData.token);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var responseError = HttpWebRequestWrapper.ParseResponse<RequestErrorMessage>(response.ResponseString);
                return new RequestData<StreamList>(responseError);
            }

            var responseData = HttpWebRequestWrapper.ParseResponse<StreamList>(response.ResponseString);
            return new RequestData<StreamList>(responseData);
        }
    }
}
