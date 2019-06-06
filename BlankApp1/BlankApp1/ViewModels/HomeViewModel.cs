using BlankApp1.Utils;
using BlankApp1.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlankApp1.ViewModels
{
	public class HomeViewModel : ViewModelBase
    {
        public ICommand AddClicked { get; set; }
        public ICommand SettingsClicked { get; set; }

        public int MaxId = 0;

        private ImageSource _imageSource;
        public ImageSource Source
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }

        private string _backgroundColor = "Black";
        public string BackgroundColor 
        {
            get { return _backgroundColor; }
            set { SetProperty(ref _backgroundColor, value); }
        }

        private ImageSource _settingsSource;
        public ImageSource SettingsSource
        {
            get { return _settingsSource; }
            set { SetProperty(ref _settingsSource, value); }
        }

        private ImageSource _likeImageSource;
        public ImageSource LikeImageSource
        {
            get { return _likeImageSource; }
            set { SetProperty(ref _likeImageSource, value); }
        }

        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        

        public readonly INavigationService navMethod;
        public readonly IPageDialogService pagDialog;
        public readonly IEventAggregator eventAgg;

        public HomeViewModel(INavigationService navigationMethod, IPageDialogService dialogService, IEventAggregator eventAggregator) : base(navigationMethod, dialogService, eventAggregator)
        {
            Source = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile("add_button.png") : ImageSource.FromFile("Assets/add_button.png");
            SettingsSource = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile("settings.png") : ImageSource.FromFile("Assets/settings.png");
            //Items = new ObservableCollection<PostItem>(PostDatabase.Instance.GetItemsAsync().Result);
            Items = new ObservableCollection<Item>();

            navMethod = navigationMethod;
            pagDialog = dialogService;
            eventAgg = eventAggregator;

            AddClicked = new Command(ImageButton_Clicked);
            SettingsClicked = new Command(Settings_Clicked);
            eventAggregator.GetEvent<SendMessageEvent>().Subscribe(MessageReceived);
        }

        private async void Settings_Clicked()
        {
            await NavigationMethod.NavigateAsync(nameof(SettingsPage));
        }

        private void MessageReceived(string post)
        {
            //Items = new ObservableCollection<PostItem>(PostDatabase.Instance.GetItemsAsync().Result);
            if (post == "Red" || post == "Orange" || post == "Green" || post == "Violet" || post == "Blue" || post == "Indigo" || post == "Yellow")
            {
                BackgroundColor = post;
            }
            else
            {
                MaxId++;
                Item it = new Item(navMethod, pagDialog, eventAgg);
                it.Content = post;
                it.ID = MaxId;
                Items.Add(it);
            }
        }

        private async void ImageButton_Clicked()
        {
            await NavigationMethod.NavigateAsync(nameof(Post));
        }

    }

    public class Item : ViewModelBase
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public ImageSource LikeImageSource => Device.RuntimePlatform == Device.UWP ? ImageSource.FromFile("Assets/like_button.png") : ImageSource.FromFile("like_button.png");

        private int _NrOfLikes;
        public int NrOfLikes
        {
            get { return _NrOfLikes; }
            set { SetProperty(ref _NrOfLikes, value); }
        }

        private bool _isLiked = false;
        public bool IsLiked
        {
            get { return _isLiked; }
            set { SetProperty(ref _isLiked, value); }
        }

        private string _isHighlighted = "Transparent";
        public string IsHighlighted
        {
            get { return _isHighlighted; }
            set { SetProperty(ref _isHighlighted, value); }
        }

        public ICommand LikeClicked { get; set; }

        private void Like_Clicked()
        {
            if (IsLiked == false)
            {
                IsHighlighted = "Accent";
                NrOfLikes++;
                IsLiked = true;
            } else {
                IsHighlighted = "Transparent";
                NrOfLikes--;
                IsLiked = false;
            }

        }

        public Item (INavigationService navigationMethod, IPageDialogService dialogService, IEventAggregator eventAggregator) : base(navigationMethod, dialogService, eventAggregator)
        {
            NrOfLikes = 0;
            LikeClicked = new Command(Like_Clicked);
        }
    }
}
