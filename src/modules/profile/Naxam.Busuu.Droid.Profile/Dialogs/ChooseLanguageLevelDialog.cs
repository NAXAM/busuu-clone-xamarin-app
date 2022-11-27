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
using Android.Graphics.Drawables;
using Android.Graphics;
using Naxam.Busuu.Droid.Core.Utils;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Droid.Profile.Dialogs
{
    public class ChooseLanguageLevelDialog : Dialog
    {
        public ChooseLanguageLevelDialog(Context context) : base(context)
        {

        }

        TextView txtTitle, txtLevel, txtAccept, txtCancel;
        View viewBeginner, viewIntermediate, viewAdvance, viewNative;

        public bool Accept { get; private set; }

        public LanguageLevel Level { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.RequestFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.choose_language_level_layout);

            Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
            Window.SetLayout(ActionBar.LayoutParams.MatchParent, ActionBar.LayoutParams.WrapContent);
            Window.SetGravity(GravityFlags.Center);

            txtCancel = FindViewById<TextView>(Resource.Id.btnCancel);
            txtAccept = FindViewById<TextView>(Resource.Id.btnAccept);
            txtTitle = FindViewById<TextView>(Resource.Id.txtTitle);
            txtLevel = FindViewById<TextView>(Resource.Id.txtLevel);
            viewBeginner = FindViewById(Resource.Id.viewBeginner);
            viewIntermediate = FindViewById(Resource.Id.viewIntermediate);
            viewAdvance = FindViewById(Resource.Id.viewAdvance);
            viewNative = FindViewById(Resource.Id.viewNative);
            Level = LanguageLevel.Beginner;
            viewBeginner.Click += ViewBeginner_Click;
            viewIntermediate.Click += ViewIntermediate_Click;
            viewAdvance.Click += ViewAdvance_Click;
            viewNative.Click += ViewNative_Click;
            txtCancel.Click += (s, e) => { Dismiss(); };
            txtAccept.Click += (s, e) =>
            {
                Accept = true;
                Dismiss();
            };
            SetLayoutViewSelected(viewBeginner);
        }

        public override void Dismiss()
        {
            viewBeginner.Click -= ViewBeginner_Click;
            viewIntermediate.Click -= ViewIntermediate_Click;
            viewAdvance.Click -= ViewAdvance_Click;
            viewNative.Click -= ViewNative_Click;
            base.Dismiss();
        }

        private void ViewNative_Click(object sender, EventArgs e)
        {
            int width = (int)Util.PxFromDp(Context, 24);
            switch (Level)
            {
                case LanguageLevel.Native:

                    return;
                case LanguageLevel.Beginner:
                    SetLayoutViewUnselected(viewBeginner);
                    break;
                case LanguageLevel.Intermediate:
                    SetLayoutViewUnselected(viewIntermediate);
                    break;
                case LanguageLevel.Advanced:
                    SetLayoutViewUnselected(viewAdvance);
                    break;
            }
            Level = LanguageLevel.Native;
            SetLayoutViewSelected(viewNative);
        }

        private void ViewAdvance_Click(object sender, EventArgs e)
        {
            int width = (int)Util.PxFromDp(Context, 24);
            switch (Level)
            {
                case LanguageLevel.Advanced:

                    return;
                case LanguageLevel.Beginner:
                    SetLayoutViewUnselected(viewBeginner);
                    break;
                case LanguageLevel.Intermediate:
                    SetLayoutViewUnselected(viewIntermediate);
                    break;
                case LanguageLevel.Native:
                    SetLayoutViewUnselected(viewNative);
                    break;
            }
            Level = LanguageLevel.Advanced;
            SetLayoutViewSelected(viewAdvance);
        }

        private void ViewIntermediate_Click(object sender, EventArgs e)
        {
            int width = (int)Util.PxFromDp(Context, 24);
            switch (Level)
            {
                case LanguageLevel.Intermediate:

                    return;
                case LanguageLevel.Beginner:
                    SetLayoutViewUnselected(viewBeginner);
                    break;
                case LanguageLevel.Advanced:
                    SetLayoutViewUnselected(viewAdvance);
                    break;
                case LanguageLevel.Native:
                    SetLayoutViewUnselected(viewNative);
                    break;
            }
            Level = LanguageLevel.Intermediate;
            SetLayoutViewSelected(viewIntermediate);
        }

        void SetLayoutViewUnselected(View view)
        {
            int width = (int)Util.PxFromDp(Context, 12);
            var param = view.LayoutParameters;
            param.Width = width;
            param.Height = width;
            view.LayoutParameters = param;
            view.SetBackgroundResource(Resource.Drawable.shape_language_level);
        }

        void SetLayoutViewSelected(View view)
        {
            txtLevel.Text = Level.ToString();
            int width = (int)Util.PxFromDp(Context, 24);
            var param = view.LayoutParameters;
            param.Width = width;
            param.Height = width;
            view.LayoutParameters = param;
            view.SetBackgroundResource(Resource.Drawable.shape_language_level_selected);

        }

        private void ViewBeginner_Click(object sender, EventArgs e)
        {
            int width = (int)Util.PxFromDp(Context, 24);
            switch (Level)
            {
                case LanguageLevel.Beginner:
                    return;
                case LanguageLevel.Intermediate:
                    SetLayoutViewUnselected(viewIntermediate);
                    break;
                case LanguageLevel.Advanced:
                    SetLayoutViewUnselected(viewAdvance);
                    break;
                case LanguageLevel.Native:
                    SetLayoutViewUnselected(viewNative);
                    break;
            }
            Level = LanguageLevel.Beginner;

            SetLayoutViewSelected(viewBeginner);
        }
    }
}