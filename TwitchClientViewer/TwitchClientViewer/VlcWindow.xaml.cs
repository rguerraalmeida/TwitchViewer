using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.ComponentModel;
using System.Linq;
using xZune.Vlc.Wpf;

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
            Player.BeginStop(()=> { Player.Dispose(); });
            //base.OnClosing(e);
        }

        private void Load()
        {
            //String pathString = Path.Combine(GetFilesPath(), "Untitled.mp3"); // GetFilesPath().GetFiles().Where(f => f.Name.StartsWith("Untitled")).FirstOrDefault().pa;
            //string localFile = @"C:\Livestreamer\stream.dat";

            /*
            Uri uri;
            if (!Uri.TryCreate(pathString, UriKind.Absolute, out uri)) return;
            */

            Uri uri;
            if (!Uri.TryCreate("http://127.0.0.1:62980", UriKind.Absolute, out uri)) return;

            Player.EndBehavior = EndBehavior.Default;
            Player.BeginStop(() =>
            {
                Player.LoadMedia(uri); //if you pass a string instead of a Uri, LoadMedia will see if it is an absolute Uri, else will treat it as a file path
                Player.Play();
            });
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
