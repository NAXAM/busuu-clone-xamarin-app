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
using Java.Util;

namespace Naxam.Busuu.Droid.Profile.Views
{
    public class PremiumArrayAdapter : BaseAdapter<PremiumObject>
    {
        Activity context;
        List<PremiumObject> listPremiumItem;
        int layoutId;

        public PremiumArrayAdapter(Activity context, int resource, List<PremiumObject> objects)
        {
            this.context = context;
            this.listPremiumItem = objects;
            this.layoutId = resource;
        }

        public override PremiumObject this[int position] => listPremiumItem[position];

        public override int Count
        {
            get
            {
                return listPremiumItem.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = context.LayoutInflater;

            if (convertView == null)
            {
                convertView = inflater.Inflate(layoutId, null);
            }

            ImageView imPremiumIcon = convertView.FindViewById<ImageView>(Resource.Id.im_premium_icon);
            TextView tvPremiumDescripe = convertView.FindViewById<TextView>(Resource.Id.tv_premium_describe);

            imPremiumIcon.SetBackgroundResource((this[position]).iconId);
            tvPremiumDescripe.Text = (this[position]).describe;
            return convertView;
        }
    }

    public class PremiumObject
    {
        public int iconId { get; set; }
        public string describe { get; set; }
    }
}