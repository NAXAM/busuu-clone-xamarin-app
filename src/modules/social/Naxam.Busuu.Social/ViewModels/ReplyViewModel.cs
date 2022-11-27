using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naxam.Busuu.Social.ViewModels
{
    public class ReplyViewModel : MvxViewModel
    {
        public ReplyViewModel()
        {
            FeedBack = new FeedbackModel {
                User = new UserModel
                {
                    Name = "Naxam"
                }
            };
        }

        public void Init()
        {
            
        }

        private string _Message;
        public string Message
        {
            get => _Message;
            set => SetProperty(ref _Message, value);
        }


        private FeedbackModel _FeedBack;
        public FeedbackModel FeedBack
        {
            get => _FeedBack;
            set => SetProperty(ref _FeedBack, value);
        } 

        private IMvxCommand _GoBackCommand;

        public IMvxCommand GoBackCommand
        {
            get { return _GoBackCommand = _GoBackCommand ?? new MvxCommand(RunGoBackCommand); }

        }

        void RunGoBackCommand()
        {
            Close(this);
        }


    }
}
