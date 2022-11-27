using System;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Social.Services;

namespace Naxam.Busuu.Social.ViewModels
{
    public class GiveFeedbackViewModel : MvxViewModel
    {
        readonly IDataSocial _dataSocial;

        public GiveFeedbackViewModel(IDataSocial dataSocial)
        {
            _dataSocial = dataSocial;
        }

        SocialModel _CommentData;

        public SocialModel CommentData
        {
            get { return _CommentData; }
            set
            {
                if (_CommentData != value)
                {
                    _CommentData = value;
                    RaisePropertyChanged(() => CommentData);
                }
            }
        }

		private IMvxCommand _BackCmd;

		public IMvxCommand BackCmd
		{
			get { return _BackCmd = _BackCmd ?? new MvxCommand(RunBackCmd); }

		}

		void RunBackCmd()
		{
			Close(this);
		}


		private IMvxCommand _SendCmd;

		public IMvxCommand SendCmd
		{
			get { return _SendCmd = _SendCmd ?? new MvxCommand(RunsendCmd); }

		}

		void RunsendCmd()
		{
			// do stuff when clicking a send button
		}

        public async void Init(SocialModel item)
        {
            CommentData = item;
            CommentData.User = (await _dataSocial.GetSocialById(item.Id)).User;
        }
    }
}
