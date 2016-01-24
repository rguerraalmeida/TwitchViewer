using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.Models
{
    public class Follow
    {
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedDate { get; set; }

        [JsonProperty(PropertyName = "notifications")]
        public bool Notifications { get; set; }

        [JsonProperty(PropertyName = "channel")]
        public Channel Channel { get; set; }
    }
}
