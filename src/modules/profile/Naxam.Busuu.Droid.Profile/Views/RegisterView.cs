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
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Views.Animations;
using Android.Content.Res;
using Android.Views.InputMethods;
using Android.Text;
using static Android.Widget.TextView;
using Android.Text.Method;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "Create Account")]
    public class RegisterView : MvxAppCompatActivity
    {
        EditText edtEmail, edtUserName, edtPassword, edtPhone, edtPhoneCode;
        TextView txtPolicy;
        LinearLayout layoutPhone, layoutSocial;
        Button btnRegister;
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.RegisterActivity);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            Button btnButtonUsePhoneEmail = FindViewById<Button>(Resource.Id.btnButtonUsePhoneEmail);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
            edtUserName = FindViewById<EditText>(Resource.Id.edtUserName);
            edtPassword = FindViewById<EditText>(Resource.Id.edtPassword);
            edtPhone = FindViewById<EditText>(Resource.Id.edtPhone);
            layoutPhone = FindViewById<LinearLayout>(Resource.Id.layoutPhone);
            layoutSocial = FindViewById<LinearLayout>(Resource.Id.layoutSocial);

            edtPhoneCode = FindViewById<EditText>(Resource.Id.edtPhoneCode);
            txtPolicy = FindViewById<TextView>(Resource.Id.txtPolicy);
            txtPolicy.MovementMethod = LinkMovementMethod.Instance;
            layoutPhone.SetBackgroundResource(Resource.Drawable.underline_background_focus);
            btnRegister.Focusable = true;
            btnRegister.FocusableInTouchMode = true;
            btnRegister.RequestFocus();
            Android.Text.ISpanned sp;
            string text = "By joining I declare that I have read and I accept the <a href='http://zing.vn'>Terms & Conditions</a> and the <a href='http://zing.vn'>Privacy Policy</a>";
            if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                sp = Html.FromHtml(text, FromHtmlOptions.ModeLegacy);
            }
            else
            {
                sp = Html.FromHtml(text);
            }
           
            txtPolicy.SetText(sp, BufferType.Spannable);

            //animation
            Animation fadeIn = new AlphaAnimation(0, 1);
            fadeIn.Duration = 200;
            Animation moveLefttoRight = new TranslateAnimation(100, 0, 0, 0);
            moveLefttoRight.Duration = 200;
            moveLefttoRight.FillAfter = true;


            AnimationSet animation = new AnimationSet(false);
            animation.AddAnimation(fadeIn);
            animation.AddAnimation(moveLefttoRight);

            edtPhone.FocusChange += (s, e) =>
            {
                if (e.HasFocus)
                {
                    layoutPhone.SetBackgroundResource(Resource.Drawable.underline_background_focus);
                    layoutSocial.Visibility = ViewStates.Gone;
                }
                else
                {
                    layoutPhone.SetBackgroundResource(Resource.Drawable.underline_background_normal);
                    layoutSocial.Visibility = ViewStates.Visible;
                }
            };

            edtEmail.FocusChange += EdtEmail_FocusChange;
            edtUserName.FocusChange += EdtEmail_FocusChange;
            edtPassword.FocusChange += EdtEmail_FocusChange;
            edtPhone.FocusChange += EdtEmail_FocusChange;
            edtPhoneCode.FocusChange += EdtEmail_FocusChange;
            btnButtonUsePhoneEmail.Click += (s, e) =>
            {
                if (edtEmail.Visibility == ViewStates.Gone)
                {
                    edtEmail.StartAnimation(animation);
                    edtEmail.Visibility = ViewStates.Visible;
                    layoutPhone.Visibility = ViewStates.Gone;
                    btnButtonUsePhoneEmail.Text = "Use Phone";
                    edtEmail.RequestFocus();
                }
                else
                {
                    edtEmail.Visibility = ViewStates.Gone;
                    layoutPhone.StartAnimation(animation);
                    layoutPhone.Visibility = ViewStates.Visible;
                    btnButtonUsePhoneEmail.Text = "Use Email";
                    edtPhone.RequestFocus();
                }

            };

        }

        private void EdtEmail_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (edtPhoneCode.HasFocus || edtUserName.HasFocus || edtPhone.HasFocus || edtPassword.HasFocus || edtEmail.HasFocus)
            {
                layoutSocial.Visibility = ViewStates.Gone;
            }
            else
            {
                layoutSocial.Visibility = ViewStates.Visible;
                InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(edtPassword.WindowToken, 0);
            }
        }
    }
}