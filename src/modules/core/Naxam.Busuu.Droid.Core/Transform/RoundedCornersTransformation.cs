using Android.Content;
using Com.Bumptech.Glide.Load.Resource.Bitmap;
using Android.Graphics;
using Com.Bumptech.Glide.Load.Engine.Bitmap_recycle;
using Naxam.Busuu.Droid.Core.Utils;
using Java.Security;
using System.Text;

namespace Naxam.Busuu.Droid.Core.Transform
{
    public class RoundedCornersTransformation : BitmapTransformation
    {

        public enum CornerType
        {
            All,
            Top_Left, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT,
            TOP, BOTTOM, LEFT, RIGHT,
            OTHER_TOP_LEFT, OTHER_TOP_RIGHT, OTHER_BOTTOM_LEFT, OTHER_BOTTOM_RIGHT,
            DIAGONAL_FROM_TOP_LEFT, DIAGONAL_FROM_TOP_RIGHT
        }


        private int mRadius;
        private int mDiameter;
        private int mMargin;
        private CornerType mCornerType;

        public string Id => "RoundedTransformation(radius=" + mRadius + ", margin=" + mMargin + ", diameter="
        + mDiameter + ", cornerType=" + mCornerType.ToString() + ")";

        public RoundedCornersTransformation(Context context, int radius, int margin, CornerType cornerType) : base(context)
        {
            mRadius = (int)Util.PxFromDp(context, radius);
            mDiameter = mRadius * 2;
            mMargin = (int)Util.PxFromDp(context, margin);
            mCornerType = cornerType;
        }

        protected override Bitmap Transform(IBitmapPool mBitmapPool, Bitmap source, int outWidth, int outHeight)
        {
            int width = source.Width;
            int height = source.Height;
            Bitmap bitmap = mBitmapPool.Get(width, height, Bitmap.Config.Argb8888);
            if (bitmap == null)
            {
                bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
            }
            Canvas canvas = new Canvas(bitmap);
            Paint paint = new Paint();
            paint.AntiAlias = true;
            paint.SetShader(new BitmapShader(source, Shader.TileMode.Clamp, Shader.TileMode.Clamp));
            DrawRoundRect(canvas, paint, width, height);
            return BitmapResource.Obtain(bitmap, mBitmapPool).Get();
        }

        private void DrawRoundRect(Canvas canvas, Paint paint, float width, float height)
        {
            float right = width - mMargin;
            float bottom = height - mMargin;

            switch (mCornerType)
            {
                case CornerType.All:
                    canvas.DrawRoundRect(new RectF(mMargin, mMargin, right, bottom), mRadius, mRadius, paint);
                    break;
                case CornerType.Top_Left:
                    DrawTopLeftRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.TOP_RIGHT:
                    DrawTopRightRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.BOTTOM_LEFT:
                    DrawBottomLeftRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.BOTTOM_RIGHT:
                    drawBottomRightRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.TOP:
                    drawTopRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.BOTTOM:
                    drawBottomRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.LEFT:
                    drawLeftRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.RIGHT:
                    drawRightRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.OTHER_TOP_LEFT:
                    drawOtherTopLeftRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.OTHER_TOP_RIGHT:
                    drawOtherTopRightRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.OTHER_BOTTOM_LEFT:
                    drawOtherBottomLeftRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.OTHER_BOTTOM_RIGHT:
                    drawOtherBottomRightRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.DIAGONAL_FROM_TOP_LEFT:
                    drawDiagonalFromTopLeftRoundRect(canvas, paint, right, bottom);
                    break;
                case CornerType.DIAGONAL_FROM_TOP_RIGHT:
                    drawDiagonalFromTopRightRoundRect(canvas, paint, right, bottom);
                    break;
                default:
                    canvas.DrawRoundRect(new RectF(mMargin, mMargin, right, bottom), mRadius, mRadius, paint);
                    break;
            }
        }

        private void DrawTopLeftRoundRect(Canvas canvas, Paint paint, float right, float bottom)
        {
            canvas.DrawRoundRect(new RectF(mMargin, mMargin, mMargin + mDiameter, mMargin + mDiameter),
                mRadius, mRadius, paint);
            canvas.DrawRect(new RectF(mMargin, mMargin + mRadius, mMargin + mRadius, bottom), paint);
            canvas.DrawRect(new RectF(mMargin + mRadius, mMargin, right, bottom), paint);
        }

        private void DrawTopRightRoundRect(Canvas canvas, Paint paint, float right, float bottom)
        {
            canvas.DrawRoundRect(new RectF(right - mDiameter, mMargin, right, mMargin + mDiameter), mRadius,
                mRadius, paint);
            canvas.DrawRect(new RectF(mMargin, mMargin, right - mRadius, bottom), paint);
            canvas.DrawRect(new RectF(right - mRadius, mMargin + mRadius, right, bottom), paint);
        }

        private void DrawBottomLeftRoundRect(Canvas canvas, Paint paint, float right, float bottom)
        {
            canvas.DrawRoundRect(new RectF(mMargin, bottom - mDiameter, mMargin + mDiameter, bottom),
                mRadius, mRadius, paint);
            canvas.DrawRect(new RectF(mMargin, mMargin, mMargin + mDiameter, bottom - mRadius), paint);
            canvas.DrawRect(new RectF(mMargin + mRadius, mMargin, right, bottom), paint);
        }

        private void drawBottomRightRoundRect(Canvas canvas, Paint paint, float right, float bottom)
        {
            canvas.DrawRoundRect(new RectF(right - mDiameter, bottom - mDiameter, right, bottom), mRadius,
                mRadius, paint);
            canvas.DrawRect(new RectF(mMargin, mMargin, right - mRadius, bottom), paint);
            canvas.DrawRect(new RectF(right - mRadius, mMargin, right, bottom - mRadius), paint);
        }

        private void drawTopRoundRect(Canvas canvas, Paint paint, float right, float bottom)
        {
            canvas.DrawRoundRect(new RectF(mMargin, mMargin, right, mMargin + mDiameter), mRadius, mRadius,
                paint);
            canvas.DrawRect(new RectF(mMargin, mMargin + mRadius, right, bottom), paint);
        }

        private void drawBottomRoundRect(Canvas canvas, Paint paint, float right, float bottom)
        {
            canvas.DrawRoundRect(new RectF(mMargin, bottom - mDiameter, right, bottom), mRadius, mRadius,
                paint);
            canvas.DrawRect(new RectF(mMargin, mMargin, right, bottom - mRadius), paint);
        }

        private void drawLeftRoundRect(Canvas canvas, Paint paint, float right, float bottom)
        {
            canvas.DrawRoundRect(new RectF(mMargin, mMargin, mMargin + mDiameter, bottom), mRadius, mRadius,
                paint);
            canvas.DrawRect(new RectF(mMargin + mRadius, mMargin, right, bottom), paint);
        }

        private void drawRightRoundRect(Canvas canvas, Paint paint, float right, float bottom)
        {
            canvas.DrawRoundRect(new RectF(right - mDiameter, mMargin, right, bottom), mRadius, mRadius,
                paint);
            canvas.DrawRect(new RectF(mMargin, mMargin, right - mRadius, bottom), paint);
        }

        private void drawOtherTopLeftRoundRect(Canvas canvas, Paint paint, float right, float bottom)
        {
            canvas.DrawRoundRect(new RectF(mMargin, bottom - mDiameter, right, bottom), mRadius, mRadius,
                paint);
            canvas.DrawRoundRect(new RectF(right - mDiameter, mMargin, right, bottom), mRadius, mRadius,
                paint);
            canvas.DrawRect(new RectF(mMargin, mMargin, right - mRadius, bottom - mRadius), paint);
        }

        private void drawOtherTopRightRoundRect(Canvas canvas, Paint paint, float right, float bottom)
        {
            canvas.DrawRoundRect(new RectF(mMargin, mMargin, mMargin + mDiameter, bottom), mRadius, mRadius,
                paint);
            canvas.DrawRoundRect(new RectF(mMargin, bottom - mDiameter, right, bottom), mRadius, mRadius,
                paint);
            canvas.DrawRect(new RectF(mMargin + mRadius, mMargin, right, bottom - mRadius), paint);
        }

        private void drawOtherBottomLeftRoundRect(Canvas canvas, Paint paint, float right, float bottom)
        {
            canvas.DrawRoundRect(new RectF(mMargin, mMargin, right, mMargin + mDiameter), mRadius, mRadius,
                paint);
            canvas.DrawRoundRect(new RectF(right - mDiameter, mMargin, right, bottom), mRadius, mRadius,
                paint);
            canvas.DrawRect(new RectF(mMargin, mMargin + mRadius, right - mRadius, bottom), paint);
        }

        private void drawOtherBottomRightRoundRect(Canvas canvas, Paint paint, float right,
            float bottom)
        {
            canvas.DrawRoundRect(new RectF(mMargin, mMargin, right, mMargin + mDiameter), mRadius, mRadius,
                paint);
            canvas.DrawRoundRect(new RectF(mMargin, mMargin, mMargin + mDiameter, bottom), mRadius, mRadius,
                paint);
            canvas.DrawRect(new RectF(mMargin + mRadius, mMargin + mRadius, right, bottom), paint);
        }

        private void drawDiagonalFromTopLeftRoundRect(Canvas canvas, Paint paint, float right,
            float bottom)
        {
            canvas.DrawRoundRect(new RectF(mMargin, mMargin, mMargin + mDiameter, mMargin + mDiameter),
                mRadius, mRadius, paint);
            canvas.DrawRoundRect(new RectF(right - mDiameter, bottom - mDiameter, right, bottom), mRadius,
                mRadius, paint);
            canvas.DrawRect(new RectF(mMargin, mMargin + mRadius, right - mDiameter, bottom), paint);
            canvas.DrawRect(new RectF(mMargin + mDiameter, mMargin, right, bottom - mRadius), paint);
        }

        private void drawDiagonalFromTopRightRoundRect(Canvas canvas, Paint paint, float right,
            float bottom)
        {
            canvas.DrawRoundRect(new RectF(right - mDiameter, mMargin, right, mMargin + mDiameter), mRadius,
                mRadius, paint);
            canvas.DrawRoundRect(new RectF(mMargin, bottom - mDiameter, mMargin + mDiameter, bottom),
                mRadius, mRadius, paint);
            canvas.DrawRect(new RectF(mMargin, mMargin, right - mRadius, bottom - mRadius), paint);
            canvas.DrawRect(new RectF(mMargin + mRadius, mMargin + mRadius, right, bottom), paint);
        }

        public override void UpdateDiskCacheKey(MessageDigest p0)
        {
            p0.Update(Encoding.UTF8.GetBytes(Id));
        }
    }
}