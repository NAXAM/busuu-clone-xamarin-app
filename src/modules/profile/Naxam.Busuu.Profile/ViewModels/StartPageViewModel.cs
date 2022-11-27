using System;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class StartPageViewModel : MvxViewModel
    {
        IMvxCommand _getStartedCommand;

        public IMvxCommand GetStartedCommand
        {
            get { return _getStartedCommand = _getStartedCommand ?? new MvxCommand(RunGetStartedCommand); }
        }

        void RunGetStartedCommand()
        {
            ShowViewModel<ChooseLanguageViewModel>();
        }


        IMvxCommand _loginCommand;

        public IMvxCommand LoginCommand
        {
            get { return _loginCommand = _loginCommand ?? new MvxCommand(RunLoginCommand); }
        }

        void RunLoginCommand()
        {
            ShowViewModel<LoginViewModel>();
        }
    }
}
