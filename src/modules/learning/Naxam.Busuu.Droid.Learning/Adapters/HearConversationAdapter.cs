using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Views;
using Android.Widget;
using Naxam.Busuu.Droid.Learning.Models;
using Com.Bumptech.Glide;
using Android.Views.Animations;
using Android.Graphics;
using Com.Bumptech.Glide.Load.Resource.Bitmap;
using Android.Content;
using Com.Bumptech.Glide.Load.Engine.Bitmap_recycle;

namespace Naxam.Busuu.Droid.Learning.Adapters
{
    public class HearConversationAdapter : BaseAdapter<Conversation>
    {
        Activity context;
        List<ConversationSentence> objects;
        int layoutId;

        public HearConversationAdapter(Activity context, int resource, List<ConversationSentence> objects)
        {
            this.context = context;
            this.objects = objects;
            this.layoutId = resource;
        }

        public override Conversation this[int position] => throw new NotImplementedException();

        public override int Count
        {
            get
            {
                return objects.Count;
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
            ImageView imAvata = convertView.FindViewById<ImageView>(Resource.Id.conversation_im_avata);
            TextView tvName = convertView.FindViewById<TextView>(Resource.Id.conversation_tv_name);
            TextView tvSentence = convertView.FindViewById<TextView>(Resource.Id.conversation_tv_sentence);

            Glide.With(context).Load(objects[position].AvataImage).Transform(new CircleTransform(context)).Into(imAvata);
            tvName.Text = objects[position].PersonalName;
            tvSentence.Text = objects[position].Sentence;
            return convertView;
        }
    }

    public class CircleTransform : BitmapTransformation
    {
        public override string Id
        {
            get { return  typeof(CircleTransform).Name; }
        }

        public CircleTransform(Context context) : base(context)
        {
        }

        protected override Bitmap Transform(IBitmapPool p0, Bitmap p1, int p2, int p3)
        {
            return CircleCrop(p0, p1);
        }
        private static Bitmap CircleCrop(IBitmapPool pool, Bitmap source)
        {
            if (source == null) return null;

            int size = Math.Min(source.Width, source.Height);
            int x = (source.Width - size) / 2;
            int y = (source.Height - size) / 2;

            // TODO this could be acquired from the pool too
            Bitmap squared = Bitmap.CreateBitmap(source, x, y, size, size);

            Bitmap result = pool.Get(size, size, Bitmap.Config.Argb8888);
            if (result == null)
            {
                result = Bitmap.CreateBitmap(size, size, Bitmap.Config.Argb8888);
            }

            Canvas canvas = new Canvas(result);
            Paint paint = new Paint();
            paint.SetShader(new BitmapShader(squared, BitmapShader.TileMode.Clamp, BitmapShader.TileMode.Clamp));
            paint.AntiAlias = (true);
            float r = size / 2f;
            canvas.DrawCircle(r, r, r, paint);
            return result;
        }
    }
}