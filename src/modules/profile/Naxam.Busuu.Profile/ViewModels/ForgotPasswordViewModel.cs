using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class ForgotPasswordViewModel : MvxViewModel
    {
        private string _emailPhone;

        public string EmailPhone
        {
            get { return _emailPhone; }
            set
            {
                if (_emailPhone != value)
                {
                    _emailPhone = value;
                    RaisePropertyChanged(() => EmailPhone);
                }
            }
        }
        
        public ForgotPasswordViewModel( )
        { 
           
        }

        public void Init(LanguageModel firstNavObject)
        {
            // use firstNavObject
        }

        private IMvxCommand _forgotPasswordCommand;

        public IMvxCommand ForgotPasswordCommand
        {
            get { return _forgotPasswordCommand = _forgotPasswordCommand ?? new MvxCommand(RunForgotPasswordCommand); }

        }

        void RunForgotPasswordCommand()
        {
            ShowViewModel<RegisterViewModel>(EmailPhone);
        }



    }
}
