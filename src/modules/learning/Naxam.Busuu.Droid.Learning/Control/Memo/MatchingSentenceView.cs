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
using Android.Graphics;
using Naxam.Busuu.Learning.Models;

namespace Naxam.Busuu.Droid.Learning.Control.Memo
{
    public class MatchingSentenceView : MemoBaseView, View.IOnTouchListener
    {
        public override event EventHandler<int> NextClick;
        Dictionary<string, string> MatchingSentence;
        TextView txt01Move, txt02Move, txt03Move, txt01, txt02, txt03, txt04, txt05, txt06, txtGuide;


        float xMove01, yMove01;
        float xMove02, yMove02;
        float xMove03, yMove03;

        float xTxt04, yTxt04;
        float xTxt05, yTxt05;
        float xTxt06, yTxt06;

        Button btnContinue;
        bool firstTouchMove01, firstTouchMove02, firstTouchMove03, firstTouchTxt04, firstTouchTxt05, firstTouchTxt06, correct = true;

        ViewGroup _root;
        Rect rectTxt04, rectTxt05, rectTxt06, rectMove01, rectMove02, rectMove03;
        private float _xDelta;
        private float _yDelta;

        public MatchingSentenceView(Context context, UnitModel unit) : base(context)
        {
            Item = unit;
            Init(context);
        }



        private void Init(Context context)
        {
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.matching_sentence_layout, null);

            MatchingSentence = new Dictionary<string, string>();
            for (int i = 0; i < Item.Inputs.Count; i++)
            {
                MatchingSentence.Add(Item.Answers[i].Text, Item.Inputs[i]);
            }

            //
            rectTxt04 = new Rect();
            rectTxt05 = new Rect();
            rectTxt06 = new Rect();
            rectMove01 = new Rect();
            rectMove02 = new Rect();
            rectMove03 = new Rect();
            //
            firstTouchMove01 = false;
            firstTouchMove02 = false;
            firstTouchMove03 = false;
            firstTouchTxt04 = false;
            firstTouchTxt05 = false;
            firstTouchTxt06 = false;
            _root = view.FindViewById<RelativeLayout>(Resource.Id.root);
            //
            txtGuide = view.FindViewById<TextView>(Resource.Id.txtGuide);
            txtGuide.Text = Item.Title;

            btnContinue = view.FindViewById<Button>(Resource.Id.btnContinue);
            btnContinue.Visibility = ViewStates.Gone;
            btnContinue.Click += (s, e) =>
            {
                NextClick?.Invoke(btnContinue, correct ? 1 : 0);
            };
            //
            txt01Move = view.FindViewById<TextView>(Resource.Id.txt01Move);
            txt02Move = view.FindViewById<TextView>(Resource.Id.txt02Move);
            txt03Move = view.FindViewById<TextView>(Resource.Id.txt03Move);
            //
            float elevation = Util.Util.PxFromDp(Context, 2);
            txt01Move.Elevation = elevation;
            txt02Move.Elevation = elevation;
            txt03Move.Elevation = elevation;
            //
            txt03 = view.FindViewById<TextView>(Resource.Id.txt03);
            txt02 = view.FindViewById<TextView>(Resource.Id.txt02);
            txt01 = view.FindViewById<TextView>(Resource.Id.txt01);
            txt04 = view.FindViewById<TextView>(Resource.Id.txt04);
            txt05 = view.FindViewById<TextView>(Resource.Id.txt05);
            txt06 = view.FindViewById<TextView>(Resource.Id.txt06);

            string key01, key02, key03, val01, val02, val03;
            Random random = new Random();
            //
            var listempKey = MatchingSentence.Keys.ToList();
            key01 = MatchingSentence.Keys.ElementAt(random.Next(0, 3));
            listempKey.Remove(key01);
            key02 = listempKey.ElementAt(random.Next(0, 2));
            listempKey.Remove(key02);
            key03 = listempKey[0];
            //
            val01 = MatchingSentence.Values.ElementAt(random.Next(0, 3));

            var listemp = MatchingSentence.Values.ToList();
            listemp.Remove(val01);
            val02 = listemp.ElementAt(random.Next(0, 2));
            listemp.Remove(val02);
            val03 = listemp[0];
            //
            // above: values
            // below: keys
            //
            txt01.Text = val01;
            txt02.Text = val02;
            txt03.Text = val03;
            //
            txt01Move.Text = val01;
            txt02Move.Text = val02;
            txt03Move.Text = val03;
            //
            txt04.Text = key01;
            txt05.Text = key02;
            txt06.Text = key03;
            //
            txt01Move.SetOnTouchListener(this);
            txt02Move.SetOnTouchListener(this);
            txt03Move.SetOnTouchListener(this);

            AddView(view, new ViewGroup.LayoutParams(-1, -1));
        }
        private void ChangeColorByValue(Rect rect, TextView fixedTxtView)// remembering pass extactly
        {
            // getting a current textview collising with 04, 05, 06
            TextView txtViewMove = getTxtMoveCollision(rect);
            if (MatchingSentence[fixedTxtView.Text] == txtViewMove.Text)
            {
                txtViewMove.SetBackgroundColor(Color.ParseColor("#74B825"));// true
            }
            else
            {
                correct = false;
                txtViewMove.SetBackgroundColor(Color.ParseColor("#EB4431"));// false
            }



        }

        private TextView getTxtMoveCollision(Rect rect)
        {
            if (rect.Intersect(rectMove01)) return txt01Move;
            if (rect.Intersect(rectMove02)) return txt02Move;
            if (rect.Intersect(rectMove03)) return txt03Move;
            return null;
        }
        private bool finishLesson()
        {
            return hasCollision040506(rectMove01) && hasCollision040506(rectMove02) && hasCollision040506(rectMove03);
        }
        private TextView getShortestDistanceRect(Rect rect)
        {
            double[] arr = new double[3];
            double SmallestDistance;
            //
            int xRect = rect.CenterX();
            int yRect = rect.CenterY();
            //
            int xRect04 = rectTxt04.CenterX();
            int yRect04 = rectTxt04.CenterY();
            //
            int xRect05 = rectTxt05.CenterX();
            int yRect05 = rectTxt05.CenterY();
            //
            int xRect06 = rectTxt06.CenterX();
            int yRect06 = rectTxt06.CenterY();

            double dis04 = getDistance(xRect, yRect, xRect04, yRect04);
            //
            double dis05 = getDistance(xRect, yRect, xRect05, yRect05);
            //
            double dis06 = getDistance(xRect, yRect, xRect06, yRect06);
            //
            arr[0] = dis04;
            arr[1] = dis05;
            arr[2] = dis06;
            //
            SmallestDistance = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {

                if (arr[i] < SmallestDistance)
                    SmallestDistance = arr[i];
            }
            if (SmallestDistance == arr[0])
            {

                return txt04;

            }
            if (SmallestDistance == arr[1])
            {

                return txt05;
            }
            if (SmallestDistance == arr[2])
            {

                return txt06;
            }

            return null;
        }
        private void setDefaultBackground()
        {
            txt04.SetBackgroundResource(Resource.Color.colorNormalBtn);
            txt05.SetBackgroundResource(Resource.Color.colorNormalBtn);
            txt06.SetBackgroundResource(Resource.Color.colorNormalBtn);
        }
        private void disableView()
        {
            txt01Move.Enabled = false;
            txt02Move.Enabled = false;
            txt03Move.Enabled = false;
            txt01Move.SetTextColor(Color.White);
            txt02Move.SetTextColor(Color.White);
            txt03Move.SetTextColor(Color.White);
        }
        private double getDistance(int x1, int y1, int x2, int y2)
        {
            double dis;
            dis = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            return dis;

        }
        private void hasCollisionMoveRect(Rect rect, View view)
        {
            // view chính là 1 rectMove

            if (rect.Intersect(rectMove01) && view.Id != txt01Move.Id)
            {
                moveToCoordinates(xMove01, yMove01, txt01Move);

            }
            if (rect.Intersect(rectMove02) && view.Id != txt02Move.Id)
            {
                moveToCoordinates(xMove02, yMove02, txt02Move);

            }
            if (rect.Intersect(rectMove03) && view.Id != txt03Move.Id)
            {
                moveToCoordinates(xMove03, yMove03, txt03Move);

            }

        }
        private void changeBackgroundColor(Rect rect)
        {
            if (rectTxt04.Intersect(rect))
            {
                txt04.SetBackgroundResource(Resource.Color.colorHoverBtn);
                txt05.SetBackgroundResource(Resource.Color.colorNormalBtn);
                txt06.SetBackgroundResource(Resource.Color.colorNormalBtn);


            }
            else if (rectTxt05.Intersect(rect))
            {
                txt04.SetBackgroundResource(Resource.Color.colorNormalBtn);
                txt05.SetBackgroundResource(Resource.Color.colorHoverBtn);
                txt06.SetBackgroundResource(Resource.Color.colorNormalBtn);

            }
            else if (rectTxt06.Intersect(rect))
            {
                txt04.SetBackgroundResource(Resource.Color.colorNormalBtn);
                txt05.SetBackgroundResource(Resource.Color.colorNormalBtn);
                txt06.SetBackgroundResource(Resource.Color.colorHoverBtn);

            }
        }
        private bool hasCollision040506(Rect rect)
        {

            if (rectTxt04.Intersect(rect) || rectTxt05.Intersect(rect) || rectTxt06.Intersect(rect))
                return true;
            return false;
        }
        private void getRectange(TextView txt04, TextView txt05, TextView txt06, TextView txt01Move, TextView txt02Move, TextView txt03Move)
        {
            txt04.GetHitRect(rectTxt04);
            txt05.GetHitRect(rectTxt05);

            txt06.GetHitRect(rectTxt06);

            txt01Move.GetHitRect(rectMove01);

            txt02Move.GetHitRect(rectMove02);

            txt03Move.GetHitRect(rectMove03);


        }
        private void getCoordinates()
        {

            if (firstTouchTxt04 == false)
            {
                xTxt04 = txt04.GetX();
                yTxt04 = txt04.GetY();
                firstTouchTxt04 = true;
            }



            if (firstTouchTxt05 == false)
            {
                xTxt05 = txt05.GetX();
                yTxt05 = txt05.GetY();
                firstTouchTxt05 = true;
            }



            if (firstTouchTxt06 == false)
            {
                xTxt06 = txt06.GetX();
                yTxt06 = txt06.GetY();
                firstTouchTxt06 = true;
            }

            if (firstTouchMove01 == false)
            {
                xMove01 = txt01Move.GetX();
                yMove01 = txt01Move.GetY();
                firstTouchMove01 = true;
            }


            else
            if (firstTouchMove02 == false)
            {
                xMove02 = txt02Move.GetX();
                yMove02 = txt02Move.GetY();
                firstTouchMove02 = true;
            }



            if (firstTouchMove03 == false)
            {
                xMove03 = txt03Move.GetX();
                yMove03 = txt03Move.GetY();
                firstTouchMove03 = true;
            }

        }
        private void moveWithCollision(Rect rect, View view)
        {
            if (getShortestDistanceRect(rect) != null)
            {
                if (getShortestDistanceRect(rect).Id == Resource.Id.txt04)
                {
                    hasCollisionMoveRect(rectTxt04, view);


                    view.Animate()
                            .X(xTxt04)
                            .Y(yTxt04)
                            .SetDuration(0)
                            .Start();
                }
                if (getShortestDistanceRect(rect).Id == Resource.Id.txt05)
                {

                    hasCollisionMoveRect(rectTxt05, view);
                    view.Animate()
                            .X(xTxt05)
                            .Y(yTxt05)
                            .SetDuration(0)
                            .Start();
                }
                if (getShortestDistanceRect(rect).Id == Resource.Id.txt06)
                {
                    hasCollisionMoveRect(rectTxt06, view);

                    view.Animate()
                            .X(xTxt06)
                            .Y(yTxt06)
                            .SetDuration(0)
                            .Start();
                }
            }
        }
        private void moveToCoordinates(float x, float y, View view)
        {
            if (x != view.GetX() || y != view.GetY())
            {
                view.Animate()
                        .X(x)
                        .Y(y)
                        .SetDuration(0)
                        .Start();
            }

        }
        public bool OnTouch(View view, MotionEvent motionEvent)
        {
            getCoordinates();
            getRectange(txt04, txt05, txt06, txt01Move, txt02Move, txt03Move);

            switch (motionEvent.Action)
            {

                case MotionEventActions.Down:

                    _xDelta = view.GetX() - motionEvent.RawX;
                    _yDelta = view.GetY() - motionEvent.RawY;

                    break;

                case MotionEventActions.Up:
                    txt01Move.SetBackgroundColor(Color.White);
                    txt02Move.SetBackgroundColor(Color.White);
                    txt03Move.SetBackgroundColor(Color.White);
                    if (view.Id == Resource.Id.txt01Move)
                    {// touching move 1
                        if (hasCollision040506(rectMove01) == false)
                        {
                            moveToCoordinates(xMove01, yMove01, view);
                        }
                        else
                        {
                            if (xMove01 != view.GetX() || yMove01 != view.GetY())
                            {
                                moveWithCollision(rectMove01, view);
                            }

                        }
                    }
                    else if (view.Id == Resource.Id.txt02Move)
                    {// touching move 2
                        if (hasCollision040506(rectMove02) == false)
                        {
                            moveToCoordinates(xMove02, yMove02, view);
                        }

                        if (hasCollision040506(rectMove02) == true)
                        {
                            if (xMove02 != view.GetX() || yMove02 != view.GetY())
                            {
                                moveWithCollision(rectMove02, view);
                            }
                        }


                    }
                    else if (view.Id == Resource.Id.txt03Move)
                    { // touching mvoe3
                        if (hasCollision040506(rectMove03) == false)
                        {
                            moveToCoordinates(xMove03, yMove03, view);
                        }
                        else
                        { // incase of hasing a collision
                            if (xMove03 != view.GetX() || yMove03 != view.GetY())
                            {
                                moveWithCollision(rectMove03, view);
                            }
                        }
                    }
                    if (finishLesson())
                    {
                        disableView();
                        //
                        ChangeColorByValue(rectTxt04, txt04);
                        ChangeColorByValue(rectTxt05, txt05);
                        ChangeColorByValue(rectTxt06, txt06);
                        //
                        txtGuide.Visibility = ViewStates.Gone;
                        btnContinue.BringToFront();
                        btnContinue.Visibility = ViewStates.Visible;
                        //Toast.MakeText(this, "Done nhé thảo đẹp trai!", ToastLength.Long).Show();
                    }
                    break;

                case MotionEventActions.Move:
                    if (view.Id == Resource.Id.txt01Move)
                    {// touching move 1
                        txt01Move.BringToFront();
                        txt01Move.SetBackgroundColor(new Color(255, 255, 255, 178));

                        if (hasCollision040506(rectMove01) == false)
                        {
                            setDefaultBackground();

                        }
                        if (hasCollision040506(rectMove01) == true)
                        {
                            changeBackgroundColor(rectMove01);

                        }

                    }
                    else if (view.Id == Resource.Id.txt02Move)
                    {// touching move 2
                        txt02Move.BringToFront();
                        txt02Move.SetBackgroundColor(new Color(255, 255, 255, 178));
                        if (hasCollision040506(rectMove02) == false)
                        {
                            setDefaultBackground();

                        }

                        if (hasCollision040506(rectMove02) == true)
                        {
                            changeBackgroundColor(rectMove02);
                        }


                    }
                    else if (view.Id == Resource.Id.txt03Move)
                    { // touching mvoe3
                        txt03Move.BringToFront();
                        txt03Move.SetBackgroundColor(new Color(255, 255, 255, 178));
                        if (hasCollision040506(rectMove03) == false)
                        {
                            setDefaultBackground();

                        }
                        if (hasCollision040506(rectMove03) == true)
                        {
                            changeBackgroundColor(rectMove03);
                        }

                    }
                    view.Animate()
                            .X(motionEvent.RawX + _xDelta)
                            .Y(motionEvent.RawY + _yDelta)
                            .SetDuration(0)
                            .Start();
                    break;
                default:
                    return false;
            }
            return true;

        }
    }
}