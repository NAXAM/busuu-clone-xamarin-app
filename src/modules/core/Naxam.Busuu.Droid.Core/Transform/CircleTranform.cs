using System;
using System.Text;
using Android.Content;
using Com.Bumptech.Glide.Load.Resource.Bitmap;
using Android.Graphics;
using Com.Bumptech.Glide.Load.Engine.Bitmap_recycle;
using Java.Security;

namespace Naxam.Busuu.Droid.Core.Transform
{
    public class CircleTransform : BitmapTransformation
    {
        public string Id
        {
            get { return typeof(CircleTransform).Name; }
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

        public override void UpdateDiskCacheKey(MessageDigest p0)
        {
            p0.Update(Encoding.UTF8.GetBytes(Id));
        }
    }
}