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
using Android.Support.V7.Widget;
using Com.Bumptech.Glide;
using Naxam.Busuu.Droid.Core.Transform;
using Com.Iarcuschin.Simpleratingbar;
using Android.Graphics;
using Naxam.Busuu.Droid.Social.Controls;
using Naxam.Busuu.Droid.Core.Utils;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Droid.Core.Controls;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Social.Adapter
{
    public class FriendsAdapter : RecyclerView.Adapter
    {
        IList<SocialModel> Items;
        public FriendsAdapter(IList<SocialModel> Items)
        {
            this.Items = Items;
        }
        public override int ItemCount
        {
            get
            {
                return Items.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            FriendsViewHolder viewHolder = (FriendsViewHolder)holder;
            viewHolder.BindData(Items[position]);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.FromContext(parent.Context).Inflate(Resource.Layout.friends_item, parent, false);
            return new FriendsViewHolder(view);
        }
    }

    public class FriendsViewHolder : RecyclerView.ViewHolder
    {
        PlayerSocial btnPlay;
        Context context;
        ImageView imgAvatar, imgLanguage;
        TextView txtName, txtLocation, txtContent, txtPosted, txtFeedBack;
        public FriendsViewHolder(View itemView) : base(itemView)
        {
            context = itemView.Context;
            imgAvatar = itemView.FindViewById<ImageView>(Resource.Id.imgAvatar);
            imgLanguage = itemView.FindViewById<ImageView>(Resource.Id.imgLanguage);
            txtName = itemView.FindViewById<TextView>(Resource.Id.txtName);
            txtLocation = itemView.FindViewById<TextView>(Resource.Id.txtLocation);
            txtContent = itemView.FindViewById<TextView>(Resource.Id.txtContent);
            txtPosted = itemView.FindViewById<TextView>(Resource.Id.txtPosted);
            txtFeedBack = itemView.FindViewById<TextView>(Resource.Id.txtFeedBack);
            SimpleRatingBar rating = itemView.FindViewById<SimpleRatingBar>(Resource.Id.ratingBar);
            btnPlay = itemView.FindViewById<PlayerSocial>(Resource.Id.btnPlayer);
            rating.Enabled = false;
            rating.Indicator = true;
        }

        public void BindData(SocialModel social)
        {
            var options = new RequestOptions()
                                .Transform(new CircleTransform(context));
            Glide.With(context).Load(social.User.Photo).Apply(options).Into(imgAvatar);
            Glide.With(context).Load(Util.GetDrawableResourceIdByName(context, social.User.Languages[0].HalfFlag)).Into(imgLanguage);
            txtName.Text = social.User.Name;
            txtLocation.Text = social.User.Country.Country;
            if (social.Type == SocialModel.SocialType.Writing)
            {
                txtContent.Visibility = ViewStates.Visible;
                btnPlay.Visibility = ViewStates.Gone;
                txtContent.Text = social.Content;
            }
            else
            {
                btnPlay.Visibility = ViewStates.Visible;
                btnPlay.AudioPath = social.Content;
                txtContent.Visibility = ViewStates.Gone;
            }
            txtPosted.Text = "Posted " + Util.GetTextDate(social.DatePosted);
        }


    }
}