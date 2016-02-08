using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchClientViewer.Helpers;

namespace TwitchClientViewer.Helpers
{
    public enum LivestreamerOptions
    {
        [StringValue("--player")]
        Player,
        [StringValue("--output")]
        File,
        [StringValue("--player-external-http")]
        Http,
    }
}
