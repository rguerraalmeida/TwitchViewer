using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.Models
{
    public class StreamList
    {
        [JsonProperty("_total")]
        public int Total { get; set; }

        [JsonProperty("streams")]
        public List<Stream> Streams { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }
}




