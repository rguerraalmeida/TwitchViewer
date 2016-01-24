using Newtonsoft.Json;

namespace TwitchClientViewer.Models
{
    public class Channel
    {
        [JsonProperty(PropertyName = "broadcaster_language")]
        public string BroadcasterLanguage { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "game")]
        public string Game { get; set; }

        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; set; }

        [JsonProperty(PropertyName = "mature")]
        public string Mature { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "partner")]
        public bool Partner { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "_id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "delay")]
        public object Delay { get; set; }

        [JsonProperty(PropertyName = "followers")]
        public int FollowerCount { get; set; }

        [JsonProperty(PropertyName = "views")]
        public int Views { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }
    }

}
