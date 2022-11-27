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
using Naxam.Busuu.Droid.Core.Utils;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Droid.Core.Controls;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Profile.Adapter
{
    public class CorrectionAdapter : RecyclerView.Adapter
    {
        IList<SocialModel> Items;
        int type;
        public CorrectionAdapter(IList<SocialModel> Items, int type)
        {
            this.Items = Items;
            this.type = type;
        }
        public override int ItemCount
        {
            get
            {
                return Items.Count + 1;
            }
        }

        public override int GetItemViewType(int position)
        {
            if (position == 0)
            {
                return 0;
            }
            return 1;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (position == 0)
            {
                HeaderViewHolder viewHolder = (HeaderViewHolder)holder;
                string header = type == 0 ? string.Format("Completed {0} conversation exercises", ItemCount - 1) : string.Format("Corrected {0} exercise{1} of other users", ItemCount - 1, ItemCount - 1 > 2 ? "s" : "");
                viewHolder.BindData(header);
            }
            else
            {
                CorrectionViewHolder viewHolder = (CorrectionViewHolder)holder;
                viewHolder.BindData(Items[position - 1]);
            }

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == 0)
            {
                View view = LayoutInflater.FromContext(parent.Context).Inflate(Resource.Layout.header_item, parent, false);
                return new HeaderViewHolder(view);
            }
            else
            {
                View view = LayoutInflater.FromContext(parent.Context).Inflate(Resource.Layout.correction_item, parent, false);
                return new CorrectionViewHolder(view);
            }
        }
    }

    public class HeaderViewHolder : RecyclerView.ViewHolder
    {
        TextView txtHeader;
        public HeaderViewHolder(View view) : base(view)
        {
            txtHeader = view.FindViewById<TextView>(Resource.Id.txtHeader);
        }

        public void BindData(string data)
        {
            txtHeader.Text = data;
        }
    }

    public class CorrectionViewHolder : RecyclerView.ViewHolder
    {
        PlayerSocial btnPlay;
        Context context;
        ImageView imgAvatar, imgLanguage;
        TextView txtName, txtLocation, txtContent, txtPosted, txtFeedBack;
        public CorrectionViewHolder(View itemView) : base(itemView)
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