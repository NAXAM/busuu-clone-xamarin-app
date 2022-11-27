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
using Naxam.Busuu.Droid.Learning.Control;
using Com.Github.Lzyzsd.Circleprogress;

namespace Naxam.Busuu.Droid.Learning.Adapter
{
    public class LessonViewHolder : RecyclerView.ViewHolder
    {
        public Naxam.Busuu.Droid.Learning.Control.LessonHeaderBackground LessonHeaderBackground;
        public TextView txtLesson, txtTitle;
        public RecyclerView RecyclerView;
        public CircleProgress circle_progress;
        public ImageView btnDownload;
        public bool IsExpanded;
        public LessonViewHolder(View view) : base(view)
        {
            txtLesson = view.FindViewById<TextView>(Resource.Id.txtLesson);
            txtTitle = view.FindViewById<TextView>(Resource.Id.txtTitle);
            circle_progress = view.FindViewById<CircleProgress>(Resource.Id.circle_progress);
            btnDownload = view.FindViewById<ImageView>(Resource.Id.btnDownload);
            LessonHeaderBackground = view.FindViewById<LessonHeaderBackground>(Resource.Id.lessonHeaderBackground);
            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
        }
    }
}