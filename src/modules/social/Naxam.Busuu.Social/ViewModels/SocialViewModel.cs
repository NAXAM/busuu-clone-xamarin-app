using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Social.ViewModels
{
    public class SocialViewModel : MvxViewModel
    {
        #region Property
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

        private MvxObservableCollection<SocialModel> _FriendsData;

        public MvxObservableCollection<SocialModel> FriendsData
        {
            get { return _FriendsData; }
            set
            {
                if (_FriendsData != value)
                {
                    _FriendsData = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion Property

        #region Command

        public IMvxCommand FilterViewCommand
        {
            get { return new MvxCommand(() => ShowViewModel<FilterViewModel>()); }
        }

        private IMvxCommand _DiscoverItemClickCommand;

        public IMvxCommand DiscoverItemClickCommand
        {
            get { return _DiscoverItemClickCommand = _DiscoverItemClickCommand ?? new MvxCommand<int>(RunDiscoverItemClickCommand); }

        }

        void RunDiscoverItemClickCommand(int position)
        {
            ShowViewModel<SocialDetailViewModel>(DiscoverData[position]);
        }

        private IMvxCommand _FriendsItemClickCommand;

        public IMvxCommand FriendsItemClickCommand
        {
            get { return _FriendsItemClickCommand = _FriendsItemClickCommand ?? new MvxCommand<int>(RunFriendsItemClickCommand); }
        }

        void RunFriendsItemClickCommand(int position)
        {
            ShowViewModel<SocialDetailViewModel>(DiscoverData[position]);
        }

        private IMvxCommand _FilterCommand;

        public IMvxCommand FilterCommand
        {
            get { return _FilterCommand = _FilterCommand ?? new MvxCommand(RunFilterCommand); }

        }

        void RunFilterCommand()
        {
            ShowViewModel<FilterViewModel>();
        }

        #endregion Command
    }
}
