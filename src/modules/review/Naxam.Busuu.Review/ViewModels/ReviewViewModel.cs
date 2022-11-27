using System.Linq;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Core.Messager;
using Naxam.Busuu.Core.ViewModels;
using Naxam.Busuu.Review.Models;
using Naxam.Busuu.Review.Services;

namespace Naxam.Busuu.Review.ViewModels
{
    public class ReviewViewModel : MvxViewModel
    {
        readonly IReviewService _reviewService;
        IMvxMessenger _messenger;

        public ReviewViewModel(IReviewService reviewService, IMvxMessenger messenger)
        {
            _reviewService = reviewService;
            _messenger = messenger;
        }

        #region Property

        string _SearchText = "";

        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                if (_SearchText != value)
                {
                    _SearchText = value;
                    if (value.Length > 0)
                    {
                        VisibleCloseButton = true;
                        RunSearchCommand();
                    }
                    else
                    {
                        VisibleCloseButton = false;
                    }

					_messenger.Publish(new ResetTableViewMessager(this));
                    RaisePropertyChanged();
                }
            }
        }

        bool _VisibleButtonSearch = true;

        public bool VisibleButtonSearch
        {
            get { return _VisibleButtonSearch; }
            set
            {
                if (_VisibleButtonSearch != value)
                {
                    _VisibleButtonSearch = value;
                    RaisePropertyChanged();
                }
            }
        }

        bool _VisibleTextSearch;

        public bool VisibleTextSearch
        {
            get { return _VisibleTextSearch; }
            set
            {
                if (_VisibleTextSearch != value)
                {
                    _VisibleTextSearch = value;
                    RaisePropertyChanged();
                }
            }
        }


        bool _VisibleCloseButton;

        public bool VisibleCloseButton
        {
            get { return _VisibleCloseButton; }
            set
            {
                if (_VisibleCloseButton != value)
                {
                    _VisibleCloseButton = value;
                    RaisePropertyChanged();
                }
            }
        }

        bool _EnabledAllButton;

		public bool EnabledAllButton
		{
			get { return _EnabledAllButton; }
			set
			{
				if (_EnabledAllButton != value)
				{
					_EnabledAllButton = value;
					RaisePropertyChanged();
				}
			}
		}

        bool _EnabledFavoriteButton = true;

		public bool EnabledFavoriteButton
		{
			get { return _EnabledFavoriteButton; }
			set
			{
				if (_EnabledFavoriteButton != value)
				{
					_EnabledFavoriteButton = value;
					RaisePropertyChanged();
				}
			}
		}	

		MvxObservableCollection<ReviewModel> _AllReviews;

		public MvxObservableCollection<ReviewModel> AllReviews
		{
			get { return _AllReviews; }
			set
			{
				if (_AllReviews != value)
				{
					_AllReviews = value;
					RaisePropertyChanged(() => AllReviews);
				}
			}
		}

		MvxObservableCollection<ReviewModel> _FavoriteReviews;

		public MvxObservableCollection<ReviewModel> FavoriteReviews
		{
			get { return _FavoriteReviews; }
			set
			{
				if (_FavoriteReviews != value)
				{
					_FavoriteReviews = value;
					RaisePropertyChanged(() => FavoriteReviews);
				}
			}
		}

		MvxObservableCollection<ReviewModel> _SearchReviews;

		public MvxObservableCollection<ReviewModel> SearchReviews
		{
			get { return _SearchReviews; }
			set
			{
				if (_SearchReviews != value)
				{
					_SearchReviews = value;
					RaisePropertyChanged(() => SearchReviews);
				}
			}
		}

        #endregion Property

        #region Command

        IMvxCommand _allClickCommand;
        public IMvxCommand AllClickCommand
        {
            get
            {
                return (_allClickCommand = _allClickCommand ?? new MvxCommand(ExecuteAllClickCommand));
            }
        }

        void ExecuteAllClickCommand()
        {
            EnabledAllButton = false;
			EnabledFavoriteButton = true;
			SearchText = "";
			VisibleButtonSearch = true;
			VisibleTextSearch = false;
			VisibleCloseButton = false;

			for (int i = 0; i < AllReviews.Count(); i++)
			{
                AllReviews[i].IsRead = false;
			}

			_messenger.Publish(new ResetTableViewMessager(this));
		}

        IMvxCommand _favoriteClickCommand;
        public IMvxCommand FavoriteClickCommand
        {
            get
            {
                return (_favoriteClickCommand = _favoriteClickCommand ?? new MvxCommand(ExecuteFavoriteClickCommand));
            }
        }

        void ExecuteFavoriteClickCommand()
        {
            EnabledFavoriteButton = false;
            EnabledAllButton = true;
			SearchText = "";
			VisibleButtonSearch = true;
			VisibleTextSearch = false;
			VisibleCloseButton = false;

			FavoriteReviews = new MvxObservableCollection<ReviewModel>(AllReviews.Where(d => d.IsFavorite));

			for (int i = 0; i < FavoriteReviews.Count(); i++)
			{
                FavoriteReviews[i].IsRead = false;
			}

			_messenger.Publish(new ResetTableViewMessager(this));
		}

        IMvxCommand _favoriteCommand;
        public IMvxCommand FavoriteCommand
        {
            get
            {
                return (_favoriteCommand = _favoriteCommand ?? new MvxCommand<ReviewModel>(ExecuteFavoriteCommand));
            }
        }

        void ExecuteFavoriteCommand(ReviewModel item)
        {
            item.IsFavorite = !item.IsFavorite;
        }

        IMvxCommand _TapCommand;
        public IMvxCommand TapCommand
        {
            get
            {
                return (_TapCommand = _TapCommand ?? new MvxCommand<ReviewModel>(ExecuteTapCommand));
            }
        }

        void ExecuteTapCommand(ReviewModel item)
        {
            item.IsRead = !item.IsRead;
        }

        IMvxCommand _goPremiumCommand;
        public IMvxCommand GoPremiumCommand
        {
            get
            {
                return (_goPremiumCommand = _goPremiumCommand ?? new MvxCommand(ExecuteGoPremiumCommand));
            }
        }

        void ExecuteGoPremiumCommand()
        {
            ShowViewModel<PremiumViewModel>();
        }

		IMvxCommand _SearchCommand;

		public IMvxCommand SearchCommand
		{
			get { return _SearchCommand = _SearchCommand ?? new MvxCommand(RunSearchCommand); }

		}

        async void RunSearchCommand()
        {
			VisibleButtonSearch = false;
			VisibleTextSearch = true;

            if (SearchText.Length > 0)
            {
                if (!EnabledAllButton)
                {
                    SearchReviews = new MvxObservableCollection<ReviewModel>(await _reviewService.SearchReview(AllReviews.ToArray(), SearchText));
                }
                else
                {
                    SearchReviews = new MvxObservableCollection<ReviewModel>(await _reviewService.SearchReview(FavoriteReviews.ToArray(), SearchText));
                }

				for (int i = 0; i < SearchReviews.Count(); i++)
				{
                    SearchReviews[i].IsRead = true;
				}
            }
        }

		private IMvxCommand _CloseCommand;

		public IMvxCommand CloseCommand
		{
			get { return _CloseCommand = _CloseCommand ?? new MvxCommand(RunCloseCommand); }
		}

		void RunCloseCommand()
		{
			if (!EnabledAllButton)
			{
                ExecuteAllClickCommand();
			}
			else
			{
                ExecuteFavoriteClickCommand();
			}

			_messenger.Publish(new ResetTableViewMessager(this));
		}

        #endregion Command

        public async override void Start()
        {
            AllReviews = new MvxObservableCollection<ReviewModel>(await _reviewService.GetAllReview());
			FavoriteReviews = new MvxObservableCollection<ReviewModel>();
            SearchReviews = new MvxObservableCollection<ReviewModel>();

			base.Start();
        }
    }
}