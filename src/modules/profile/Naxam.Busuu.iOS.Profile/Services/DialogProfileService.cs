using System;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.iOS.Profile.Common;
using Naxam.Busuu.iOS.Profile.Views;
using Naxam.Busuu.Profile.Services;

namespace Naxam.Busuu.iOS.Profile.Services
{
	public class ShowLanguageLevelDialogMessage : MvxMessage
	{
		public ShowLanguageLevelDialogMessage(object sender) : base(sender)
		{
            
		}
  	}

    public class DialogProfileService : IDialogProfileService
    {
        public static IMvxMessenger messenger = Mvx.Resolve<IMvxMessenger>();

        public void ChooseLanguageLevel()
        {
            messenger.Publish(new ShowLanguageLevelDialogMessage(this));
        }
    }
}
