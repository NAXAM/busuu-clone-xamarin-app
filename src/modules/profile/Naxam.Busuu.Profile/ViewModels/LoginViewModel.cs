using MvvmCross.Core.ViewModels; 
using Naxam.Busuu.Learning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks; 

namespace Naxam.Busuu.Profile.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        // properties
        private bool _IsEnableLoginBtn;

        public bool IsEnableLoginBtn
        {
            get { return _IsEnableLoginBtn; }
            set
            {
                if (_IsEnableLoginBtn != value)
                {
                    _IsEnableLoginBtn = value;
                    RaisePropertyChanged(()=> IsEnableLoginBtn);
                }
            }
        }


        private string _TextPass;

        public string TextPass
        {
            get { return _TextPass; }
            set
            {
                if (_TextPass != value)
                {
                    _TextPass = value;
                    IsEnableLoginBtn = CheckPhoneNumber(TextEmail, TextPass);
                    RaisePropertyChanged();
                }
            }
        }

        private string _TextEmail;

        public string TextEmail
        {
            get { return _TextEmail; }
            set
            {
                if (_TextEmail != value)
                {
                    _TextEmail = value;
                    IsEnableLoginBtn = CheckPhoneNumber(TextEmail, TextPass);
                    RaisePropertyChanged();
                }
            }
        }
        // constructor
        public LoginViewModel()
        {

        }

        // commands
        private IMvxCommand _ForgotPasswordCommand;

        public IMvxCommand ForgotPasswordCommand
        {
            get { return _ForgotPasswordCommand = _ForgotPasswordCommand ?? new MvxCommand(RunForgotPasswordCommand); }

        }

        void RunForgotPasswordCommand()
        {
            ShowViewModel<ForgotPasswordViewModel>();
        }

        private IMvxCommand _LoginCmd;

        public IMvxCommand LoginCmd
        {
            get { return _LoginCmd = _LoginCmd ?? new MvxCommand(RunLoginCmd); }

        }

        void RunLoginCmd()
        {
            ShowViewModel<LearnViewModel>();
        }

        private IMvxCommand _LoginViaFaceCmd;

        public IMvxCommand LoginViaFaceCmd
        {
            get { return _LoginViaFaceCmd = _LoginViaFaceCmd ?? new MvxCommand(RunLoginViaFaceCmd); }

        }

        void RunLoginViaFaceCmd()
        {

        }

        private IMvxCommand _LoginViaGoogleCmd;

        public IMvxCommand LoginViaGoogleCmd
        {
            get { return _LoginViaGoogleCmd = _LoginViaGoogleCmd ?? new MvxCommand(RunLoginViaGoogleCmd); }

        }

        void RunLoginViaGoogleCmd()
        {

        }

      
        private bool CheckPhoneNumber(string email, string pass)
        {
            Regex regex = new Regex("^[a-zA-Z0-9-_\\.]+@[a-z0-9]+\\.[a-z]{2,4}$");
            bool checkMail = regex.IsMatch(email);

            Regex regexP = new Regex("^+?[0-9]{9,13}$");
            bool checkPhone = regexP.IsMatch(email);
            return (checkMail || checkPhone) && pass.Length >= 6;
        }


    }
}
