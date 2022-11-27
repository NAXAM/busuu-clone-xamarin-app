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
using Android.Support.V7.App;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity]
    public class ProfileInputView : MvxAppCompatActivity
    {
        private LinearLayout layoutInput;
        private LinearLayout layoutGender;

        private EditText edtInput;
        private ListView lvCountry;

        private Button btnCancel; 
        private RadioButton rbtnMale;
        private RadioButton rbtnFemale;
        private RadioButton rbtnUndisclosed;
        private RadioGroup rbtnGenderGroup;

        List<string> listCountry;
        string input = "";
        string country = "";

        int REQUEST_CODE_CHANGE_DATA = 1;

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.profile_input);
            btnCancel = FindViewById<Button>(Resource.Id.bt_cancel);

            layoutInput = FindViewById<LinearLayout>(Resource.Id.layout_profile_input_text);

            layoutGender = FindViewById<LinearLayout>(Resource.Id.layout_input_gender);
            lvCountry = FindViewById<ListView>(Resource.Id.lv_input_language);
            edtInput = FindViewById<EditText>(Resource.Id.txt_input_text);
            rbtnMale = FindViewById<RadioButton>(Resource.Id.rbt_male);
            rbtnFemale = FindViewById<RadioButton>(Resource.Id.rbt_female);
            rbtnUndisclosed = FindViewById<RadioButton>(Resource.Id.rbt_undisclosed);
            rbtnGenderGroup = FindViewById<RadioGroup>(Resource.Id.rbt_gender_group);
            lvCountry.ItemsCanFocus = true;

            listCountry = new List<string>();
            //create lits country 
            for (int i = 0; i < 20; i++)
            {
                listCountry.Add("Country " + i);
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, listCountry);
            lvCountry.Adapter = adapter;

            Intent intentResult = new Intent();

            if (Intent.HasExtra("ProfileInputData") && Intent.HasExtra("ProfileInputType"))
            {
                if (Intent.GetStringExtra("ProfileInputType").Trim().Equals("gender"))
                {
                    layoutInput.Visibility = ViewStates.Gone;
                    layoutGender.Visibility = ViewStates.Visible;
                    lvCountry.Visibility = ViewStates.Gone;

                    if (Intent.GetIntExtra("gender", 0) == 0) rbtnMale.Checked = true;
                    else if (Intent.GetIntExtra("gender", 0) == 1) rbtnFemale.Checked = true;
                    else rbtnUndisclosed.Checked = true;
                }
                else if (Intent.GetStringExtra("ProfileInputType").Trim().Equals("country"))
                {
                    lvCountry.Visibility = ViewStates.Visible;
                    layoutInput.Visibility = ViewStates.Gone;
                    layoutGender.Visibility = ViewStates.Gone;
                }
                else
                {
                    edtInput.Visibility = ViewStates.Visible;
                    layoutGender.Visibility = ViewStates.Gone;
                    lvCountry.Visibility = ViewStates.Gone;
                    input = Intent.GetStringExtra("ProfileInputData").Trim();
                    edtInput.Text = input;
                }
            }

             
            rbtnGenderGroup.CheckedChange += (s, e) =>
            {
               // btnDone.Visibility = ViewStates.Visible;
            };

            lvCountry.ItemClick += (s, e) =>
            {
              //  btnDone.Visibility = ViewStates.Visible;
                country = adapter.GetItem(e.Position);
            };



            //lvCountry.ItemSelected += (s, e) =>
            //{

            //};

            btnCancel.Click += (s, e) =>
            {
                OnBackPressed();
            };

           

        }

      
    }
}