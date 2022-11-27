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
using Android.Support.V7.App;
using Naxam.Busuu.Droid.Profile.Controls;
using Android.Webkit;
using Android.Graphics;
using Naxam.Busuu.Droid.Core.Utils;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Profile.ViewModels;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "It Work!", Theme = "@style/AppTheme")]
    public class ItWorkView : MvxAppCompatActivity<ItWorksViewModel>
    {
        Button btnReadFull;
        WebView webView;
        TextView txtItWork;
        MyExpandableLayout expand1, expand2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_it_work);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            bindViews();
            btnReadFull.Background = BackgroundUtil.BackgroundRound(this, 1000, Resources.GetColor(Resource.Color.colorPrimary));
            btnReadFull.Text = "read the full study";
            btnReadFull.SetTextColor(Color.White);
            String str = "<p style=\"font-size:150%;\"><b>It works!</b></p>" +
                "<font color=\"#000000\"><p style=\"color:black;\" >Learning a foreign language isn’t always easy, but if you pick the right language and take the time to learn, it can really pay off</p></font>\n" +
                "<font color=\"#778086\"><p >Learning a foreign language isn’t always easy, but if you pick the right language and take the time to learn, it can really pay off</p>\n" +
                "<p >Learning a foreign language isn’t always easy, but if you pick the right language and take the time to learn, it can really pay off</p>\n" +
                "<p >Picking a second (or third) language to learn is a broad subject, and it’s ultimately a very personal choice. Perhaps you want to live somewhere new, or maybe you’re looking to explore business opportunities.</p>\n" +
                "<p >Picking a second (or third) language to learn is a broad subject, and it’s ultimately a very personal choice. Perhaps you want to live somewhere new, or maybe you’re looking to explore business opportunities.</p>\n" +
                "<font color=\"#000000\"><strong>• </strong></font>38% of Americans said gay and lesbian relations were morally acceptable in 2002</br>\n" +
                "<font color=\"#000000\"><strong>• </strong></font>Different apps and devices require different methods for accessing Unicode characters, but typically on a Mac you can access them via the special characters interface</br>\n" +
                "<font color=\"#000000\"><strong>• </strong></font>38% of Americans said gay and lesbian relations were morally acceptable in 2002</br>\n" +
                "<font color=\"#000000\"><strong>• </strong></font>110,000 characters, but aside from the full Latin alphabet, numerals and punctuation symbols, the most recognisable characters are probably</br>\n" +
                "<font color=\"#000000\"><strong>• </strong></font>38% of Americans said gay and lesbian relations were morally acceptable in 2002</br>\n" +
                "<p >Picking a second (or third) language to learn is a broad subject, and it’s ultimately a very personal choice. Perhaps you want to live somewhere new, or maybe you’re looking to explore business opportunities.</p></font>\n" +
                "<font color=\"#000000\"><p style=\"font-size:120%;color:black;\" >\"Learning a foreign language isn’t always easy, but if you pick the right language and take the time to learn, it can really pay off\"</p></font>";
            webView.LoadDataWithBaseURL(null, str, "text/html", "utf-8", null);
            webView.SetBackgroundColor(Color.Transparent);
            expand1.Title = "Complete cources for 12 doifferent languages";
            expand1.Detail = "No matter how difficult a challenge is, you are capable of completing it by using your exceptionally quick wits and tremendous";
            expand1.Init();
            //
            expand2.Title = "Practice with 60 million users worldwide";
            expand2.Detail = "No matter how difficult a challenge is, you are capable of completing it by using your exceptionally quick wits and tremendous";
            expand2.Init();
        }
        private void bindViews()
        {
            btnReadFull = (Button)FindViewById(Resource.Id.btnReadFull);
            webView = (WebView)FindViewById(Resource.Id.myWebview);
            expand1 = (MyExpandableLayout)FindViewById(Resource.Id.expand1);
            expand2 = (MyExpandableLayout)FindViewById(Resource.Id.expand2);

        }

        public override bool OnSupportNavigateUp()
        {
            ViewModel.GoBackCommand?.Execute();
            return base.OnSupportNavigateUp();
        }
    }
}