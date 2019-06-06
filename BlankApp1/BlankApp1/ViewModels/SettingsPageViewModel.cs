using BlankApp1.Utils;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlankApp1.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
	{
        public ICommand ChangeColor { get; set; }

        private string _backgroundColor;
        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set { SetProperty(ref _backgroundColor, value); }
        }

        
        public SettingsPageViewModel(INavigationService navigationMethod, IPageDialogService dialogService, IEventAggregator eventAggregator) : base(navigationMethod, dialogService, eventAggregator)
        {
            ChangeColor = new Command(ChangeColor_Clicked);
        }

        private async void ChangeColor_Clicked()
        {
            await NavigationMethod.GoBackAsync();
            if (BackgroundColor != null)
                EventAggregator.GetEvent<SendMessageEvent>().Publish(BackgroundColor);
        }
    }
}
