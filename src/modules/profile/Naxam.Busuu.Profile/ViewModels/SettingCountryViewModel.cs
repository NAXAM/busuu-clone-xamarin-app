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
    public class SettingCountryViewModel : MvxViewModel
    {
        IMvxMessenger messenger;
        IDataProfileService profileService;

        public SettingCountryViewModel(IMvxMessenger messenger, IDataProfileService profileService)
        {
            this.messenger = messenger;
            this.profileService = profileService;
            Countries = new MvxObservableCollection<CountryModel>(profileService.GetAllCountry().Result);
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            Key = parameters.Data["Key"];
            string value = parameters.Data["Value"];
            Value = Countries?.Where(d => d.Country == value).FirstOrDefault()?? Countries.First();
            VisibleDoneButton = false;
        }

        #region Property
        private MvxObservableCollection<CountryModel> _Countries;

        public MvxObservableCollection<CountryModel> Countries
        {
            get { return _Countries; }
            set
            {
                if (_Countries != value)
                {
                    _Countries = value;
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

        private bool _VisibleDoneButton;

        public bool VisibleDoneButton
        {
            get { return _VisibleDoneButton; }
            set
            {
                if (_VisibleDoneButton != value)
                {
                    _VisibleDoneButton = value;
                    RaisePropertyChanged();
                }
            }
        }


        private CountryModel _Value;

        public CountryModel Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    VisibleDoneButton = true;
                    RaisePropertyChanged();
                }
            }
        }

        private string _Key;

        public string Key
        {
            get { return _Key; }
            set
            {
                if (_Key != value)
                {
                    _Key = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion Property

        #region Command
        private IMvxCommand _DoneCommand;

        public IMvxCommand DoneCommand
        {
            get { return _DoneCommand = _DoneCommand ?? new MvxCommand(RunDoneCommand); }

        }

        void RunDoneCommand()
        {
            User = new UserModel
            {
                Country = Value
            };
            messenger.Publish(new SettingModel("")
            {
                Key = Key,
                Value = User
            });
            Close(this);
        }

        private IMvxCommand _CancelCommand;

        public IMvxCommand CancelCommand
        {
            get { return _CancelCommand = _CancelCommand ?? new MvxCommand(RunCancelCommand); }

        }

        void RunCancelCommand()
        {
            Close(this);
        }

        #endregion Command
    }
}

