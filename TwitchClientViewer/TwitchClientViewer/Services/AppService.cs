using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TwitchClientViewer.Models;
using TwitchClientViewer.OAuth;
using TwitchClientViewer.ViewModels;
using TwitchClientViewer.Views.Login;

namespace TwitchClientViewer.Services
{
    public sealed class AppService
    {
        #region Singleton
        
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static AppService() { }

        private static readonly AppService instance = new AppService();

        private AppService() { }

        public static AppService Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        string token = string.Empty;
        string username = string.Empty;
        OAuthFlow flow = new OAuthFlow();


        private AppUser _appUser;
        public AppUser AppUser
        {
            get
            {
                if (_appUser == null) _appUser = new AppUser();
                return _appUser;
            }
        }

        private List<object> _container = new List<object>();

        public void RegisterView<V,VM>(string namedInstance = null)
        {
            V view = Activator.CreateInstance<V>();
            VM viewmodel = Activator.CreateInstance <VM>();

            if ((view is Window || view is UserControl) && (viewmodel is BindableBase))
            {
                var v = view as ContentControl;
                var vm = viewmodel as BindableBase;

                v.DataContext = vm;
            }

            if (view != null && viewmodel != null)
            {
                this._container.Add(view);
                this._container.Add(viewmodel);
            }
        }

        public T TryGetType<T>(string namedInstance = null)
        {
            return (T)_container.Where(x => x is T).FirstOrDefault();
        }

        public void ShowLoginView()
        {
            var view = new LoginView();
            var viewmodel = new LoginViewModel();
            view.DataContext = viewmodel;

            view.ShowDialog();
        }

        public void DoLogin(string name)
        {
            username = username.Trim();
            var url = flow.GetAuthorizeUrl(username);
            BrowserWindow bw = new BrowserWindow(url);
            //bw.Owner = this;
            if (bw.ShowDialog() == true)
            {
                if (!string.IsNullOrWhiteSpace(bw.Token))
                OAuthData.token = bw.Token;
                this.token = bw.Token;
                this._appUser.IsAuthenticated = true;
                this._appUser.UserName = username;
            }
        }

        public void DoLogout()
        {
            throw new NotImplementedException("Logout not implemented");
            //Clean cookies and stuff?? ...
        }
    }
}