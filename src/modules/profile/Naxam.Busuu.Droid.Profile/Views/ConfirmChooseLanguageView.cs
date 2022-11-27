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
using static Android.Views.View; 
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Droid.Profile.Views
{
    public class ConfirmChooseLanguageView : MvxDialogFragment
    {
        public ConfirmChooseLanguageView(LanguageModel language)
        {
            this.language = language;
        }

        LanguageModel language;
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.EnsureBindingContextSet(savedInstanceState);
            var dialog = new ConfirmChooseLanguageDialog(Activity, language);

            return dialog;
        }
    }

    public class ConfirmChooseLanguageDialog : Dialog
    {
        Context context;
        LanguageModel language;
        public ConfirmChooseLanguageDialog(Context c, LanguageModel language) : base(c)
        {
            context = c;
            this.language = language;
        }

        public bool Confirm { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.dialog_confirm_choose_language);
            Button btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
            btnCancel.Click += (s, e) =>
            {
                Confirm = false;
                this.Dismiss();
            };
            Button btnContinue = FindViewById<Button>(Resource.Id.btnContinue);
            btnContinue.Click += (s, e) =>
            {
                Confirm = true;
                this.Dismiss();
            };
            ImageView imgFlag = FindViewById<ImageView>(Resource.Id.imgFlag);
            TextView txtContent = FindViewById<TextView>(Resource.Id.txtContent);
            txtContent.Text = string.Format("Looks like you speak {0} already, are you sure you want to LEARN this language ?", language.Language);
        }
    }
}