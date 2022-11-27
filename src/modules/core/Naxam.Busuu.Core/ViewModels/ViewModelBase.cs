using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Core.ViewModels
{
    public class ViewModelBase : MvxViewModel
    {
        private IMvxCommand _GoBackCommand;

        public IMvxCommand GoBackCommand
        {
            get { return _GoBackCommand = _GoBackCommand ?? new MvxCommand(RunGoBackCommand); }

        }

        public virtual void RunGoBackCommand()
        {
            Close(this);
        }

    }
}
