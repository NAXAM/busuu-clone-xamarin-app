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
using Android.Text;
using Java.Lang;
using Android.Graphics;
using Android.Text.Style;
using MvvmCross.Droid.Support.V7.AppCompat;
using Naxam.Busuu.Social.ViewModels;

namespace Naxam.Busuu.Droid.Social.Views
{
    //TODO Missing class GiveFeedbackAnswerViewModel
    //[Activity(Label = "Correct Exercise", Theme = "@style/AppTheme.NoActionBar")]
    //public class GiveFeedbackAnswerView : MvxAppCompatActivity<GiveFeedbackAnswerViewModel>, ITextWatcher
    //{ 
    //    private EditText edtAnswer;
    //    private int Start; 
    //    private int len;
    //    public void AfterTextChanged(IEditable s)
    //    {
    //        if (Start + 1 <= s.Length() && len <= s.Length())
    //            s.SetSpan(new ForegroundColorSpan(Color.ParseColor("#00D800")), Start, Start + 1, SpanTypes.InclusiveInclusive);
    //    }

    //    public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
    //    {
    //        len = s.Length();
    //        Start = start;
    //    }

    //    //public void OnClick(View v)
    //    //{
    //    //    if (v.Id == btnSend.Id)
    //    //    {
    //    //        Dialog dialog = new Dialog(v.Context);
    //    //        dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
    //    //        dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
    //    //        dialog.SetContentView(Resource.Layout.dialog_send);
    //    //        dialog.Show();

    //    //    }
    //    //}

    //    public void OnTextChanged(ICharSequence s, int start, int before, int count)
    //    {
    //    }

    //    protected override void OnCreate(Bundle savedInstanceState)
    //    {
    //        base.OnCreate(savedInstanceState);
    //        SetContentView(Resource.Layout.activity_give_feedback_answer);
    //        Init();
    //    }
         

    //    private void Init()
    //    {
    //        var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
    //        toolbar.SetTitleTextColor(Color.White);
    //        SetSupportActionBar(toolbar);
    //        SupportActionBar.SetDisplayHomeAsUpEnabled(true);
    //        edtAnswer = (EditText)FindViewById(Resource.Id.edtAnswer);
    //        edtAnswer.AddTextChangedListener(this);
    //    }

    //    public override bool OnSupportNavigateUp()
    //    {
    //        ViewModel.BackCmd?.Execute();
    //        return base.OnSupportNavigateUp();
    //    }
    //}
}