using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Services;
using Plugin.Settings.Abstractions;

namespace Naxam.Busuu.Social.ViewModels
{
    public class FriendsViewModel : MvxViewModel
    {
        private bool _FilterWriting;

        public bool FilterWriting
        {
            get { return _FilterWriting; }
            set
            {
                if (_FilterWriting != value)
                {
                    _FilterWriting = value;
                    RaisePropertyChanged();
                }
            }
        }


        private bool _FilterSpeaking;

        public bool FilterSpeaking
        {
            get { return _FilterSpeaking; }
            set
            {
                if (_FilterSpeaking != value)
                {
                    _FilterSpeaking = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _FilterLanguage;

        public bool FilterLanguage
        {
            get { return _FilterLanguage; }
            set
            {
                if (_FilterLanguage != value)
                {
                    _FilterLanguage = value;
                    RaisePropertyChanged();
                }
            }
        }

        MvxObservableCollection<SocialModel> _friends;

        public MvxObservableCollection<SocialModel> FriendsData
        {
            get { return _friends; }
            set
            {
                if (_friends != value)
                {
                    _friends = value;
                    RaisePropertyChanged(() => FriendsData);
                }
            }
        }

        IMvxCommand _ViewFriendsCommand;
        public IMvxCommand ViewFriendsCommand
        {
            get
            {
                return (_ViewFriendsCommand = _ViewFriendsCommand ?? new MvxCommand<SocialModel>(ExecuteViewFriendsCommand));
            }
        }

        void ExecuteViewFriendsCommand(SocialModel item)
        {
            ShowViewModel<SocialDetailViewModel>(new SocialModel
            {
                Id = item.Id
            });
        }

        ISettings settings;
        readonly IDataSocial _datafriends;
        MvxSubscriptionToken _token;

        public FriendsViewModel(IDataSocial datafriens, IMvxMessenger messenger, ISettings settings)
        {
			this.settings = settings;
            _datafriends = datafriens;
            _token = messenger.Subscribe<FilterModel>(OnReceiveMessage);
        }

        async void OnReceiveMessage(FilterModel obj)
        {
            FilterWriting = obj.FilterWriting;
            FilterSpeaking = obj.FilterSpeaking;
            FilterLanguage = obj.FilterLanguage;

			FriendsData = new MvxObservableCollection<SocialModel>(await _datafriends.GetFriendSocial(FilterSpeaking, FilterWriting));
		}


        public async override void Start()
        {
            FilterWriting = settings.GetValueOrDefault(nameof(FilterWriting), true);
            FilterSpeaking = settings.GetValueOrDefault(nameof(FilterSpeaking), true);
            FilterLanguage = settings.GetValueOrDefault(nameof(FilterLanguage), true);
            FriendsData = new MvxObservableCollection<SocialModel>(await _datafriends.GetFriendSocial(FilterSpeaking, FilterWriting));
        }

    }
}
