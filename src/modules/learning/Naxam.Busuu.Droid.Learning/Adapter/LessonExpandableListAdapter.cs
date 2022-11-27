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
using Java.Lang;
using Naxam.Busuu.Learning.Model;
using Com.Github.Lzyzsd.Circleprogress;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace Naxam.Busuu.Droid.Learning.Adapter
{
    public class LessonExpandableListAdapter : BaseExpandableListAdapter
    {
        Context context;
        IList<LessonModel> ItemSource;
        public LessonExpandableListAdapter(Context context, IList<LessonModel> ItemSource)
        {
            this.context = context;
            this.ItemSource = ItemSource;
        }
        public override int GroupCount => ItemSource.Count;

        public override bool HasStableIds => false;

        public TopicModel ChildAt(int groupPosition, int childPosition)
        {
            return ItemSource.ElementAt(groupPosition).Topics.ElementAt(childPosition);
        }
        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return ChildAt(groupPosition, childPosition).Id;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return ItemSource.ElementAt(groupPosition).Topics.Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            return new TextView(convertView.Context);
        }

        public LessonModel GroupAt(int groupPosition)
        {
            return ItemSource.ElementAt(groupPosition);
        }
        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return null;
        }

        public override long GetGroupId(int groupPosition)
        {
            return GroupAt(groupPosition).Id;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.layout_lesson_header, null);
            }
            TextView txtLesson = convertView.FindViewById<TextView>(Resource.Id.txtLesson);
            TextView txtTitle = convertView.FindViewById<TextView>(Resource.Id.txtTitle);
            CircleProgress circle_progress = convertView.FindViewById<CircleProgress>(Resource.Id.circle_progress);
            ImageView btnDownload = convertView.FindViewById<ImageView>(Resource.Id.btnDownload);
            LessonModel lesson = GroupAt(groupPosition);
            txtLesson.Text = lesson.Name;
            txtTitle.Text = lesson.Title;
            circle_progress.Progress = lesson.Percent;
            circle_progress.FinishedColor = StringToColor(lesson.Color, 80);
            circle_progress.Background = Border(1, StringToColor(lesson.Color, 160));
            convertView.Click += (s, e) =>
            {
                Toast.MakeText(context, lesson.Name, ToastLength.Short).Show();
            };
            return convertView;
        }

        private Drawable Border(int border, Color color)
        {
            GradientDrawable drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetStroke((int)Util.Util.PxFromDp(context, border), color);
            drawable.SetCornerRadius(Util.Util.PxFromDp(context, 1000));
            return drawable;
        }

        private Color StringToColor(string _color, int alpha)
        {
            Color color = Color.ParseColor((string)_color);
            var red = Color.GetRedComponent(color);
            var blue = Color.GetBlueComponent(color);
            var green = Color.GetGreenComponent(color);
            return Color.Argb(alpha, red, green, blue);
        }
        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }


}
