using System;
using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Profile.ViewModels
{
    public class ItWorksViewModel : MvxViewModel
    {
        private IMvxCommand _GoBackCommand;

        public IMvxCommand GoBackCommand
        {
            get { return _GoBackCommand = _GoBackCommand ?? new MvxCommand(RunGoBackCommand); }

        }

        void RunGoBackCommand()
        {
            Close(this);
        }

        private IMvxCommand _ReadFullCommand;

        public IMvxCommand ReadFullCommand
        {
            get { return _ReadFullCommand = _ReadFullCommand ?? new MvxCommand(RunReadFullCommand); }

        }

        void RunReadFullCommand()
        {
            Close(this);
        }
    }
}
