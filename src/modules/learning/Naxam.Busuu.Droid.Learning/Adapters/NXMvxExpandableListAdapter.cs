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
using Naxam.Busuu.Learning.Models;
using MvvmCross.Binding.Droid.Views;
using Android.Graphics.Drawables;
using Android.Animation;
using Android.Util;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Droid.Core.Listener;

namespace Naxam.Busuu.Droid.Learning.Control
{
    public class NXMvxExpandableListAdapter : MvxExpandableListAdapter
    {
        Context context; IList<LessonModel> ItemSource
        {
            get => (IList<LessonModel>)ItemsSource;
        }
        public event EventHandler<int> DownloadClick;
        public event EventHandler<ExerciseClickEventArg> ExerciseClick;
        int[][] states = new int[][] {
                            new int[] { Android.Resource.Attribute.StateEnabled}, // enabled
                            new int[] {-Android.Resource.Attribute.StateEnabled}, // disabled
                           // new int[] {-Android.Resource.Attribute.StateChecked}, // unchecked
                           // new int[] { Android.Resource.Attribute.StatePressed}  // pressed
                        };
        public NXMvxExpandableListAdapter(Context context, IMvxAndroidBindingContext bindingContex) : base(context, bindingContex)
        {
            this.context = context;
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

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var view = base.GetChildView(groupPosition, childPosition, isLastChild, convertView, parent);
            ExerciesView item = view.FindViewById<ExerciesView>(Resource.Id.exerciseView);
            item.ExerciseClick += (s, e) =>
            {
                ExerciseClick?.Invoke(this, new ExerciseClickEventArg
                {
                    LessonIndex = groupPosition,
                    TopicIndex = childPosition,
                    Exercise = e.Exercise,
                    ExerciseIndex = e.ExerciseIndex
                });
            };
            if (isLastChild)
            {

                ((LinearLayout.LayoutParams)item.LayoutParameters).BottomMargin = (int)Util.Util.PxFromDp(view.Context, 56);
            }
            else
            {
                ((LinearLayout.LayoutParams)item.LayoutParameters).BottomMargin = (int)Util.Util.PxFromDp(view.Context, 24);
            }
            return view;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            LessonHeaderBackground LessonHeader = (LessonHeaderBackground)base.GetGroupView(groupPosition, isExpanded, convertView, parent);

            TextView txtLessonName = LessonHeader.FindViewById<TextView>(Resource.Id.txtLessonName);
            TextView txtLessonNumber = LessonHeader.FindViewById<TextView>(Resource.Id.txtLessonNumber);
            ImageView btnDownload = LessonHeader.FindViewById<ImageView>(Resource.Id.btnDownload);

            btnDownload.Tag = groupPosition;
            btnDownload.Clickable = true;
            btnDownload.Click -= BtnDownload_Click;
            btnDownload.Click += BtnDownload_Click;


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

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            ImageView image = (ImageView)sender;
            DownloadClick?.Invoke(this, (int)image.Tag);
        }

        bool busy;

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
    }


}