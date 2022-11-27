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
using Android.Util;

namespace Naxam.Busuu.Droid.Learning.Adapter
{
    public class LessonAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ClickHeader;
        Context context;
        IList<LessonModel> ItemSource;
        RecyclerView recyclerView;
        public LessonAdapter(Context context, IList<LessonModel> ItemSource, RecyclerView recyclerView)
        {
            this.context = context;
            this.ItemSource = ItemSource;
            this.recyclerView = recyclerView;
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
            LessonViewHolder viewHolder = (LessonViewHolder)holder;
            LessonModel lesson = ItemSource[position];
            viewHolder.txtLesson.Text = lesson.LessonNumber;
            viewHolder.txtTitle.Text = lesson.LessonName;
            viewHolder.circle_progress.Progress = lesson.Percent;
            viewHolder.circle_progress.FinishedColor = StringToColor(lesson.Color, 80);
            viewHolder.circle_progress.Background = Border(1, StringToColor(lesson.Color, 160));

            if (viewHolder.IsExpanded)
            {
                if (!viewHolder.LessonHeaderBackground.IsBusy)
                    viewHolder.LessonHeaderBackground.SetBackgroundColor(StringToColor(lesson.Color, 255));
                viewHolder.txtLesson.TextSize = 26;
                viewHolder.txtLesson.SetTextColor(Color.White);
                viewHolder.txtTitle.SetTextColor(Color.White);
            }
            else
            {
                if (!viewHolder.LessonHeaderBackground.IsBusy)
                    viewHolder.LessonHeaderBackground.SetBackgroundColor(Color.White);
                viewHolder.txtLesson.SetTextColor(Color.ParseColor("#3791CE"));
                viewHolder.txtTitle.SetTextColor(Color.ParseColor("#3791CE"));
                viewHolder.txtLesson.TextSize = 16;

            }
            viewHolder.RecyclerView.SetAdapter(new TopicAdapter(context, lesson.Topics));
            viewHolder.btnDownload.SetImageResource(viewHolder.IsExpanded ? Resource.Drawable.ic_download_white : Resource.Drawable.ic_download);
            viewHolder.LessonHeaderBackground.Click += (s, e) =>
            {
                viewHolder.LessonHeaderBackground.InitAnim(12, 12);
            };
            OnTouchListener touch = new OnTouchListener(viewHolder, lesson, () => { ClickHeader?.Invoke(viewHolder,position); });
            viewHolder.LessonHeaderBackground.SetOnClickListener(touch);
            viewHolder.LessonHeaderBackground.SetOnTouchListener(touch);
            viewHolder.btnDownload.Click += (s, e) =>
            {
                Toast.MakeText(context, lesson.LessonNumber, ToastLength.Short).Show();
            };
        }
        public class OnTouchListener : Java.Lang.Object, IOnTouchListener, IOnClickListener
        {
            Color BackgroundColor;
            Color SecondColor;
            Context context;
            bool expand, busy;
            TextView txtLesson, textView;
            LessonViewHolder viewHolder;
            RecyclerView recyclerView;
            LessonModel lesson;
            int position;
            Action click;
            public OnTouchListener(LessonViewHolder viewHolder, LessonModel lesson, Action click)
            {
                this.lesson = lesson;
                this.click = click;
                this.position = position;
                this.recyclerView = recyclerView;
                this.viewHolder = viewHolder;
                BackgroundColor = StringToColor(lesson.Color, 255);
                SecondColor = Color.White;
                ExpandableListView lsv;
                
            }
            float x, y;
            public void OnClick(View v)
            {
                click?.Invoke();
                context = v.Context;
                LessonHeaderBackground lessonHeaderBackground = (LessonHeaderBackground)v;
                // lesson.InitAnim(x, y);
                if (viewHolder.IsExpanded)
                {
                    var param = viewHolder.RecyclerView.LayoutParameters;
                    param.Height = 0;
                    viewHolder.RecyclerView.LayoutParameters = param;
                }
                else
                {
                    var param = viewHolder.RecyclerView.LayoutParameters;
                    param.Height = 0;
                    viewHolder.RecyclerView.Measure(MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.AtMost), MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));

                    viewHolder.RecyclerView.LayoutParameters = param;
                    param.Height = 360;
                    viewHolder.RecyclerView.LayoutParameters = param;
                }
                viewHolder.IsExpanded = !viewHolder.IsExpanded; 


                if (busy)
                    return;
                busy = true;
                textView = new TextView(context);
                txtLesson = lessonHeaderBackground.FindViewById<TextView>(Resource.Id.txtLesson);
                FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(0, 0);
                textView.SetX(x);
                textView.SetY(y);
                textView.LayoutParameters = layoutParams;
                PaintDrawable paint = new PaintDrawable(expand ? SecondColor : BackgroundColor);
                paint.Shape = new RectShape();
                paint.SetCornerRadius(1000);
                textView.Background = paint;
                ValueAnimator anim = ValueAnimator.OfInt(0, 20);
                ValueAnimator animText = ValueAnimator.OfInt(0, 10);
                int textSize = 16;
                animText.AddUpdateListener(new AnimatorUpdateListener(
                    (d2) =>
                    {
                        int val = (int)d2.AnimatedValue;
                        if (!expand)
                        {
                            textSize -= 1;
                        }
                        else
                        {
                            textSize += 1;
                        }
                        txtLesson.SetTextSize(ComplexUnitType.Dip, textSize);

                    }));
                animText.AddListener(new AnimatorListener
                {
                    AnimationStartHandle = (start) =>
                    {
                        if (expand)
                        {
                            textSize = 16;
                            txtLesson.SetTextSize(ComplexUnitType.Dip, 16);
                        }
                        else
                        {
                            textSize = 26;
                            txtLesson.SetTextSize(ComplexUnitType.Dip, 26);
                        }
                    },
                    AnimationEndHandle = (end) =>
                    {
                        
                    }
                });
                anim.AddUpdateListener(new AnimatorUpdateListener(
                    (d) =>
                    {
                        int val = (int)d.AnimatedValue;
                        FrameLayout.LayoutParams layoutParamsTV = (FrameLayout.LayoutParams)textView.LayoutParameters;
                        layoutParamsTV.Height += (int)Util.Util.PxFromDp(context, 20);
                        layoutParamsTV.Width += (int)Util.Util.PxFromDp(context, 20);
                        textView.SetX(textView.GetX() - (int)Util.Util.PxFromDp(context, 20) / 2);
                        textView.SetY(textView.GetY() - (int)Util.Util.PxFromDp(context, 20) / 2);
                        textView.LayoutParameters = layoutParams;


                    }));
                anim.AddListener(new AnimatorListener
                {
                    AnimationStartHandle = (start) =>
                    {
                        lessonHeaderBackground.AddView(textView, 0);
                        busy = true;
                    },
                    AnimationEndHandle = (end) =>
                    {
                        busy = false;
                        lessonHeaderBackground.RemoveView(textView);
                        lessonHeaderBackground.SetBackgroundColor(expand ? SecondColor : BackgroundColor);
                        expand = !expand;
                        animText.SetDuration(250);
                        animText.Start();
                    }
                });

                anim.SetDuration(250);
                anim.Start();




            }

            public bool OnTouch(View v, MotionEvent e)
            {
                x = e.GetX();
                y = e.GetY();
                return false;
            }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.layout_lesson_item, null);
            return new LessonViewHolder(view);
        }
    }
}