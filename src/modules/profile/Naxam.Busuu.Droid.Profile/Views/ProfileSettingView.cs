using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Orhanobut.Dialogplus;
using System.IO;
using Com.Nguyenhoanglam.Imagepicker.Model;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Core.Models;
using Naxam.Busuu.Profile.ViewModels;
using Naxam.Busuu.Droid.Core.Transform;
using Com.Nguyenhoanglam.Imagepicker.UI.Imagepicker;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Profile.Views
{
    [Activity(Label = "Settings")]
    public class ProfileSettingView : MvxAppCompatActivity<ProfileSettingViewModel>
    {
        private FrameLayout layoutPersonalAvatar;
        private LinearLayout layoutNotificationSetting;
        private LinearLayout layoutLogOut;
        private ImageView imPersonalAvatar; 

        int REQUEST_CODE_PICKER = 2000;
        int REQUEST_CODE_CAMERA = 0;
        int REQUEST_CODE_CHANGE_DATA = 1;
        private List<Image> images = new List<Image>();
        UserModel model;

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.profiles_setting);

            model = new UserModel()
            {
                username = "nghianahit",
                password = "kobiet",
                avatarImage = "https://scontent.fhan2-1.fna.fbcdn.net/v/t1.0-9/19554344_1170448823067074_3677184999917790335_n.jpg?oh=2882b6b2b9c7bfcba934fa3f4e9876cb&oe=5A01D016",
                fullName = "Ha Minh Nghia",
                gender = 1,
                Country = new CountryModel()
                {
                    Country = "Viet Nam",
                    PhoneCode = "+84"
                },
                SpeakLanguages = new List<LanguageModel>()
                {
                    new LanguageModel()
                    {
                        Language = "Viet Nam"
                    },
                    new LanguageModel()
                    {
                        Language = "English"
                    },
                    new LanguageModel()
                    {
                        Language = "French"
                    }
                },
                interfaceLanguage = new LanguageModel()
                {
                    Language = "Viet Nam"
                }
            };

            InitInterface();
        }

        public void InitInterface()
        {
            layoutPersonalAvatar = FindViewById<FrameLayout>(Resource.Id.layout_personal_avatar);


            layoutNotificationSetting = FindViewById<LinearLayout>(Resource.Id.layout_notifications_setting);



            layoutLogOut = FindViewById<LinearLayout>(Resource.Id.layout_log_out);



            imPersonalAvatar = FindViewById<ImageView>(Resource.Id.im_avatar);

              
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);



            if (model.avatarImage != null)
            {
                var options = new RequestOptions()
                    .Transform(new CircleTransform(this));

                Glide.With(this).Load("https://scontent.fhan2-1.fna.fbcdn.net/v/t1.0-9/19554344_1170448823067074_3677184999917790335_n.jpg?oh=2882b6b2b9c7bfcba934fa3f4e9876cb&oe=5A01D016").Apply(options).Into(imPersonalAvatar);
            }




            Intent intentChangeInput = new Intent(this, typeof(ProfileInputView));

            layoutPersonalAvatar.Click += (s, e) =>
            {
                DialogPlus dialog = DialogPlus.NewDialog(this)
                .SetContentHolder(new ViewHolder(Resource.Layout.dialog_image_picker))
                .SetGravity((int)GravityFlags.Bottom)
                .SetAdapter(new DialogImagePickerAdapter())
                .SetOnClickListener(new OnClickListener()
                {
                    ClickAction = (d, v) =>
                    {
                        if (v.Id == Resource.Id.btn_take_camera)
                        {
                            Intent cameraIntent = new Intent(Android.Provider.MediaStore.ActionImageCapture);
                            StartActivityForResult(cameraIntent, REQUEST_CODE_CAMERA);
                        }

                        if (v.Id == Resource.Id.btn_take_gallery)
                        {
                            ImagePicker.With(this)
                                       .SetMultipleMode(false)
                                       .SetSelectedImages(images.ToArray())
                                       .SetFolderTitle("Album")
                                       .SetImageTitle("Tap to select")
                                       .SetShowCamera(true)
                                       .SetMaxSize(10)
                                       .SetFolderMode(true)
                                       .Start();
                        }

                        d.Dismiss();
                    }
                })
                .SetOnItemClickListener(new OnItemClickListener()
                {
                    ItemClick = (p0, p1, p2, p3) =>
                    {
                    }
                }).SetExpanded(false)
                .SetCancelable(true)
                .Create();
                dialog.Show();
            };


            layoutLogOut.Click += (s, e) =>
            {
                Toast.MakeText(this, "Log out complete!", ToastLength.Short).Show();
            };


        }

        public override bool OnSupportNavigateUp()
        {
            ViewModel.BackCommand?.Execute();
            return base.OnSupportNavigateUp();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            var options = new RequestOptions()
                .Transform(new CircleTransform(this));
            
            if (requestCode == 0 && resultCode == Result.Ok)
            {
                Bitmap photo = (Bitmap)data.Extras.Get("data");
                MemoryStream stream = new MemoryStream();
                photo.Compress(Bitmap.CompressFormat.Png, 0, stream);
                byte[] bitmapData = stream.ToArray();

                Glide.With(this).Load(bitmapData).Apply(options).Into(imPersonalAvatar);
                imPersonalAvatar.SetImageBitmap(photo);
            }
            else if (resultCode == Result.Ok && requestCode == Config.RcPickImages)
            {
                var imagexx = data.GetParcelableArrayListExtra(Config.ExtraImages);
                images.Clear();
                foreach (var item in imagexx)
                {
                    images.Add((Image)item);
                }

                Glide.With(this).Load(new Java.IO.File(images[0].Path)).Apply(options).Into(imPersonalAvatar);
            }
            else if (resultCode == Result.Ok && requestCode == REQUEST_CODE_CHANGE_DATA)
            {
                if (data.HasExtra("name") && data.GetStringExtra("name") != null)
                {
                    // txtPersonalName.Text = data.GetStringExtra("name");
                }
                else if (data.HasExtra("aboutme") && data.GetStringExtra("aboutme") != null)
                {
                    //  txtAboutMe.Text = data.GetStringExtra("aboutme").Length != 0 ? data.GetStringExtra("aboutme") : "Write a bit about yourselft";
                }
                else if (data.HasExtra("country") && data.GetStringExtra("country") != null)
                {
                    //txtCountry.Text = data.GetStringExtra("country");
                }
                else if (data.HasExtra("gender") && data.GetIntExtra("gender", 0) != null)
                {
                    // txtGender.Text = data.GetIntExtra("gender", 0) != 0 ? data.GetIntExtra("gender", 0) != 1 ? "Undisclosed" : "Female" : "Male";
                }
                else if (data.HasExtra("ispeak") && data.GetStringExtra("ispeak") != null)
                {

                }
                else if (data.HasExtra("interfacelanguage") && data.GetStringExtra("interfacelanguage") != null)
                {

                }
            }
        }
        class OnClickListener : Java.Lang.Object, IOnClickListener
        {
            public Action<DialogPlus, View> ClickAction;
            public void OnClick(DialogPlus p0, View p1)
            {
                ClickAction?.Invoke(p0, p1);
            }
        }
        class OnItemClickListener : Java.Lang.Object, IOnItemClickListener
        {
            public Action<DialogPlus, Java.Lang.Object, View, int> ItemClick { get; set; }
            public void OnItemClick(DialogPlus p0, Java.Lang.Object p1, View p2, int p3)
            {
                ItemClick?.Invoke(p0, p1, p2, p3);
            }
        }

    }
}