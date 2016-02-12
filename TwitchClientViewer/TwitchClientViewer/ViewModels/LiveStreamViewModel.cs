using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.ViewModels
{
    public class LiveStreamViewModel : BindableBase
    {
        public LiveStreamViewModel() { }

        public LiveStreamViewModel(
            string name, 
            string logo, 
            string displayName, 
            string preview, 
            string liveUrl, 
            string gameName, 
            string previewLarge, 
            string gameLogo, 
            string previewTemplate, 
            int viewers)
        {
            this.Name = name;
            this.Logo = logo;
            this.DisplayName = displayName;
            this.Preview = preview;
            this.LiveUrl = liveUrl;
            this.GameName = gameName;
            this.PreviewLarge = previewLarge;
            this.GameLogo = gameLogo;
            this.PreviewTemplate = previewTemplate;
            this.ViewerCount = viewers;
        }

        public string Name { get; set; }
        public string Logo { get; set; }
        public string DisplayName { get; set; }
        public string Preview { get; set; }
        public string LiveUrl { get; set; }
        public string GameName { get; set; }
        public string PreviewLarge { get; set; }
        public string GameLogo { get; set; }
        public string PreviewTemplate { get; set; }
        public int ViewerCount { get; set; }
    }
}
