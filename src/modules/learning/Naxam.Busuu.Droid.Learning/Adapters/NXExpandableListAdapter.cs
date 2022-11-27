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
using MvvmCross.Binding.Droid.Views;
using Android.Graphics.Drawables;
using Android.Animation;
using Android.Util;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using MvvmCross.Binding.Droid.BindingContext;
using Com.Github.Lzyzsd.Circleprogress;
using Naxam.Busuu.Droid.Core.Listener;
using Naxam.Busuu.Learning.Models;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class NXExpandableListAdapter : BaseExpandableListAdapter
    {
        Context context; IList<LessonModel> ItemSource;
        public event EventHandler<int> DownloadClick;
        int[][] states = new int[][] {
                            new int[] { Android.Resource.Attribute.StateEnabled}, // enabled
                            new int[] {-Android.Resource.Attribute.StateEnabled}, // disabled
                           // new int[] {-Android.Resource.Attribute.StateChecked}, // unchecked
                           // new int[] { Android.Resource.Attribute.StatePressed}  // pressed
                        };
        public NXExpandableListAdapter(Context context, IList<LessonModel> ItemSource)
        {
            this.context = context;
            this.ItemSource = ItemSource;
        }

        public TopicModel ChildAt(int groupPosition, int childPosition)
        {
            return ItemSource.ElementAt(groupPosition).ElementAt(childPosition);
        }
        public event EventHandler<bool> DoneAnim;

        public LessonModel GroupAt(int groupPosition)
        {
            return ItemSource.ElementAt(groupPosition);
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            // LessonHeaderBackground LessonHeader = (LessonHeaderBackground)base.GetGroupView(groupPosition, isExpanded, convertView, parent);
            if (convertView == null)
            {
                convertView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.layout_lesson_header, null);
            }
            LessonHeaderBackground LessonHeader = (LessonHeaderBackground)convertView;
            TextView txtLessonName = LessonHeader.FindViewById<TextView>(Resource.Id.txtLessonName);
            TextView txtLessonNumber = LessonHeader.FindViewById<TextView>(Resource.Id.txtLessonNumber);
            ImageView btnDownload = LessonHeader.FindViewById<ImageView>(Resource.Id.btnDownload);
            LessonModel lesson = ItemSource.ElementAt(groupPosition);
            txtLessonNumber.Text = lesson.LessonNumber;
            txtLessonName.Text = lesson.LessonName;
            LessonHeader.BackgroundColor = Color.ParseColor(lesson.Color);
            CircleProgress progress = LessonHeader.FindViewById<CircleProgress>(Resource.Id.circle_progress);
            progress.Progress = lesson.Percent;

            Color color = Color.ParseColor(lesson.Color);
            var red = Color.GetRedComponent(color);
            var blue = Color.GetBlueComponent(color);
            var green = Color.GetGreenComponent(color);
            progress.FinishedColor = Color.Argb(80, red, green, blue);

            GradientDrawable drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetStroke((int)Util.Util.PxFromDp(context, 1), Color.Argb(160, red, green, blue));
            drawable.SetCornerRadius(Util.Util.PxFromDp(context, 1000));
            progress.Background = drawable;


            btnDownload.Click += (s, e) =>
            {
                DownloadClick?.Invoke(s, groupPosition);
            };
            if (isExpanded)
            {
                if (!busy)
                {
                    LessonHeader.SetBackgroundColor(LessonHeader.BackgroundColor);
                    txtLessonNumber.TextSize = 26;
                }

                txtLessonName.SetTextColor(Color.White);
                txtLessonNumber.SetTextColor(Color.White);
                btnDownload.SetImageResource(Resource.Drawable.ic_download_white);
            }
            else
            {
                if (!busy)
                {
                    txtLessonNumber.TextSize = 16;
                    LessonHeader.SetBackgroundColor(Color.White);
                }
                txtLessonName.SetTextColor(new Color(55, 145, 206));
                txtLessonNumber.SetTextColor(new Color(55, 145, 206));
                btnDownload.SetImageResource(Resource.Drawable.ic_download);
            }


            TouchClick touch = new TouchClick((e) =>
            {
                InitAnim(LessonHeader, e, isExpanded, groupPosition);
            });

            LessonHeader.SetOnClickListener(touch);
            LessonHeader.SetOnTouchListener(touch);

            return LessonHeader;
        }

        bool busy;

        public override int GroupCount => ItemSource.Count;

        public override bool HasStableIds => false;

        public void InitAnim(LessonHeaderBackground view, PositionClick e, bool expand, int position)
        {

            if (!expand)
            {
                DoneAnim?.Invoke(position, false);
            }
            if (busy)
                return;
            busy = true;
            TextView txtBackground = new TextView(view.Context);
            TextView txtLessonNumber = view.FindViewById<TextView>(Resource.Id.txtLessonNumber);
            TextView txtLessonName = view.FindViewById<TextView>(Resource.Id.txtLessonName);
            FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(0, 0);
            txtBackground.SetX(e.X);
            txtBackground.SetY(e.Y);
            txtBackground.LayoutParameters = layoutParams;
            PaintDrawable paint = new PaintDrawable(expand ? view.SecondColor : view.BackgroundColor);
            paint.Shape = new RectShape();
            paint.SetCornerRadius(1000);
            txtBackground.SetBackgroundDrawable(paint);
            view.SetBackgroundColor(!expand ? view.SecondColor : view.BackgroundColor);
            ValueAnimator backgroundAnim = ValueAnimator.OfInt(0, 100);
            ValueAnimator textSizeAnim = ValueAnimator.OfInt(0, 10);
            textSizeAnim.AddUpdateListener(new AnimatorUpdateListener((d) =>
            {

                if (expand)
                {
                    if (txtLessonNumber.TextSize > Util.Util.PxFromDp(context, 16))
                    {
                        System.Diagnostics.Debug.WriteLine("---------" + txtLessonNumber.TextSize + " ------------ " + Util.Util.DpFromPx(context, txtLessonNumber.TextSize));
                        txtLessonNumber.SetTextSize(ComplexUnitType.Sp, Util.Util.DpFromPx(context, txtLessonNumber.TextSize) - 1);

                    }

                }
                else
                {
                    if (txtLessonNumber.TextSize < Util.Util.PxFromDp(context, 26))
                    {
                        System.Diagnostics.Debug.WriteLine("+++++++++" + txtLessonNumber.TextSize + " ++++++++++++++++++ " + Util.Util.DpFromPx(context, txtLessonNumber.TextSize));
                        txtLessonNumber.SetTextSize(ComplexUnitType.Sp, Util.Util.DpFromPx(context, txtLessonNumber.TextSize) + 1);
                    }

                }
            }));
            backgroundAnim.AddUpdateListener(new AnimatorUpdateListener(
                (d) =>
                {
                    int val = (int)d.AnimatedValue;
                    layoutParams = (FrameLayout.LayoutParams)txtBackground.LayoutParameters;
                    int dp40 = (int)Util.Util.PxFromDp(context, 40);
                    layoutParams.Height += dp40;
                    layoutParams.Width += dp40;
                    txtBackground.SetX(txtBackground.GetX() - dp40 / 2);
                    txtBackground.SetY(txtBackground.GetY() - dp40 / 2);
                    txtBackground.LayoutParameters = layoutParams;
                    view.ForceLayout();
                    //if (expand)
                    //{
                    //    txtLessonNumber.SetTextSize(ComplexUnitType.Dip, Util.Util.DpFromPx(context, txtLessonName.TextSize) - 1);
                    //}
                    //else
                    //{
                    //    txtLessonNumber.SetTextSize(ComplexUnitType.Dip, Util.Util.DpFromPx(context, txtLessonName.TextSize) + 1);
                    //}
                }));
            textSizeAnim.AddListener(new AnimatorListener
            {
                AnimationEndHandle = (end) =>
                {

                    if (!expand)
                    {
                        txtLessonNumber.SetTextSize(ComplexUnitType.Sp, 26);
                    }
                    else
                    {
                        txtLessonNumber.SetTextSize(ComplexUnitType.Sp, 16);
                    }
                }

            });
            backgroundAnim.AddListener(new AnimatorListener
            {
                AnimationStartHandle = (start) =>
                {
                    view.AddView(txtBackground, 0);
                    busy = true;
                },
                AnimationEndHandle = (end) =>
                {
                    busy = false;
                    view.RemoveView(txtBackground);
                    view.SetBackgroundColor(expand ? view.SecondColor : view.BackgroundColor);
                    // txtLessonName.SetTextColor(!expand ? view.SecondColor : view.BackgroundColor);
                    if (!expand)
                    {
                        // txtLessonNumber.SetTextSize(ComplexUnitType.Dip, 26);
                    }
                    else
                    {
                        //txtLessonNumber.SetTextSize(ComplexUnitType.Dip, 16);
                        DoneAnim?.Invoke(position, true);
                    }
                    textSizeAnim.SetDuration(250);
                    textSizeAnim.StartDelay = 200;
                    textSizeAnim.Start();
                }
            });
            backgroundAnim.SetDuration(100);
            backgroundAnim.Start();
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return 0;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return ItemSource.ElementAt(groupPosition).Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            TopicModel topic = ItemSource.ElementAt(groupPosition).ElementAt(childPosition);
            if (convertView == null)
            {
                convertView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.layout_topic_item, null);
            }
            TextView txtTopic = convertView.FindViewById<TextView>(Resource.Id.txtTopic);
            TextView txtTime = convertView.FindViewById<TextView>(Resource.Id.txtTime);
            //NXRecyclerView recycerExercise = convertView.FindViewById<NXRecyclerView>(Resource.Id.recyclerExercise);
            txtTopic.Text = topic.Toppic;
            txtTime.Text = topic.Time + topic.Time > 1 ? "minutes" : "minute";
            return convertView;

        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return null;
        }

        public override long GetGroupId(int groupPosition)
        {
            return 0;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return false;
        }
    }
    public class ClickListener : Java.Lang.Object, View.IOnClickListener
    {
        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }
    }
    public class TouchClick : Java.Lang.Object, View.IOnTouchListener, View.IOnClickListener
    {
        public Action<PositionClick> Clicked;
        PositionClick pos;
        public TouchClick(Action<PositionClick> click)
        {
            Clicked = click;
        }

        public void OnClick(View v)
        {
            Clicked?.Invoke(pos);
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            this.pos = new PositionClick
            {
                X = e.GetX(),
                Y = e.GetY()
            };
            return false;
        }
    }

    public class PositionClick
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}