using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwitchClientViewer.Api;
using TwitchClientViewer.Coverters;
using TwitchClientViewer.OAuth;
using TwitchClientViewer.Processes;
using TwitchClientViewer.ViewModels;

namespace TwitchClientViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string token = string.Empty;
        string username = string.Empty;

        OAuthFlow flow = new OAuthFlow();
        Twitch api = new Twitch();
        StreamViewModel StreamViewModel = new StreamViewModel();

        const string vlcPath = "vlc-2.2.4\\vlc.exe";
        const string quality = "source"; //"best";


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this.StreamViewModel;
            this.CheckTextboxUsername();
        }

        private void signin_Click(object sender, RoutedEventArgs e)
        {
            username = this.textBox.Text.Trim();
            var url = flow.GetAuthorizeUrl(username);
            BrowserWindow bw = new BrowserWindow(url);
            bw.Owner = this;
            if (bw.ShowDialog() == true)
            {
                OAuthData.token = bw.Token;
            }

            GetData();
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.CheckTextboxUsername();
        }

        private void CurrentStreams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StreamViewModel.RaisePropertyChangedOn(() => StreamViewModel.SelectedLiveStream);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            this.GetData();
        }

        private void CheckTextboxUsername()
        {
            if (this.textBox.Text.Length > 0 && !string.IsNullOrWhiteSpace(this.textBox.Text))
            {
                this.signin.IsEnabled = true;
            }
            else
            {
                this.signin.IsEnabled = false;
            }
        }

        private void GetData()
        {
            if (string.IsNullOrWhiteSpace(username)) { return; }

            var currentStreams = api.Streams(username);
            if (!currentStreams.HasError)
            {
                var streamsbuffer = new ObservableCollection<LiveStreamViewModel>();

                foreach (var stream in currentStreams.Data.Streams)
                {
                    var temp = new LiveStreamViewModel()
                    {
                        Logo = stream.Channel.Logo,
                        DisplayName = stream.Channel.DisplayName,
                        Preview = stream.Preview.Small,
                        LiveUrl = stream.Channel.Url,
                        GameName = stream.Game,
                        PreviewLarge = stream.Preview.Large,
                        GameLogo = stream.Preview.Medium,
                        PreviewTemplate = StreamPreviewTemplateUrl.Convert(stream.Preview.Template),
                        ViewerCount = stream.Viewers,
                    };

                    streamsbuffer.Add(temp);
                }
                StreamViewModel.LiveStreams = new ObservableCollection<LiveStreamViewModel>(streamsbuffer.OrderBy(o => o.ViewerCount));
                StreamViewModel.RaisePropertyChangedOn(() => StreamViewModel.LiveStreams);
            }

        }

        private void Play_click(object sender, RoutedEventArgs e)
        {
            this.Play();
        }

        private void Play()
        {
            //LivestreamerWrapper ls = new LivestreamerWrapper();
            //ls.Start(StreamViewModel.SelectedLiveStream.LiveUrl, Helpers.LivestreamerOptions.Http);

            //VlcWindow vlcwindow = new VlcWindow();
            //vlcwindow.ShowDialog();

            this.PlayVLC();
        }

        private void createTempBat(string name, string quality, string player)
        {
            if (File.Exists("ExternalLibs\\temp.bat"))
            {
                File.Delete("ExternalLibs\\temp.bat");
            }

            StreamWriter streamWriter = new StreamWriter("ExternalLibs\\temp.bat", false);
            try
            {
                streamWriter.WriteLine("@echo");
                string[] strArrays = new string[] { "ExternalLibs\\livestreamer-v1.12.2\\livestreamer.exe -p \"ExternalLibs\\", player, "\" \"http://www.twitch.tv/", name, "\" ", quality };
                streamWriter.WriteLine(string.Concat(strArrays));
                streamWriter.WriteLine("@echo off");
            }
            finally
            {
                if (streamWriter != null)
                {
                    ((IDisposable)streamWriter).Dispose();
                }
            }
        }


        private void PlayVLC()
        {
            this.createTempBat(StreamViewModel.SelectedLiveStream.DisplayName, quality, vlcPath);
            Process process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                FileName = "ExternalLibs\\temp.bat",
                UseShellExecute = false
            };

            process.StartInfo = processStartInfo;
            process.Start();
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler((object proc, EventArgs processEa) => AutoClosingMessageBox.Show("Stream Ended or Streamer is offline", "End", 2000));
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            if (File.Exists("Data\\temp.bat"))
            {
                File.Delete("Data\\temp.bat");
            }

            if (File.Exists("Data\\startLivestreamer.bat"))
            {
                File.Delete("Data\\startLivestreamer.bat");
            }
        }

    }
}
