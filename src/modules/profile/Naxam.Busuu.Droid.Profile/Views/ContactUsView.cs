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
using MvvmCross.Droid.Views;
using Android.Graphics.Drawables;
using Android.Graphics;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Profile.ViewModels;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "Contact Us",Theme ="@style/AppTheme")]
    public class ContactUsView : MvxAppCompatActivity<ContactUsViewModel>
    {
        GradientDrawable shape;
        Button btnSend;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SetContentView(Resource.Layout.activity_contact_us);
            shape = new GradientDrawable();
            shape.SetColor(Color.ParseColor("#38A8F5"));
            shape.SetShape(ShapeType.Rectangle);
            shape.SetCornerRadius(1000);
            btnSend = (Button)FindViewById(Resource.Id.btnSend);
            btnSend.Background=shape;
        }

        public override bool OnSupportNavigateUp()
        {
            ViewModel.GoBackCommand?.Execute();
            return base.OnSupportNavigateUp();
        }
    }
}