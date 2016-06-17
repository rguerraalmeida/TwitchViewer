using System;
using System.IO;
using System.Windows;
using TwitchClientViewer.Services;
using TwitchClientViewer.Views.Following;
using TwitchClientViewer.Views.MainView;
using System.IO.Compression;

namespace TwitchClientViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string externalLibsPath = @"ExternalLibs";
        
        private const string livestreamerPath = @"ExternalLibs\livestreamer-v1.12.2";
        private const string livestreamerZipPath = @"ExternalLibs\livestreamer-v1.12.2-win32.zip";
        private const string livestreamerExePath = @"ExternalLibs\livestreamer-v1.12.2\livestreamer.exe";

        private const string vlcPath = @"ExternalLibs\vlc-2.2.4";
        private const string vlcZipPath = @"ExternalLibs\vlc-2.2.4-win32.zip";
        private const string vlcExePath = @"ExternalLibs\vlc-2.2.4\vlc.exe";

        protected override void OnStartup(StartupEventArgs e)
        {
            //AppService.Instance.RegisterView<MainView, MainViewModel>();
            //Application.Current.MainWindow = (Window)AppService.Instance.TryGetType<MainView>();
            //Application.Current.MainWindow.Show();

            this.UnzipExternalLibraries();

            base.OnStartup(e);
        }

        protected void UnzipExternalLibraries()
        {
            if (!Directory.Exists(externalLibsPath))
            {
                throw new DirectoryNotFoundException(@"Unable to find the required folder " + externalLibsPath);
            }


            if (!File.Exists(livestreamerZipPath))
            {
                throw new FileNotFoundException(@"Unable to find the required file " + livestreamerZipPath);
            }

            if (!File.Exists(vlcZipPath))
            {
                throw new FileNotFoundException(@"Unable to find the required file " + livestreamerZipPath);
            }

            try
            {
                if (Directory.Exists(livestreamerPath))
                {
                    Directory.Delete(livestreamerPath, true);
                }

                if (Directory.Exists(vlcPath))
                {
                    Directory.Delete(vlcPath, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            try
            {
                ZipFile.ExtractToDirectory(livestreamerZipPath, externalLibsPath);
                ZipFile.ExtractToDirectory(vlcZipPath, externalLibsPath);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}