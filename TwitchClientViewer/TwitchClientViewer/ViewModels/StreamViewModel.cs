using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TwitchClientViewer.ViewModels
{
    public class StreamViewModel : BindableBase
    {
        public StreamViewModel()
        {
            this.SelectedLiveStream = new LiveStream();
            this.LiveStreams = new List<LiveStream>();
        }

        public LiveStream SelectedLiveStream { get; set; }
        public List<LiveStream> LiveStreams { get; set; }
    }

    public class LiveStream : BindableBase
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string DisplayName { get; set; }
        public string Preview { get; set; }
        public string LiveUrl { get; set; }
        public string GameName { get; set; }
        public string PreviewLarge { get; set; }
        public string GameLogo { get; set; }
        public bool TemplateLoaded { get; set; }

        private string template;
        public string Template { get { return template; } set { template = value; RaisePropertyChangedOn(() => this.Template); } }

        public BitmapSource templateSource;
        public BitmapSource TemplateSource { get { return templateSource; } } 


        public LiveStream() { }
        public LiveStream(string name, string logo, string displayName, string preview, string liveUrl, string gameName, string previewLarge, string gameLogo, string template)
        {
            this.Name = name;
            this.Logo = logo;
            this.DisplayName = displayName;
            this.Preview = preview;
            this.LiveUrl = liveUrl;
            this.GameName = gameName;
            this.PreviewLarge = previewLarge;
            this.GameLogo = gameLogo;
            this.Template = template;
        }

        private BitmapSource FetchImage(string URLlink)
        {
            JpegBitmapDecoder decoder = null;
            BitmapSource bitmapSource = null;

            try
            {
                decoder = new JpegBitmapDecoder(new Uri(URLlink, UriKind.Absolute), BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                bitmapSource = decoder.Frames[0];
                bitmapSource.Freeze();
            }
            catch { }

            return bitmapSource;
        }

        private void GetBitmapImage(string path)
        {
            templateSource = FetchImage(path);
            if (templateSource != null)
            {
                TemplateLoaded = true;
                RaisePropertyChangedOn(() => this.TemplateSource);
                RaisePropertyChangedOn(() => this.TemplateLoaded);
            }
        }
    }
}
