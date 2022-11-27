using MvvmCross.Core.ViewModels;

namespace Naxam.Busuu.Core.ViewModels
{
    public class BuyPremiumViewModel : MvxViewModel
    {
#region command
        private IMvxCommand _GoBackCmd;

        public IMvxCommand GoBackCmd
        {
            get { return _GoBackCmd = _GoBackCmd ?? new MvxCommand(RunGoBackCmd); }

        }

        void RunGoBackCmd()
        {
            Close(this);
        }
        private IMvxCommand _RestorePurchaseCmd;

        public IMvxCommand RestorePurchaseCmd
        {
            get { return _RestorePurchaseCmd = _RestorePurchaseCmd ?? new MvxCommand(RunRestorePurchaseCmd); }

        }

        void RunRestorePurchaseCmd()
        {
            // do stuff here
        }


        #endregion


    }
}
