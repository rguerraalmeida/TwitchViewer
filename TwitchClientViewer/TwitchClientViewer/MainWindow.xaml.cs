using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
using TwitchClientViewer.Coverters;
using TwitchClientViewer.ViewModels;

namespace TwitchClientViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        const string vlcPath = "vlc-2.1.5\\vlc.exe";
        const string quality = "source"; //"best";

        string token = string.Empty;
        string username = string.Empty;

        OAuthFlow flow = new OAuthFlow();
        TwitchApi api = new TwitchApi();
        StreamViewModel StreamViewModel = new StreamViewModel();

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
                var streamsbuffer = new List<LiveStream>();

                foreach (var stream in currentStreams.Data.Streams)
                {
                    var temp = new LiveStream()
                    {
                        Logo = stream.Channel.Logo,
                        DisplayName = stream.Channel.DisplayName,
                        Preview = stream.Preview.Small,
                        LiveUrl = stream.Channel.Url,
                        GameName = stream.Game,
                        PreviewLarge = stream.Preview.Large,
                        GameLogo = stream.Preview.Medium,
                        Template = StreamPreviewTemplateUrl.Convert(stream.Preview.Template),
                };

                    streamsbuffer.Add(temp);
                }
                StreamViewModel.LiveStreams = streamsbuffer;
                StreamViewModel.RaisePropertyChangedOn(() => StreamViewModel.LiveStreams);
            }

        }

        private void Play_click(object sender, RoutedEventArgs e)
        {
            //var player = new HosterWindow(StreamViewModel.SelectedLiveStream.DisplayName);
            //player.Show();

            //var VlcPlayer = new VlcWindow();
            //VlcPlayer.Show();

            this.PlayVLC();

        }

        private void createTempBat(string name, string quality, string player)
        {
            if (File.Exists("Data\\temp.bat"))
            {
                File.Delete("Data\\temp.bat");
            }

            StreamWriter streamWriter = new StreamWriter("Data\\temp.bat",false);
            try
            {
                streamWriter.WriteLine("@echo");
                string[] strArrays = new string[] { "Data\\livestreamer\\livestreamer.exe -p \"Data\\", player, "\" \"http://www.twitch.tv/", name, "\" ", quality };
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
                FileName = "Data\\temp.bat",
                UseShellExecute = false
            };
            process.StartInfo = processStartInfo;
            process.Start();
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler((object proc, EventArgs processEa) => AutoClosingMessageBox.Show("Stream Ended or Streamer is offline", "End", 2000));
        }


        private void StartLivestreamer()
        {
            if (File.Exists("Data\\startLivestreamer.bat"))
            {
                File.Delete("Data\\startLivestreamer.bat");
            }

            StreamWriter streamWriter = new StreamWriter("Data\\startLivestreamer.bat", false);
            try
            {
                streamWriter.WriteLine("@echo");
                string[] strArrays = new string[] { "Data\\livestreamer\\livestreamer.exe -p \"Data\\", player, "\" \"http://www.twitch.tv/", name, "\" ", quality };
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


        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            if (File.Exists("Data\\temp.bat"))
            {
                File.Delete("Data\\temp.bat");
            }
        }
              
    }
}
