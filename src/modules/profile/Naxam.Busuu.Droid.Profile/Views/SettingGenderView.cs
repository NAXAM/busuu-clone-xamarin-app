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
{
    [Activity(Theme = "@style/AppTheme.NoActionBar")]
    public class SettingGenderView : MvxAppCompatActivity<SettingGenderViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.setting_gender_layout);
        }
    }
}