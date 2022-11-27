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
using static Android.Resource;
using Android.Views.Animations;
using Naxam.Busuu.Learning.Models;
using Android.Content.Res;
using Naxam.Busuu.Droid.Learning.Control.Memo;

namespace Naxam.Busuu.Droid.Learning.Control.Vocabulary
{
    public class PreparePronounceView : MemoBaseView
    {
        private bool isClickStar;
        ImageView imgPlayBtn, imgStarBtn;
         
        public PreparePronounceView(Context context, UnitModel Item) : base(context)
        {
            this.Item = Item;
            Init(context);
        }

        protected override void OnConfigurationChanged(Configuration newConfig)
        {
            Init(Context);
            base.OnConfigurationChanged(newConfig);
        }

        private void Init(Context context)
        {
            RemoveAllViews();
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.PreparePronounce, null);
            imgStarBtn = view.FindViewById<ImageView>(Resource.Id.imgStar);
            imgStarBtn.SetBackgroundResource(Resource.Drawable.star_white);
            view.Tag = "1";
            imgStarBtn.Click += (s, e) =>
            {
                if (isClickStar == false)
                {
                    imgStarBtn.SetBackgroundResource(Resource.Drawable.ic_yellow_star);
                }
                else
                {
                    imgStarBtn.SetBackgroundResource(Resource.Drawable.star_white);
                }
                isClickStar = !isClickStar;

            };
            AddView(view, new ViewGroup.LayoutParams(-1, -1));
        }
    }
}