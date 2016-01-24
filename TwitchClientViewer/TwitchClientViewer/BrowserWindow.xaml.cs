using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
    /// Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {

        public string Token;

        public BrowserWindow(string url)
        {
            InitializeComponent();
            this.button.Visibility = Visibility.Hidden;
            this.Loaded += (object sender, RoutedEventArgs e) =>
            {
                webBrowser.Navigate(url);
            };
        }

        private void SetAddress(string text)
        {
            if (textBlock.Text != text)
            {
                textBlock.Text = text;
            }
        }

        private void SetControlsVisibility()
        {
            this.textBlock.Visibility = Visibility.Hidden;
            this.webBrowser.Visibility = Visibility.Hidden;
            this.button.Visibility = Visibility.Visible;
        }

        private void webBrowser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            this.SetAddress(e.Uri.AbsoluteUri.ToString());
        }

        private void webBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            //SetSilent(webBrowser, true); // make it silent

            this.SetAddress(e.Uri.AbsoluteUri.ToString());
            //If the URL has an access_token, grab it and walk away...
            var url = e.Uri.Fragment;
            if (url.Contains("access_token") && url.Contains("#"))
            {
                url = (new System.Text.RegularExpressions.Regex("#")).Replace(url, "?", 1);
                this.Token = System.Web.HttpUtility.ParseQueryString(url).Get("access_token");
                this.SetControlsVisibility();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public static void SetSilent(WebBrowser browser, bool silent)
        {
            if (browser == null)
                throw new ArgumentNullException("browser");

            // get an IWebBrowser2 from the document
            IOleServiceProvider sp = browser.Document as IOleServiceProvider;
            if (sp != null)
            {
                Guid IID_IWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
                Guid IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");

                object webBrowser;
                sp.QueryService(ref IID_IWebBrowserApp, ref IID_IWebBrowser2, out webBrowser);
                if (webBrowser != null)
                {
                    webBrowser.GetType().InvokeMember("Silent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.PutDispProperty, null, webBrowser, new object[] { silent });
                }
            }
        }

        [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IOleServiceProvider
        {
            [PreserveSig]
            int QueryService([In] ref Guid guidService, [In] ref Guid riid, [MarshalAs(UnmanagedType.IDispatch)] out object ppvObject);
        }
    }
}
