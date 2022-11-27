using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Profile.Services;
using Plugin.Settings.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Naxam.Busuu.Profile.ViewModels
{
    public enum FriendState
    {
        Unfriend, Friend, RequestFriend, Me
    }

    public class ProfileViewModel : MvxViewModel
    {

        #region Command
        private IMvxCommand _SettingCommand;

        public IMvxCommand SettingCommand
        {
            get { return _SettingCommand = _SettingCommand ?? new MvxCommand(RunSettingCommand); }
        }

        void RunSettingCommand()
        {
            ShowViewModel<ProfileSettingViewModel>();
        }

        private IMvxCommand _SelectFriendCommand;

        public IMvxCommand SelectFriendCommand
        {
            get { return _SelectFriendCommand = _SelectFriendCommand ?? new MvxCommand(RunSelectFriendCommand); }

        }

        void RunSelectFriendCommand()
        {
            ShowViewModel<FriendListViewModel>(User.Id);
        }

        private IMvxCommand _RequestFriendCommand;

        public IMvxCommand RequestFriendCommand
        {
            get { return _RequestFriendCommand = _RequestFriendCommand ?? new MvxCommand(RunRequestFriendCommand); }

        }

        void RunRequestFriendCommand()
        {
            if (FriendState == FriendState.Friend)
            {
                FriendState = FriendState.Unfriend;
            }
            if (FriendState == FriendState.Unfriend)
            {
                FriendState = FriendState.RequestFriend;
            }
        }



        #endregion Command

        #region Property

        private FriendState _FriendState;

        public FriendState FriendState
        {
            get { return _FriendState; }
            set
            {
                if (_FriendState != value)
                {
                    _FriendState = value;
                    RaisePropertyChanged();
                }
            }
        }



        private UserModel _CurrentUser;
        public UserModel CurrentUser
        {
            get => _CurrentUser;
            set => SetProperty(ref _CurrentUser, value);
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
                    RaisePropertyChanged(() => LanguageFlag);
                }
            }
        }

        private IList<SocialModel> _MyExercises;

        public IList<SocialModel> Exercises
        {
            get { return _MyExercises; }
            set
            {
                if (_MyExercises != value)
                {
                    _MyExercises = value;
                    RaisePropertyChanged();
                }
            }
        }


        private IList<SocialModel> _MyCorrections;

        public IList<SocialModel> Corrections
        {
            get { return _MyCorrections; }
            set
            {
                if (_MyCorrections != value)
                {
                    _MyCorrections = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => CountLikeText);
                }
            }
        }


        public string CountLikeText
        {
            get { return Corrections.Select(d => d.Feedbacks.Where(e => e.User.Id == User.Id).Sum(s => s.Likes.Count)).Sum(s => s).ToString() + " LIKES"; }
        }

        private string _BestCorrections;

        public string BestCorrections
        {
            get { return _BestCorrections; }
            set
            {
                if (_BestCorrections != value)
                {
                    _BestCorrections = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string LanguageFlag
        {
            get { return User.Languages?.Count > 0 ? User.Languages[0].Flag : "flag_small_english"; }
        }


        #endregion Property

        IDataProfileService dataProfileService;
        ISettings settings;

        public ProfileViewModel(IDataProfileService dataProfileService, ISettings settings)
        {
            this.dataProfileService = dataProfileService;
            this.settings = settings;
        }

        protected override void ReloadFromBundle(IMvxBundle state)
        {
            base.ReloadFromBundle(state);
        }

        protected override void SaveStateToBundle(IMvxBundle bundle)
        {
            base.SaveStateToBundle(bundle);
        }

        public override void Start()
        {
            User = dataProfileService.GetUser(settings.GetValueOrDefault("user", 1)).Result;
            CurrentUser = dataProfileService.GetUser(settings.GetValueOrDefault("currentUser", 1)).Result;
            Corrections = new MvxObservableCollection<SocialModel>(dataProfileService.GetCorrections(User).Result);
            Exercises = new MvxObservableCollection<SocialModel>(dataProfileService.GetExercises(User).Result);

           
            var friendRequest = User.FriendsRequest.Where(d => d.Id == CurrentUser.Id).FirstOrDefault();
            var friend = User.Friends.Where(d => d.Id == CurrentUser.Id).FirstOrDefault();
            BestCorrections = "1 BEST CORRECTIONS";

            if (CurrentUser.Id == User.Id)
            {
                FriendState = FriendState.Me;
                return;
            }

            if (friend != null)
            {
                FriendState = FriendState.Friend;
                return;
            }

            if (friendRequest != null)
            {
                FriendState = FriendState.RequestFriend;
                return;
            }

            FriendState = FriendState.Unfriend;
        }
    }
}
