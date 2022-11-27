using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Social.Models;
using Naxam.Busuu.Social.Services;
using Plugin.Settings.Abstractions;

namespace Naxam.Busuu.Social.ViewModels
{
    public class DiscoverViewModel : MvxViewModel
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

        MvxObservableCollection<SocialModel> _discovers;

        public MvxObservableCollection<SocialModel> DiscoverData
        {
            get { return _discovers; }
            set
            {
                if (_discovers != value)
                {
                    _discovers = value;
                    RaisePropertyChanged(() => DiscoverData);
                }
            }
        }

        IMvxCommand _ViewDisoverCommand;
        public IMvxCommand ViewDisoverCommand
        {
            get
            {
                return (_ViewDisoverCommand = _ViewDisoverCommand ?? new MvxCommand<SocialModel>(ExecuteViewDiscoverCommand));
            }
        }

        void ExecuteViewDiscoverCommand(SocialModel item)
        {
            ShowViewModel<SocialDetailViewModel>(new SocialModel
            {
                Id = item.Id
            });
        }

        readonly ISettings settings;
        readonly IDataSocial _datadiscover;
        MvxSubscriptionToken _token;

        public DiscoverViewModel(ISettings settings, IDataSocial datadiscover, IMvxMessenger messenger)
        {
            this.settings = settings;
            _datadiscover = datadiscover;
            _token = messenger.Subscribe<FilterModel>(OnReceiveMessage);
        }

        async void OnReceiveMessage(FilterModel obj)
        {
            FilterWriting = obj.FilterWriting;
            FilterSpeaking = obj.FilterSpeaking;
            FilterLanguage = obj.FilterLanguage;

            DiscoverData = new MvxObservableCollection<SocialModel>(await _datadiscover.GetDiscoverSocial(FilterSpeaking, FilterWriting));
        }

        public async override void Start()
        {
            FilterWriting = settings.GetValueOrDefault(nameof(FilterWriting), true);
            FilterSpeaking = settings.GetValueOrDefault(nameof(FilterSpeaking), true);
            FilterLanguage = settings.GetValueOrDefault(nameof(FilterLanguage), true);
            DiscoverData = new MvxObservableCollection<SocialModel>(await _datadiscover.GetDiscoverSocial(FilterSpeaking, FilterWriting));
        }
    }
}
