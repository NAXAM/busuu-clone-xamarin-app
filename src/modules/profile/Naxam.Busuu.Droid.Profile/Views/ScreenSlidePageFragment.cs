using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Naxam.Busuu.Droid.Profile.Models;

namespace Naxam.Busuu.Droid.Profile.Views
{
    public class ScreenSlidePageFragment : Fragment
    {
        StartPageModel obj;
        public ScreenSlidePageFragment(StartPageModel obj)
        {
            this.obj = obj;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewGroup rootView = (ViewGroup)inflater.Inflate(Resource.Layout.fragment_screen_slide_page, container, false);
            var tvTitle = rootView.FindViewById<TextView>(Resource.Id.tv_title);
            var tvContent = rootView.FindViewById<TextView>(Resource.Id.tv_content);

            tvTitle.Text = obj.title;
            tvContent.Text = obj.content;
            return rootView;
        }
    }
}