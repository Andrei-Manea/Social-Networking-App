using BlankApp1.Utils;
using BlankApp1.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlankApp1.ViewModels
{
	public class PostViewModel : ViewModelBase
	{
        public ICommand SendClicked { get; set; }

        private string _postContent;
        public string PostContent
        {
            get { return _postContent; }
            set { SetProperty(ref _postContent, value); }
        }
        public PostViewModel(INavigationService navigationMethod, IPageDialogService dialogService, IEventAggregator eventAggregator) : base(navigationMethod, dialogService, eventAggregator)
        {
            SendClicked = new Command(Send_Clicked);
        }

        private async void Send_Clicked()
        {
            //await PostDatabase.Instance.UpdateItemData(PostContent);
            await NavigationMethod.GoBackAsync();
            EventAggregator.GetEvent<SendMessageEvent>().Publish(PostContent);
        }

    }
}
