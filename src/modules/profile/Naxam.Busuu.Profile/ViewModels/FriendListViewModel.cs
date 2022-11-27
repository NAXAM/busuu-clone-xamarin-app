using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Profile.Models;
using Naxam.Busuu.Profile.Services;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class FriendListViewModel : MvxViewModel
    {
        int id;
        IMvxMessenger messenger;
        IDataProfileService profileService;
        ISettings settings;

        public FriendListViewModel(ISettings settings, IMvxMessenger messenger, IDataProfileService profileService)
        {
            this.settings = settings;
            this.messenger = messenger;
            this.profileService = profileService;
            User = profileService.GetUser(id).Result;
            Friends = new MvxObservableCollection<UserModel>(User.Friends);
            FriendsSearch = new MvxObservableCollection<UserModel>();

            if (User.FriendsRequest?.Count > 0)
            {
                Friends.Insert(0, new FriendListModel
                {
                    Photo = User.FriendsRequest[0].Photo,
                    Name = "Friends Request",
                    Count = User.FriendsRequest.Count
                });
            }

            VisibleButtonSearch = true;
        }

        public void Init(int id)
        {
            this.id = id;
        }

        #region Property

        private string _SearchText;

        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                if (_SearchText != value)
                {
                    _SearchText = value;
                    if (value.Length > 0)
                    {
                        VisibleCloseButton = true;
                        RunSearchCommand();
                    }
                    else
                    {
                        VisibleCloseButton = false;
                    }
                    RaisePropertyChanged();
                }
            }
        }

        private bool _VisibleButtonSearch = true;

        public bool VisibleButtonSearch
        {
            get { return _VisibleButtonSearch; }
            set
            {
                if (_VisibleButtonSearch != value)
                {
                    _VisibleButtonSearch = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _VisibleTextSearch;

        public bool VisibleTextSearch
        {
            get { return _VisibleTextSearch; }
            set
            {
                if (_VisibleTextSearch != value)
                {
                    _VisibleTextSearch = value;
                    RaisePropertyChanged();
                }
            }
        }


        private bool _VisibleCloseButton;

        public bool VisibleCloseButton
        {
            get { return _VisibleCloseButton; }
            set
            {
                if (_VisibleCloseButton != value)
                {
                    _VisibleCloseButton = value;
                    RaisePropertyChanged();
                }
            }
        }

        private MvxObservableCollection<UserModel> _Friends;

        public MvxObservableCollection<UserModel> Friends
        {
            get { return _Friends; }
            set
            {
                if (_Friends != value)
                {
                    _Friends = value;
                    RaisePropertyChanged();
                }
            }
        }

        private MvxObservableCollection<UserModel> _FriendsSeach;

        public MvxObservableCollection<UserModel> FriendsSearch
        {
            get { return _FriendsSeach; }
            set
            {
                if (_FriendsSeach != value)
                {
                    _FriendsSeach = value;
                    RaisePropertyChanged();
                }
            }
        }

        private UserModel _User;

        public UserModel User
        {
            get { return _User; }
            set
            {
                if (_User != value)
                {
                    _User = value;
                    RaisePropertyChanged();
                }
            }
        }


        #endregion Property
        #region Command
        private IMvxCommand _SelectedUserCommand;

        public IMvxCommand SelectedUserCommand
        {
            get { return _SelectedUserCommand = _SelectedUserCommand ?? new MvxCommand<UserModel>(RunSelectedUserCommand); }

        }

        void RunSelectedUserCommand(UserModel user)
        {
            settings.AddOrUpdateValue("user", user.Id);
            ShowViewModel<ProfileViewModel>();
        }




        private IMvxCommand _BackCommand;

        public IMvxCommand BackCommand
        {
            get { return _BackCommand = _BackCommand ?? new MvxCommand(RunBackCommand); }

        }

        void RunBackCommand()
        {
            Close(this);
            
        }

        private IMvxCommand _SearchCommand;

        public IMvxCommand SearchCommand
        {
            get { return _SearchCommand = _SearchCommand ?? new MvxCommand(RunSearchCommand); }

        }

        void RunSearchCommand()
        {
            VisibleButtonSearch = false;
            VisibleTextSearch = true;

            FriendsSearch.Clear();
            for (int i = 0; i < Friends.Count(); i++)
            {
                try
                {
                    int abcd = Friends[i].Name.IndexOf(SearchText);
                    if (Friends[i].Name.IndexOf(SearchText) != -1)
                    {
                        FriendsSearch.Add(Friends[i]);
                    }
                }
                catch
                {

                }
            }
        }

        private IMvxCommand _CloseCommand;

        public IMvxCommand CloseCommand
        {
            get { return _CloseCommand = _CloseCommand ?? new MvxCommand(RunCloseCommand); }
        }

        void RunCloseCommand()
        {
            SearchText = "";
            VisibleButtonSearch = true;
            VisibleTextSearch = false;
            VisibleCloseButton = false;
        }

        #endregion Command
    }
}
