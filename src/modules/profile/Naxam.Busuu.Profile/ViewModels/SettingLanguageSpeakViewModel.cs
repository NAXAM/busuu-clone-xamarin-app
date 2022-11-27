using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Profile.Models;
using Naxam.Busuu.Profile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class SettingLanguageSpeakViewModel : MvxViewModel
    {
        IMvxMessenger messenger;
        IDataProfileService profileService;
        IDialogProfileService dialogProfileService;
        MvxSubscriptionToken token;
        public SettingLanguageSpeakViewModel(IMvxMessenger messenger, IDataProfileService profileService, IDialogProfileService dialogProfileService)
        {
            this.messenger = messenger;
            this.profileService = profileService;
            this.dialogProfileService = dialogProfileService;
            token = messenger.Subscribe<LanguageLevelMessage>(OnReviceMessage);
            Languages = new MvxObservableCollection<LanguageModel>(profileService.GetAllLanguage().Result);
        }

        private void OnReviceMessage(LanguageLevelMessage obj)
        {
            if (LanguageSelected == null)
                return;
            if (obj.Accept)
            {
                LanguageSelected.LanguageLevel = obj.Level;
                LanguageSelected.IsCurrent = true;
            }
            LanguageSelected = null;
        }

        public void Init(int id)
        {
            UserModel user = profileService.GetUser(id).Result;
            LanguagesSpeak = new MvxObservableCollection<LanguageModel>(user.SpeakLanguages);
            foreach (var item in LanguagesSpeak)
            {
                if (Languages.Contains(item))
                {
                    item.IsCurrent = true;
                }
            }
        }

        #region Property
        private MvxObservableCollection<LanguageModel> _Languages;

        public MvxObservableCollection<LanguageModel> Languages
        {
            get { return _Languages; }
            set
            {
                if (_Languages != value)
                {
                    _Languages = value;
                    RaisePropertyChanged();
                }
            }
        }

        private MvxObservableCollection<LanguageModel> _LanguagesSpeak;

        public MvxObservableCollection<LanguageModel> LanguagesSpeak
        {
            get { return _LanguagesSpeak; }
            set
            {
                if (_LanguagesSpeak != value)
                {
                    _LanguagesSpeak = value;
                    RaisePropertyChanged();
                }
            }
        }

        private LanguageModel _LanguageSelected;

        public LanguageModel LanguageSelected
        {
            get { return _LanguageSelected; }
            set
            {

                if (value?.IsCurrent == true)
                {
                    value.IsCurrent = false;
                    return;
                }
                _LanguageSelected = value;
                RaisePropertyChanged();
                if (value != null)
                {
                    dialogProfileService.ChooseLanguageLevel();
                }
            }
        }




        #endregion Property
        #region Command
        private IMvxCommand _ItemClickCommand;

        public IMvxCommand ItemClickCommand
        {
            get { return _ItemClickCommand = _ItemClickCommand ?? new MvxCommand<LanguageModel>(RunItemClickCommand); }

        }

        void RunItemClickCommand(LanguageModel language)
        {
            if (language.IsCurrent)
            {
                language.IsCurrent = false;
                return;
            }
            _LanguageSelected = language;
            RaisePropertyChanged();
            if (language != null)
            {
                dialogProfileService.ChooseLanguageLevel();
            }
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

        private IMvxCommand _DoneCommand;

        public IMvxCommand DoneCommand
        {
            get { return _DoneCommand = _DoneCommand ?? new MvxCommand(RunDoneCommand); }

        }

        void RunDoneCommand()
        {
            UserModel User = new UserModel
            {
                SpeakLanguages = LanguagesSpeak
            };
            messenger.Publish(new SettingModel(nameof(SettingLanguageSpeakViewModel))
            {
                Key = nameof(UserModel.SpeakLanguages),
                Value = User
            });
            Close(this);
        }

        #endregion Command
    }
}
