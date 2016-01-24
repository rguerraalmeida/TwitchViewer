using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.Models
{
    public class FollowList
    {

        [JsonProperty(PropertyName = "_total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "follows")]
        public List<Follow> Follows { get; set; }

    }
}
