using System.Windows;
using TwitchClientViewer.Services;
using TwitchClientViewer.Views.Following;
using TwitchClientViewer.Views.MainView;

namespace TwitchClientViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppService.Instance.RegisterView<MainView, MainViewModel>();
            Application.Current.MainWindow = (Window)AppService.Instance.TryGetType<MainView>();
            Application.Current.MainWindow.Show();

            base.OnStartup(e);
        }
    }
}