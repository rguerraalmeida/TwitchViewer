using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.Models
{
    public class Stream
    {
        [JsonProperty("_id")]
        public object Id { get; set; }

        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("viewers")]
        public int Viewers { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("video_height")]
        public int VideoHeight { get; set; }

        [JsonProperty("average_fps")]
        public double AverageFps { get; set; }

        [JsonProperty("delay")]
        public int Delay { get; set; }

        [JsonProperty("is_playlist")]
        public bool IsPlaylist { get; set; }

        [JsonProperty("preview")]
        public Preview Preview { get; set; }

        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}
