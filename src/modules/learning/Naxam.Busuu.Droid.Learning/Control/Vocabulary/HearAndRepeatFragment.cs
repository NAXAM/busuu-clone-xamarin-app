using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V4.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using static Android.Views.Animations.Animation;
using Android.Views.Animations;
using Android.Graphics;
using Android.Animation;
using Naxam.Busuu.Learning.Models;

namespace Naxam.Busuu.Droid.Learning.Control.Vocabulary
{
    public class HearAndRepeatFragment : BaseFragment
    {
        public override event EventHandler<int> NextClick;
        ImageView imgMic, hiddenCircle, imgPlayBtn;
        GradientDrawable clikedShape;
        GradientDrawable UnclikedShape;
        TextView txtGuide;
        bool isClick;

        public HearAndRepeatFragment(UnitModel item)
        {
            this.Item = item;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.HearAndRepeat, container, false);
            Init(view);
            return view;

        }

        private void Init(View view)
        {
            imgPlayBtn = view.FindViewById<ImageView>(Resource.Id.imgPlayBtn);

            imgPlayBtn.Click += (s, e) =>
            {

            };
            hiddenCircle = (ImageView)view.FindViewById(Resource.Id.hiddenCircle);

            ObjectAnimator anim = ObjectAnimator.OfFloat(hiddenCircle, "Scale", 1.5f, 1f);
            anim.RepeatMode = ValueAnimatorRepeatMode.Reverse;
            anim.RepeatCount = 1000000000;
            anim.SetDuration(200);

            imgMic = (ImageView)view.FindViewById(Resource.Id.imgMic);
            txtGuide = (TextView)view.FindViewById(Resource.Id.txtGuide);

            clikedShape = new GradientDrawable();
            clikedShape.SetShape(ShapeType.Rectangle);
            clikedShape.SetCornerRadius(1000);
            clikedShape.SetColor(Color.Red);
            //
            UnclikedShape = new GradientDrawable();
            UnclikedShape.SetShape(ShapeType.Rectangle);
            UnclikedShape.SetCornerRadius(10000);
            UnclikedShape.SetColor(Color.ParseColor("#38A9F6"));
            imgMic.Clickable = true;
            // setting drawable for views
            hiddenCircle.Background = UnclikedShape;

            imgMic.Click += (s, e) =>
            {
                if (isClick == false)
                {
                    imgPlayBtn.Enabled = false;
                    txtGuide.Visibility = ViewStates.Invisible;
                    imgMic.Background = clikedShape;
                    anim.Start();
                    isClick = !isClick;
                }
                else
                {
                    anim.Cancel();
                    imgPlayBtn.Enabled = true;
                    txtGuide.Visibility = ViewStates.Visible;
                    imgMic.Background = UnclikedShape;
                    isClick = !isClick;
                    NextClick?.Invoke(this, 1);
                };

            };

        }
    }
}