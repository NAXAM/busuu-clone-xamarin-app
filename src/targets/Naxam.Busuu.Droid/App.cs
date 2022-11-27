using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Naxam.Busuu.Social.Services;
using Naxam.Busuu.Notification.Services;
using Naxam.Busuu.Review.Services;
using Naxam.Busuu.Learning.Services;
using Naxam.Busuu.Profile.Services;
using Naxam.Busuu.Social.ViewModels;
using MvvmCross.Core.Navigation;
using Naxam.Busuu.ViewModels;
using Naxam.Busuu.Droid.Profile.Service;
using Naxam.Busuu.Profile.ViewModels;
using Naxam.Busuu.Learning.ViewModels;
using Plugin.Settings.Abstractions;
using Plugin.Settings;
using Naxam.Busuu.Review.ViewModels;
using Acr.UserDialogs;

namespace Naxam.Busuu.Droid
{
    public class App : MvxApplication
    {
        public App()
        {
            //  Mvx.RegisterType<IDialogService, DialogService>();
        }
        public override void Initialize()
        {
            Mvx.RegisterSingleton<ISettings>(CrossSettings.Current);
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.RegisterType<IDataSocial, DataSocial>();
            Mvx.RegisterType<IDataNotification, DataNotification>();
            Mvx.RegisterType<IReviewService, ReviewService>();
            Mvx.RegisterType<ILearningService, LearningService>();
            Mvx.RegisterType<IDataProfileService, DataProfileService>();
            Mvx.RegisterType<IDialogProfileService, DialogProfileService>();
            RegisterAppStart(new AppStart());
            // RegisterAppStart<MainIndexViewModel>();
        }
    }

    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            Mvx.Resolve<IMvxNavigationService>().Navigate<LearnViewModel>();
        }
    }
}