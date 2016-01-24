using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TwitchClientViewer
{
    /// <summary>
    /// Interaction logic for HosterWindow.xaml
    /// </summary>
    public partial class HosterWindow : Window
    {
        public HosterWindow(string channelName)
        {
            InitializeComponent();

            this.Player.Navigated += Player_Navigated;
            

            this.Title = channelName;
            this.Player.Navigate("http://player.twitch.tv/?channel=" + channelName);
        }

        private void Player_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            this.DisableJavascript();
        }

        private void DisableJavascript()
        {
            dynamic document = this.Player.Document;
            dynamic head = document.GetElementsByTagName("head")[0];
            dynamic scriptEl = document.CreateElement("script");
            scriptEl.text = @"function noError() {  return true; } window.onerror = noError;";
            head.AppendChild(scriptEl);
        }
    }
}
