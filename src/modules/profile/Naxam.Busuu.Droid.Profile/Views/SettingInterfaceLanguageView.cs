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
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Profile.ViewModels;

namespace Naxam.Busuu.Droid.Profile.Views
{   [Activity(Label = "Interface Language")]
    public class SettingInterfaceLanguageView : MvxAppCompatActivity<SettingInterfaceLanguageViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.setting_interface_language_layout);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnSupportNavigateUp()
        {
            ViewModel.BackCommand?.Execute();
            return base.OnSupportNavigateUp();
        }
    }
}