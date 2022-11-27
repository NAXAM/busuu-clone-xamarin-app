using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Profile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class ProfileInputViewModel : MvxViewModel
    {
        IMvxMessenger messenger;
        public ProfileInputViewModel(IMvxMessenger messenger)
        {
            this.messenger = messenger;
        }

        public void Init(UserModel user)
        {
            User = user;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters); 
            Key = parameters.Data["Key"]; 
             VisibleDoneButton = false;
        }

        #region Property
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


        private string _Value;

        public string Value
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
            User.Name = Value;
            messenger.Publish(new SettingModel("")
            { 
                
                Key = Key,
                Value = User
            });
        }


        #endregion Command
    }
}
