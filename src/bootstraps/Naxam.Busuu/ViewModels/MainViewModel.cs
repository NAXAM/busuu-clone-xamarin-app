using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Learning.ViewModels;
using Naxam.Busuu.Notification.ViewModels;
using Naxam.Busuu.Profile.ViewModels;
using Naxam.Busuu.Review.ViewModels;
using Naxam.Busuu.Social.Services;
using Naxam.Busuu.Social.ViewModels;
using Plugin.Settings.Abstractions;

namespace Naxam.Busuu.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        ISettings settings;
        public MainViewModel(ISettings settings)
        {
            this.settings = settings;
            settings.AddOrUpdateValue("user", 1);
            settings.AddOrUpdateValue("currentUser", 1);
        }

		public IDataSocial DataSocial;
		public MainViewModel(IDataSocial _datadiscover)
		{
			this.DataSocial = _datadiscover;
		}

        IMvxCommand _showInitialViewModelsCommand;
        public IMvxCommand ShowInitialViewModelsCommand
        {
            get
            {
                return _showInitialViewModelsCommand ?? (_showInitialViewModelsCommand = new MvxCommand(ShowInitialViewModels));
            }
        }

        void ShowInitialViewModels()
        {
            ShowViewModel<LearnViewModel>();
            ShowViewModel<ReviewViewModel>();
            ShowViewModel<SocialViewModel>();
            ShowViewModel<NotificationViewModel>();
            ShowViewModel<ProfileViewModel>();
        }

        private IMvxCommand _MenuSelectCommand;

        public IMvxCommand MenuSelectCommand
        {
            get { return _MenuSelectCommand = _MenuSelectCommand ?? new MvxCommand<int>(RunMenuSelectCommand); }

        }

        void RunMenuSelectCommand(int menuId)
        {
            switch (menuId)
            {
                case 0:
                    ShowViewModel<LearnViewModel>();
                    break;
                case 1:
                    ShowViewModel<ReviewViewModel>();
                    break;
                case 2:
                    ShowViewModel<SocialViewModel>();
                    break;
                case 3:
                    ShowViewModel<NotificationViewModel>();
                    break;
                case 4:
                    ShowViewModel<ProfileViewModel>();
                    break;
            }

        }
    }
}
