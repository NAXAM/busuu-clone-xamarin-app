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

namespace Naxam.Busuu.Droid.Learning.Adapter
{
    public class TopicViewHolder : RecyclerView.ViewHolder
    {
        public TextView Topic;
        public TextView Time;
        public RecyclerView Exercise;

        public TopicViewHolder(View view):base(view)
        {
            Topic = view.FindViewById<TextView>(Resource.Id.txtTopic);
            Time = view.FindViewById<TextView>(Resource.Id.txtTime);
            Exercise = view.FindViewById<RecyclerView>(Resource.Id.recyclerExercise);
        }
    }
}