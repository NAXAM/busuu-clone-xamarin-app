using System;
using System.Linq;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Social.Services;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Social.ViewModels
{
    public class SocialDetailViewModel : MvxViewModel
    {
        readonly IDataSocial _datasocialdetail;

        public SocialDetailViewModel(IDataSocial datasocialdetail)
        {
            _datasocialdetail = datasocialdetail;
        }

        public IMvxCommand CommentViewCommand
        {
            get { return new MvxCommand(() => ShowViewModel<GiveFeedbackViewModel>(SocialDetailData)); }
        }

		IMvxCommand _BackCommand;

		public IMvxCommand BackCommand
		{
			get { return _BackCommand = _BackCommand ?? new MvxCommand(() => Close(this)); }

		}

		IMvxCommand _ReplyViewCommand;

		public IMvxCommand ReplyViewCommand
		{
            get { return _ReplyViewCommand = _ReplyViewCommand ?? new MvxCommand(() => ShowViewModel<ReplyViewModel>()); }

		}

        SocialModel _socialdetail;
        public SocialModel SocialDetailData
        {
            get { return _socialdetail; }
            set
            {
                if (_socialdetail != value)
                {
                    _socialdetail = value;
                    RaisePropertyChanged(() => SocialDetailData);
                }
            }
        }

        double _rating;
        public double Rating
        {
            get { return _rating; }
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    RaisePropertyChanged();
                }
            }
        }

        int _countRating;
        public int CountRating
        {
            get { return _countRating; }
            set
            {
                if (_countRating != value)
                {
                    _countRating = value;
                    RaisePropertyChanged();
                }
            }
        }


        private SocialModel _Item;
        public SocialModel Item
        {
            get => _Item;
            set => SetProperty(ref _Item, value);
        }

        public async void Init(SocialModel item)
        {
            Item = item;
            SocialDetailData = await _datasocialdetail.GetSocialById(item.Id);
            SocialDetailData.Feedbacks = await _datasocialdetail.GetFeedbackById(item.Id);
            CountRating = SocialDetailData.Feedbacks == null ? 0 : SocialDetailData.Feedbacks.Count;
            Rating = CountRating == 0 ? 0 : (double)SocialDetailData.Feedbacks.Sum(d => d.Rating) / CountRating;
        }

        private IMvxCommand _GiveFeedBackCommand;

        public IMvxCommand GiveFeedBackCommand
        {
            get { return _GiveFeedBackCommand = _GiveFeedBackCommand ?? new MvxCommand(RunGiveFeedBackCommand); }

        }

        void RunGiveFeedBackCommand()
        {
            ShowViewModel<GiveFeedbackViewModel>(Item);
        }

        private IMvxCommand _ReplyCommand;

        public IMvxCommand ReplyCommand
        {
            get { return _ReplyCommand = _ReplyCommand ?? new MvxCommand<int>(RunReplyCommand); }

        }

        void RunReplyCommand(int position)
        {
            ShowViewModel<ReplyViewModel>(Item);
        }
    }
}
