using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.Models
{
    public class Links
    {
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }
}
