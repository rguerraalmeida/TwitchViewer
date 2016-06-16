using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.Coverters
{
    public class StreamPreviewTemplateUrl
    {
        public static string Convert(object value)
        {
            string template = value as string;

            if (!string.IsNullOrWhiteSpace(template))
            {
                template = template
                    .Replace("{width}", "1920")
                    .Replace("{height}", "1080");
            }

            return template;
        }
    }
}