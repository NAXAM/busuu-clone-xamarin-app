using System;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Start.ViewModels
{
    public class StartPageViewModel : MvxViewModel
    {
        public IMvxCommand GetStartedCommand
        {
			get { return new MvxCommand(() => ShowViewModel<ChooseLanguageViewModel>()); }
        }

        IMvxCommand _loginCommand;
        public IMvxCommand LoginCommand
        {
			get
			{
				return _loginCommand = _loginCommand ?? new MvxCommand(() => ShowViewModel<LoginViewModel>());
			}
        }
    }
}
