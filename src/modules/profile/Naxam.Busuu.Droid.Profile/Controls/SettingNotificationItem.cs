using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Content.Res;
using static Android.Widget.CompoundButton;

namespace Naxam.Busuu.Droid.Profile.Controls
{
    public class SettingNotificationItem : RelativeLayout, IOnCheckedChangeListener
    {
        public event EventHandler<bool> CheckedChange;

        public bool IsEnabled
        {
            get => mSwitch?.Enabled == true;
            set
            {
                if (mSwitch == null)
                    return;
                mSwitch.Enabled = value;
            }
        }

        public bool Checked
        {
            get => mSwitch?.Checked == true;
            set
            {
                if (mSwitch == null)
                    return;
                mSwitch.Checked = value;
            }
        }

        Switch mSwitch;

        public SettingNotificationItem(Context context) : base(context)
        {
            AddView(context, null);
        }

        public SettingNotificationItem(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            AddView(context, attrs);
        }

        public SettingNotificationItem(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            AddView(context, attrs);
        }

        public SettingNotificationItem(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            AddView(context, attrs);
        }

        protected SettingNotificationItem(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            AddView(Context, null);
        }

        private void AddView(Context context, IAttributeSet attrs)
        {
            RemoveAllViews();
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.setting_notification_item, null);
            if (attrs != null)
            {
                TypedArray typedArray = context.ObtainStyledAttributes(attrs, Resource.Styleable.SettingNotification);
                string title = typedArray.GetString(Resource.Styleable.SettingNotification_sn_title);
                string description = typedArray.GetString(Resource.Styleable.SettingNotification_sn_description);
                bool bold = typedArray.GetBoolean(Resource.Styleable.SettingNotification_sn_bold, false);
                TextView txtTitle = view.FindViewById<TextView>(Resource.Id.txtTypeNotify);
                txtTitle.Text = title;
                txtTitle.SetTypeface(txtTitle.Typeface, bold ? Android.Graphics.TypefaceStyle.Bold : Android.Graphics.TypefaceStyle.Normal);
                TextView txtDescription = view.FindViewById<TextView>(Resource.Id.txtExample);
                if (string.IsNullOrEmpty(description))
                {
                    txtDescription.Visibility = ViewStates.Gone;
                }
                else
                {
                    txtDescription.Visibility = ViewStates.Visible;
                    txtDescription.Text = description;
                }

            }
            mSwitch = view.FindViewById<Switch>(Resource.Id.mSwitch);
            mSwitch.SetOnCheckedChangeListener(this);
            AddView(view, new LayoutParams(-1, -1));
        }

        public virtual void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            CheckedChange?.Invoke(this, isChecked);
        }
    }
}