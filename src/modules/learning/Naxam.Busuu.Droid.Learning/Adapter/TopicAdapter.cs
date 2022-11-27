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
using Naxam.Busuu.Learning.Model;
using Android.Graphics;
using Android.Graphics.Drawables;
using static Android.Views.View;
using Naxam.Busuu.Droid.Learning.Control;
using Android.Graphics.Drawables.Shapes;
using Android.Animation;

namespace Naxam.Busuu.Droid.Learning.Adapter
{
    public class TopicAdapter : RecyclerView.Adapter
    {
        Context context;
        IList<TopicModel> ItemSource;
        public TopicAdapter(Context context, IList<TopicModel> ItemSource)
        {
            this.context = context;
            this.ItemSource = ItemSource;
        }
        public static Color StringToColor(string _color, int alpha)
        {
            Color color = Color.ParseColor((string)_color);
            var red = Color.GetRedComponent(color);
            var blue = Color.GetBlueComponent(color);
            var green = Color.GetGreenComponent(color);
            return Color.Argb(alpha, red, green, blue);
        }
        private Drawable Border(int border, Color color)
        {
            GradientDrawable drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetStroke((int)Util.Util.PxFromDp(context, border), color);
            drawable.SetCornerRadius(Util.Util.PxFromDp(context, 1000));
            return drawable;
        }
        public override int ItemCount => ItemSource.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            TopicModel topic = ItemSource[position];
            TopicViewHolder viewHolder = (TopicViewHolder)holder;
            viewHolder.Topic.Text = topic.Toppic;
            viewHolder.Time.Text = topic.Time + "Minutes";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.layout_topic_item, null);
            return new LessonViewHolder(view);
        }
    }
}