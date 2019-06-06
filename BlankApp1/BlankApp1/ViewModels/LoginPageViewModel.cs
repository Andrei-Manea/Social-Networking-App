using BlankApp1.Views;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinFormsAcademy.Utils;

namespace BlankApp1.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public ICommand LoginClicked { get; set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        private bool _rememberMe;
        public bool RememberMe
        {
            get { return _rememberMe; }
            set { SetProperty(ref _rememberMe, value); }
        }

        public LoginPageViewModel(INavigationService navigationMethod, IPageDialogService dialogService, IEventAggregator eventAggregator) : base(navigationMethod, dialogService, eventAggregator)
        {
            if (Settings.RememberMe)
            {
                RememberMe = true;
                Username = Settings.Username;
                Password = Settings.Password;
            }
            LoginClicked = new Command(Button_Clicked);
        }

        private async void Button_Clicked()
        {
            bool loggedIn = false;

            Settings.RememberMe = _rememberMe;

            if (_rememberMe)
            {
                Settings.Username = _username;

                if (Settings.Password == Settings.DefaultPassword)
                {
                    Settings.Password = _password;
                }
            }
            else
            {
                Settings.Username = _username;
                if (Settings.Password == _password)
                {
                    Settings.Password = Settings.DefaultPassword;
                    loggedIn = true;
                }
                Settings.Username = string.Empty;
            }

            if (loggedIn || (Settings.Password == _password))
            {
                await NavigationMethod.NavigateAsync(nameof(Home));
            }
            else
            {
                await DialogService.DisplayAlertAsync("Alert", "Invalid Password", "OK");
            }
        }
    }
}
