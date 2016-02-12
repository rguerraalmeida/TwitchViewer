using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.Api.Models
{
    //{"display_name":"dazzzY0","_id":55854217,"name":"dazzzy0","type":"user",
    //"bio":"Geek, Gaming Enthusiast, Internet Addict, Random Overclocker.",
    //"created_at":"2014-01-31T01:24:52Z","updated_at":"2016-01-16T14:17:05Z",
    //"logo":"https://static-cdn.jtvnw.net/jtv_user_pictures/dazzzy0-profile_image-176de621005cd79f-300x300.png",
    //"_links":{"self":"https://api.twitch.tv/kraken/users/dazzzy0"}}
    public class User
    {
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName = string.Empty;

        [JsonProperty(PropertyName = "_id")]
        public string Id = string.Empty;

        [JsonProperty(PropertyName = "name")]
        public string Name = string.Empty;

        [JsonProperty(PropertyName = "type")]
        public string Type = string.Empty;

        [JsonProperty(PropertyName = "bio")]
        public string Bio = string.Empty;

        [JsonProperty(PropertyName = "logo")]
        public string Logo = string.Empty;

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedDate;

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedDate;

        public FollowList FollowList;
    }
}
