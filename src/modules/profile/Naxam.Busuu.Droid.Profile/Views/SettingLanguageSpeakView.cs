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
using MvvmCross.Binding.Droid.Views;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "Settings")]
    public class SettingLanguageSpeakView : MvxAppCompatActivity<SettingLanguageSpeakViewModel>
    { 
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.setting_language_speak_layout);  FindViewById<MvxListView>(Resource.Id.lstView);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnSupportNavigateUp()
        {
            ViewModel.BackCommand?.Execute();
            return base.OnSupportNavigateUp();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_setting_language_speak, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menu_done)
            {
                ViewModel.DoneCommand?.Execute();
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}