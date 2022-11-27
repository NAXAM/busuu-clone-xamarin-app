using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Naxam.Busuu.ViewModels;
using Naxam.Busuu.Notification.Services;
using Naxam.Busuu.Social.Services;
using Naxam.Busuu.Review.Services;
using Naxam.Busuu.Start.ViewModels;
using Naxam.Busuu.Learning.Services;
using Naxam.Busuu.Profile.Services;
using System.IO;
using System;
using Naxam.Busuu.iOS.Profile.Services;
using Plugin.Settings;
using Acr.UserDialogs;

namespace Naxam.Busuu.iOS
{
    public class App : MvxApplication
	{
		public App()
        {
            Mvx.RegisterSingleton(CrossSettings.Current);
            Mvx.RegisterSingleton(() => UserDialogs.Instance);
			Mvx.RegisterType<ILearningService, LearningService>();
            Mvx.RegisterType<IReviewService, ReviewService>();
            Mvx.RegisterType<IDataSocial, DataSocial>();
			Mvx.RegisterType<IDataNotification, DataNotification>();
            Mvx.RegisterType<IDataProfileService, DataProfileService>();
            Mvx.RegisterType<IDialogProfileService, DialogProfileService>();
	
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			try
			{
				var textfile = File.ReadAllText(documents + "/MySettingDocuments/fileCheckLogin.txt");

				if (textfile == "0")
				{
                    RegisterAppStart<MainViewModel>();
				}
                else
                {
					RegisterAppStart<StartPageViewModel>();
				}
			}
			catch
			{
				RegisterAppStart<StartPageViewModel>();
			}
        }		
	}
}
