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
using Android.Graphics;
using Naxam.Busuu.Droid.Core;
using Naxam.Busuu.Social.ViewModels;
using MvvmCross.Droid.Support.V4;
using Android.Support.V7.App;
using MvvmCross.Binding.Droid.BindingContext;

namespace Naxam.Busuu.Droid.Social.Views
{
    [NxFragment(BusuuFragmentHosts.MainView, true, ViewModelType = typeof(FilterViewModel))]
    [Register("naxam.busuu.droid.social.views.FilterFragment")]
    public class FilterFragment : MvxFragment<FilterViewModel>
    { 
        Android.Support.V7.Widget.Toolbar toolbar;   

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.activity_filter, container, false);
            Init(view);
            return view;
        }


        public void Init(View view)
        {
            // showWriting = Intent.GetBooleanExtra(SocialFragment.ShowWriting, true);
            //  showSpeaking = Intent.GetBooleanExtra(SocialFragment.ShowSpeaking, true);
            //  filterLanguage = Intent.GetBooleanExtra(SocialFragment.FilterLanguage, true);
            //  swtShowSpeaking = FindViewById<Switch>(Resource.Id.swtShowSpeaking);
            //   swtShowWriting = FindViewById<Switch>(Resource.Id.swtShowWriting);
            //   swtFilterLanguage = FindViewById<Switch>(Resource.Id.swtLanguage);
            //   txtShowWriting = FindViewById<TextView>(Resource.Id.txtShowWriting);
            //   txtShowSpeaking = FindViewById<TextView>(Resource.Id.txtShowSpeaking);
            //    txtDone = FindViewById<TextView>(Resource.Id.txtDone);
            // ImageView imgFlag = FindViewById<ImageView>(Resource.Id.imgFlag);
            //  imgFlag.SetImageResource(Resource.Drawable.flag_small_english);
            //  txtDone.Visibility = ViewStates.Gone;
            //   txtDone.Click += TxtDone_Click;
            toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitleTextColor(Color.White);
            toolbar.SetSubtitleTextColor(Color.White);
            toolbar.Title = "Social";
            ((AppCompatActivity)Activity).SetSupportActionBar(toolbar);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            //swtShowSpeaking.Checked = showSpeaking;

            //swtShowWriting.Checked = showWriting;
            //swtFilterLanguage.Checked = filterLanguage;

            //swtShowSpeaking.CheckedChange += SwtShowSpeaking_CheckedChange;
            //swtFilterLanguage.CheckedChange += SwtShowSpeaking_CheckedChange;
            //swtShowWriting.CheckedChange += SwtShowSpeaking_CheckedChange;
            //txtShowWriting.Click += TxtShowWriting_Click;
            //txtShowSpeaking.Click += TxtShowSpeaking_Click;

        }

        private void TxtDone_Click(object sender, EventArgs e)
        {
            //  Finish();
        }

        //private void SwtShowSpeaking_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        //{
        //    txtDone.Visibility = CheckDone() ? ViewStates.Visible : ViewStates.Gone;
        //    intent.PutExtra(SocialFragment.ShowSpeaking, swtShowSpeaking.Checked);
        //    intent.PutExtra(SocialFragment.ShowWriting, swtShowWriting.Checked);
        //    intent.PutExtra(SocialFragment.FilterLanguage, swtFilterLanguage.Checked);
        //    SetResult(Result.Ok, intent);
        //}


        //private void TxtShowSpeaking_Click(object sender, EventArgs e)
        //{
        //    swtShowSpeaking.Checked = !swtShowSpeaking.Checked;
        //}

        //private void TxtShowWriting_Click(object sender, EventArgs e)
        //{
        //    swtShowWriting.Checked = !swtShowWriting.Checked; 
        //}

        //private bool CheckDone()
        //{ 
        //    if (!swtFilterLanguage.Checked)
        //        return false;
        //    if (!swtShowSpeaking.Checked && !swtShowWriting.Checked)
        //        return false;
        //    return true; ;
        //}
    }
}