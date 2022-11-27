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

namespace Naxam.Busuu.Droid.Core.Dialogs
{
    public class VoucherDialog : Dialog
    {
        public VoucherDialog(Context context) : base(context)
        {
            Init();        }

        public VoucherDialog(Context context, int themeResId) : base(context, themeResId)
        {
            Init();
        }

        protected VoucherDialog(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init();
        }

        protected VoucherDialog(Context context, bool cancelable, EventHandler cancelHandler) : base(context, cancelable, cancelHandler)
        {
            Init();
        }

        protected VoucherDialog(Context context, bool cancelable, IDialogInterfaceOnCancelListener cancelListener) : base(context, cancelable, cancelListener)
        {
            Init();
        }
        private void Init()
        {
            this.RequestWindowFeature(1);
            this.SetContentView(Resource.Layout.voucher_dialog);
            this.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

        }
    }
}