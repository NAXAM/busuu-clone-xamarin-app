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
using Android.Support.V4.View;
using Java.Lang; 
using Com.Bumptech.Glide;
using Naxam.Busuu.Droid.Core.Transform;
using Naxam.Busuu.Droid.Core.Utils;
using Naxam.Busuu.Droid.Social.Controls;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Droid.Core.Controls;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Social.Adapter
{
    public class DiscoverAdapter : PagerAdapter
    {
        public event EventHandler<int> ItemPositionClick;
        IList<SocialModel> Items;
        public DiscoverAdapter(IList<SocialModel> Items)
        {
            this.Items = Items;
        }
        public override int Count
        {
            get
            {
                return Items.Count;
            }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == (LinearLayout)@object;
        }



        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            Context context = container.Context;
            View itemView = LayoutInflater.FromContext(container.Context).Inflate(Resource.Layout.discover_item, container, false);
            ImageView imgAvatar = itemView.FindViewById<ImageView>(Resource.Id.imgAvatar);
            ImageView imgLanguage = itemView.FindViewById<ImageView>(Resource.Id.imgLanguage);
            TextView txtName = itemView.FindViewById<TextView>(Resource.Id.txtName);
            TextView txtLocation = itemView.FindViewById<TextView>(Resource.Id.txtLocation);
            TextView txtContent = itemView.FindViewById<TextView>(Resource.Id.txtContent);
            TextView txtLanguage = itemView.FindViewById<TextView>(Resource.Id.txtLanguage);
            LinearLayout layoutSpeak = itemView.FindViewById<LinearLayout>(Resource.Id.layoutSpeak);
            Button btnView = itemView.FindViewById<Button>(Resource.Id.btnView);
            PlayerSocial btnPlay = itemView.FindViewById<PlayerSocial>(Resource.Id.btnPlayer);
            SocialModel social = Items[position];
            txtName.Text = social.User.Name;
            txtLocation.Text = social.User.Country.Country;
            txtLanguage.Text = social.User.Languages[0].Language;
            txtContent.Text = social.Content;
            if (social.Type == SocialModel.SocialType.Writing)
            {
                txtContent.Visibility = ViewStates.Visible;
                btnPlay.Visibility = ViewStates.Gone;
            }
            else
            {
                txtContent.Visibility = ViewStates.Gone;
                btnPlay.Visibility = ViewStates.Visible;
                btnPlay.AudioPath = "http://app4e.net/9176438991583737843.mp3";
            }
            int widthImage = (int)Util.PxFromDp(context, 16);
            foreach (var item in social.User.SpeakLanguages)
            {
                ImageView imgView = new ImageView(context);
                var param = new LinearLayout.LayoutParams(widthImage, widthImage);
                param.LeftMargin = widthImage / 6;
                Glide.With(context).Load(Util.GetDrawableResourceIdByName(context, item.Flag)).Into(imgView);
                layoutSpeak.AddView(imgView, param);
            }
            Glide.With(context).Load(Util.GetDrawableResourceIdByName(context, social.User.Languages[0].Flag)).Into(imgLanguage);

            var options = new RequestOptions()
                .Transform(new CircleTransform(context));
            Glide.With(context).Load(social.User.Photo).Apply(options).Into(imgAvatar);
            container.AddView(itemView, new ViewGroup.LayoutParams(-1, -2));
            itemView.Click += ItemView_Click;
            btnView.Click += BtnView_Click;
            itemView.Tag = position;
            btnView.Tag = position;
            return itemView;
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            View itemView = (View)sender;
            ItemPositionClick?.Invoke(this, (int)itemView.Tag);
        }

        private void ItemView_Click(object sender, EventArgs e)
        {
            View itemView = (View)sender;
            ItemPositionClick?.Invoke(this, (int)itemView.Tag);
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            ((View)@object).Click -= ItemView_Click;
            Button btnView = ((View)@object).FindViewById<Button>(Resource.Id.btnView);
            btnView.Click -= BtnView_Click;
            container.RemoveView((LinearLayout)@object);
        }
    }
}