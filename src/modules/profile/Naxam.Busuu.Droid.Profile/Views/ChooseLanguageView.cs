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
using MvvmCross.Droid.Views;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Theme = "@style/AppTheme", Label ="")]
    public class ChooseLanguageView : MvxAppCompatActivity
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.ChooseLanguageActivity);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Elevation = 0; 
        }
 
    }
}