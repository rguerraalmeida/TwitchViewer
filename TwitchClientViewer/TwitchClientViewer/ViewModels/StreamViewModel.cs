using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TwitchClientViewer.ViewModels
{
    public class StreamViewModel : BindableBase
    {
        public StreamViewModel()
        {
            this.SelectedLiveStream = new LiveStream();
            this.LiveStreams = new ObservableCollection<LiveStream>();
        }

        public LiveStream SelectedLiveStream { get; set; }
        public ObservableCollection<LiveStream> LiveStreams { get; set; }

        public CollectionView LiveStreamsCollection { get; private set; }

    }

    public class LiveStream : BindableBase
    {
        public LiveStream() { }
        public LiveStream(string name, string logo, string displayName, string preview, string liveUrl, string gameName, string previewLarge, string gameLogo, string previewTemplate)
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
        }

        public string Name { get; set; }
        public string Logo { get; set; }
        public string DisplayName { get; set; }
        public string Preview { get; set; }
        public string LiveUrl { get; set; }
        public string GameName { get; set; }
        public string PreviewLarge { get; set; }
        public string GameLogo { get; set; }
        public string PreviewTemplate { get; set;  }
    }
}
