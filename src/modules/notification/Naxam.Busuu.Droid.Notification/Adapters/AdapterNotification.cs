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
using Android.Text;
using Android.Text.Style;
using Com.Bumptech.Glide;
using Naxam.Busuu.Droid.Core.Transform;
//using Naxam.Busuu.Notification.Models;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Droid.Notification.Adapters
{
    public class AdapterNotification : RecyclerView.Adapter
    {
        //private IList<NotificationModelBase> listNotify;

        //public AdapterNotification(IList<NotificationModelBase> listNotify)
        //{
        //    this.listNotify = listNotify;
        //}
        //public override int ItemCount => listNotify.Count;

        //public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        //{
        //    String name = listNotify[position].NameUser;
        //    SpannableStringBuilder sb = new SpannableStringBuilder(name + " has asked you to correct their excise");

        //     StyleSpan bss = new StyleSpan(Android.Graphics.TypefaceStyle.Bold); // Span to make text bold
        //    sb.SetSpan(bss, 0, name.Length, SpanTypes.InclusiveInclusive); 

        //    ((RecyclerViewHolder)holder).txtNotify.TextFormatted = sb;
        //}

        //public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        //{
        //    LayoutInflater inflater = LayoutInflater.From(parent.Context);
        //    View itemview = inflater.Inflate(Resource.Layout.item_notification, parent, false);
        //    return new AdapterNotification.RecyclerViewHolder(itemview);
        //}

        //public class RecyclerViewHolder : RecyclerView.ViewHolder
        //{
        //    public RelativeLayout mRelative;
        //    public TextView txtNotify, txtTimePost;
        //    public ImageView imgAvatar;

        //public RecyclerViewHolder(View itemView): base(itemView)
        //{
        //    txtNotify = (TextView)itemView.FindViewById(Resource.Id.txtNotify);
        //    txtTimePost = (TextView)itemView.FindViewById(Resource.Id.txtTimePost);
        //    imgAvatar = (ImageView)itemView.FindViewById(Resource.Id.imgAvatar);
        //     Glide.With(itemView.Context).Load("http://media.phunutoday.vn/files/tho_nguyen/2017/05/31/ngoc-trinh-4-1429-phunutoday.jpg").Transform(new CircleTransform(itemView.Context)).Into(imgAvatar);

        //    }

        //}
        public override int ItemCount => throw new NotImplementedException();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }
    }
}