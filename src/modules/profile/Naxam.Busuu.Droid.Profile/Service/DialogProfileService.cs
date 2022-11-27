using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Profile.Services;
using Naxam.Busuu.Droid.Profile.Dialogs;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using Naxam.Busuu.Profile.Models;

namespace Naxam.Busuu.Droid.Profile.Service
{
    public class DialogProfileService : IDialogProfileService
    {
        public void ChooseLanguageLevel()
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;
            ChooseLanguageLevelDialog dialog = new ChooseLanguageLevelDialog(act);
            dialog.DismissEvent += Dialog_DismissEvent;
            dialog.Show();
        }

        private void Dialog_DismissEvent(object sender, EventArgs e)
        {
            ChooseLanguageLevelDialog dialog = (ChooseLanguageLevelDialog)sender;
            var messenger = Mvx.Resolve<IMvxMessenger>();
            messenger.Publish<LanguageLevelMessage>(new LanguageLevelMessage("")
            {
                Accept = dialog.Accept,
                Level = dialog.Level
            });
        }
    }
}