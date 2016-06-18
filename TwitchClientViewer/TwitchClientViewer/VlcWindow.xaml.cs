using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.ComponentModel;
using System.Linq;
using Meta.Vlc.Wpf;

namespace TwitchClientViewer
{
    /// <summary>
    /// Interaction logic for VlcWindow.xaml
    /// </summary>
    public partial class VlcWindow : Window
    {
        public VlcWindow()
        {
            InitializeComponent();

            Load();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Player.Stop();

            Player.Dispose();

            ApiManager.ReleaseAll();

            base.OnClosing(e);
        }

        private void Load()
        {
            Uri uri;
            if (!Uri.TryCreate("http://127.0.0.1:62980", UriKind.Absolute, out uri)) return;

            Player.Stop();

            Player.LoadMedia(uri);
            
            //if you pass a string instead of a Uri, LoadMedia will see if it is an absolute Uri, else will treat it as a file path
            Player.Play();
        }

        private string GetFilesPath()
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
                return null;

            return new DirectoryInfo(Path.Combine(currentDirectory, @"Files\")).FullName;
        }

    }
}
