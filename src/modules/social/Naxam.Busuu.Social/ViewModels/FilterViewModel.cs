using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Social.Models;
using Plugin.Settings.Abstractions;

namespace Naxam.Busuu.Social.ViewModels
{
    public class FilterViewModel : MvxViewModel
    {
        ISettings settings;
        IMvxMessenger messenger;
        public FilterViewModel(IMvxMessenger messenger, ISettings settings)
        {
            this.settings = settings;
            this.messenger = messenger;
            Flag = "flag_small_english";
        }

        public override void Start()
        {
            FilterWriting = settings.GetValueOrDefault(nameof(FilterWriting), true);
            FilterSpeaking = settings.GetValueOrDefault(nameof(FilterSpeaking), true);
            FilterLanguage = settings.GetValueOrDefault(nameof(FilterLanguage), true);
        }

        public override void ViewDisappeared()
        {
            settings.AddOrUpdateValue(nameof(FilterWriting), FilterWriting);
            settings.AddOrUpdateValue(nameof(FilterSpeaking), FilterSpeaking);
            settings.AddOrUpdateValue(nameof(FilterLanguage), FilterLanguage);
        }

        #region Property

        private string _Flag;

        public string Flag
        {
            get { return _Flag; }
            set
            {
                if (_Flag != value)
                {
                    _Flag = value;
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
                    RaisePropertyChanged(() => VisibilyDoneButton);
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
                    RaisePropertyChanged(() => VisibilyDoneButton);
                }
            }
        }

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
                    RaisePropertyChanged(() => VisibilyDoneButton);
                }
            }
        }


        public bool VisibilyDoneButton
        {
            get { return CheckVisibleDoneButton(); }
        }

        #endregion Property

        private IMvxCommand _DoneCommand;

        public IMvxCommand DoneCommand
        {
            get { return _DoneCommand = _DoneCommand ?? new MvxCommand(RunDoneCommand); }

        }

        void RunDoneCommand()
        {
            messenger.Publish(new FilterModel("")
            {
                FilterSpeaking = FilterSpeaking,
                FilterWriting = FilterWriting,
                FilterLanguage = FilterLanguage
            });
            Close(this);
        }

        public bool Write
        {
            get;
            set;
        }

        public IMvxCommand GoBackCommand
        {
            get { return new MvxCommand(() => Close(this)); }
        }

        private bool CheckVisibleDoneButton()
        {
            if (!FilterLanguage)
                return false;
            if (!FilterSpeaking && !FilterWriting)
                return false;
            return true;
        }
    }
}
