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
using Android.Support.V4.App;
using Android.Support.V4.View;
using Java.Lang;
using Naxam.Busuu.Learning.Models;
using Naxam.Busuu.Droid.Learning.Control.Vocabulary;
using Naxam.Busuu.Droid.Learning.Control;
using Naxam.Busuu.Droid.Learning.Control.Memo;

namespace Naxam.Busuu.Droid.Learning.Adapters
{
    public class VocabularyPagerAdapter : PagerAdapter
    {
        public event EventHandler<int> Next;
        UnitModel Unit;
        public VocabularyPagerAdapter(UnitModel unit)
        {
            Unit = unit;
        }

        public override int Count => Unit.Tip == null ? 2 : 3;

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == (View)@object;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            var obj = (MemoBaseView)@object;
            if (obj != null)
            {
                obj.NextClick -= Item_NextClick;
                container.RemoveView(obj);
            } 
        }
        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            Context context = container.Context;
            if (position == 0)
            {
                PreparePronounceView ppv = new PreparePronounceView(context, Unit);
                container.AddView(ppv);
                return ppv;
            }
            if (position == 1 && Unit.Tip != null)
            {
                TipView tip = new TipView(context, Unit);
                container.AddView(tip);
                return tip;
            }
            if ((position == 1 && Unit.Tip == null) || position == 2)
            {

                switch (Unit.Type)
                {
                    case UnitModel.UnitType.FillSentence:
                        var fillst = new FillSentenceView(context, Unit);
                        fillst.NextClick += Item_NextClick;
                        container.AddView(fillst);
                        return fillst;
                    case UnitModel.UnitType.ChooseWord:
                        var choose = new ChooseWordView(context, Unit);
                        choose.NextClick += Item_NextClick;
                        container.AddView(choose);
                        return choose;
                    case UnitModel.UnitType.MatchingSentence:
                        var matching = new MatchingSentenceView(context, Unit);
                        matching.NextClick += Item_NextClick;
                        container.AddView(matching);
                        return matching;
                    case UnitModel.UnitType.SelectWord:
                        var select = new SelectWordView(context, Unit);
                        select.NextClick += Item_NextClick;
                        container.AddView(select);
                        return select;
                    case UnitModel.UnitType.SelectWordImage:
                        var selectImage = new SelectWordImageView(context, Unit);
                        selectImage.NextClick += Item_NextClick;
                        container.AddView(selectImage);
                        return selectImage;
                    case UnitModel.UnitType.OrderWord:
                        var order = new OrderWordView(context, Unit);
                        order.NextClick += Item_NextClick;
                        container.AddView(order);
                        return order;
                    case UnitModel.UnitType.CompleteSentence:
                        var complete = new CompleteSentenceView(context, Unit);
                        complete.NextClick += Item_NextClick;
                        container.AddView(complete);
                        return complete;
                    case UnitModel.UnitType.TrueFalseQuestion:
                        var truefalse = new TrueFalseHearQuestionView(context, Unit);
                        truefalse.NextClick += Item_NextClick; ;
                        container.AddView(truefalse);
                        return truefalse;
                }
            }
            return null;
        }

        private void Item_NextClick(object sender, int e)
        {
            Next?.Invoke(this, e);
        }
    }
}