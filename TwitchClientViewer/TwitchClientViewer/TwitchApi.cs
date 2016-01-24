using System.Net;
using TwitchClientViewer.Models;

namespace TwitchClientViewer
{
    public class TwitchApi
    {
        public TwitchData<FollowList> Follows(string username)
        {
            var requestUrl = "https://api.twitch.tv/kraken/users/"+ username + "/follows/channels";
            var response = HttpWebRequestWrapper.ExecuteWebRequest(requestUrl, OAuthData.token);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var responseError = HttpWebRequestWrapper.ParseResponse<ErrorMessage>(response.ResponseString);
                return new TwitchData<FollowList>(responseError);
            }

            var responseData = HttpWebRequestWrapper.ParseResponse<FollowList>(response.ResponseString);
            return new TwitchData<FollowList>(responseData);
        }

        public TwitchData<StreamList> Streams(string username)
        {
            var requestUrl = "https://api.twitch.tv/kraken/streams/followed";
            var response = HttpWebRequestWrapper.ExecuteWebRequest(requestUrl, OAuthData.token);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var responseError = HttpWebRequestWrapper.ParseResponse<ErrorMessage>(response.ResponseString);
                return new TwitchData<StreamList>(responseError);
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

            return new TwitchData<StreamList>(responseData);
        }

        public TwitchData<StreamList> NextStreams(string url)
        {
            var requestUrl = url;
            var response = HttpWebRequestWrapper.ExecuteWebRequest(requestUrl, OAuthData.token);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var responseError = HttpWebRequestWrapper.ParseResponse<ErrorMessage>(response.ResponseString);
                return new TwitchData<StreamList>(responseError);
            }

            var responseData = HttpWebRequestWrapper.ParseResponse<StreamList>(response.ResponseString);
            return new TwitchData<StreamList>(responseData);
        }
    }
}
