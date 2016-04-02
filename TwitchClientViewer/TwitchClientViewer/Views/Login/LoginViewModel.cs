using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwitchClientViewer.Models;
using TwitchClientViewer.Services;
using TwitchClientViewer.ViewModels;

namespace TwitchClientViewer.Views.Login
{
    public class LoginViewModel : BindableBase
    {
        public string AppUsername
        {
            get
            {
                return AppService.Instance.AppUser.UserName;
            }
        }
        public bool UserIsAuthenticated
        {
            get
            {
                return AppService.Instance.AppUser.IsAuthenticated;
            }
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; base.RaisePropertyChanged(() => this.UserName); }
        }

        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { _isAuthenticated = value; base.RaisePropertyChanged(() => this.UserName); }
        }


        public LoginViewModel()
        {
            this.LoginCommand = new DelegateCommand<object>(this.OnLoginCommand);
            this.LogoutCommand = new DelegateCommand<object>(this.OnLogoutCommand);
        }

        private void OnLoginCommand(object arg)
        {
            AppService.Instance.DoLogin(_userName);
        }

        private void OnLogoutCommand(object arg)
        {
            AppService.Instance.DoLogout();
        }
    }
}
