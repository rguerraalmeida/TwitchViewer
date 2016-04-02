using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwitchClientViewer.Services;
using TwitchClientViewer.ViewModels;

namespace TwitchClientViewer.Views.MainView
{
    public class MainViewModel : BindableBase
    {
        public string UserName {
            get
            {
                return AppService.Instance.AppUser.UserName;
            }
        }

        public ICommand ShowFolowingChannelsView { get; private set; }
        public ICommand LoginCommand { get; private set; }

        public MainViewModel()
        {
            this.ShowFolowingChannelsView = new DelegateCommand<object>(this.OnShowFolowingChannelsView, this.CanShowFolowingChannelsView);
            this.LoginCommand = new DelegateCommand<object>(this.OnLoginCommand, this.CanExecuteLoginCommand);
        }

        private bool CanShowFolowingChannelsView(object arg) { return true; }
        private void OnShowFolowingChannelsView(object arg)
        {



        }

        private bool CanExecuteLoginCommand(object arg) { return true; }
        private void OnLoginCommand(object arg)
        {
            AppService.Instance.ShowLoginView();
        }
    }
}