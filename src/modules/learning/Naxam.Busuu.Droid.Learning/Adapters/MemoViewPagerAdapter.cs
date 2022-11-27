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
using Android.Support.V4.View;
using Java.Lang;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Droid.Learning.Control.Memo;
using Naxam.Busuu.Droid.Learning.Control;

namespace Naxam.Busuu.Droid.Learning.Adapters
{
    public class MemoViewPagerAdapter : PagerAdapter
    {
        public event EventHandler<int> Next;
        Context context;
        IList<UnitModel> itemSource;
        public MemoViewPagerAdapter(Context context, IList<UnitModel> itemSource)
        {
            this.context = context;
            this.itemSource = itemSource;
        }
        public override int Count => itemSource.Count;

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == ((MemoBaseView)@object);
        }
         

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
             
            UnitModel unit = itemSource[position]; 
            switch (unit.Type)
            {
                case UnitModel.UnitType.FillSentence:
                    var fillst = new FillSentenceView(context, unit);
                    fillst.NextClick += Item_NextClick;
                    container.AddView(fillst);
                    return fillst;
                case UnitModel.UnitType.ChooseWord:
                    var choose = new ChooseWordView(context, unit);
                    choose.NextClick += Item_NextClick;
                    container.AddView(choose);
                    return choose;
                case UnitModel.UnitType.MatchingSentence:
                    var matching = new MatchingSentenceView(context, unit);
                    matching.NextClick += Item_NextClick;
                    container.AddView(matching);
                    return matching;
                case UnitModel.UnitType.SelectWord:
                    var select = new SelectWordView(context, unit); 
                    select.NextClick += Item_NextClick;
                    container.AddView(select);
                    return select;
                case UnitModel.UnitType.SelectWordImage:
                    var selectImage = new SelectWordImageView(context, unit);
                    selectImage.NextClick += Item_NextClick;
                    container.AddView(selectImage);
                    return selectImage;
                case UnitModel.UnitType.OrderWord:
                    var order = new OrderWordView(context, unit);
                    order.NextClick += Item_NextClick;
                    container.AddView(order);
                    return order;
                case UnitModel.UnitType.CompleteSentence:
                    var complete = new CompleteSentenceView(context, unit);
                    complete.NextClick += Item_NextClick;
                    container.AddView(complete);
                    return complete;
                case UnitModel.UnitType.TrueFalseQuestion:
                    var truefalse = new TrueFalseHearQuestionView(context, unit);
                    truefalse.NextClick += Item_NextClick;
                    container.AddView(truefalse);
                    return truefalse;
            } 
            return null;
        }

        private void Item_NextClick(object sender, int e)
        {
            Next?.Invoke(sender, e);
        }


        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            ((MemoBaseView)@object).NextClick -= Item_NextClick;
            container.RemoveView((MemoBaseView)@object);
        }


    }
}