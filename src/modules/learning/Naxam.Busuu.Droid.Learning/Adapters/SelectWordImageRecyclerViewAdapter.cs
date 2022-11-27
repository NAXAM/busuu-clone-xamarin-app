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
using Android.Support.V7.Widget;
using Naxam.Busuu.Learning.Models;
using Com.Bumptech.Glide;
using Android.Graphics;
using Com.Bumptech.Glide.Request;

namespace Naxam.Busuu.Droid.Learning.Adapters
{
    public class SelectWordImageRecyclerViewAdapter : RecyclerView.Adapter
    {
        public event EventHandler<AnswerModel> ItemClicked;
        IList<AnswerModel> answers;
        Context context;
        int clickPosition;
        List<int> listChoice;
        public SelectWordImageRecyclerViewAdapter(Context context, IList<AnswerModel> answers,IList<int> listChoice)
        {
            this.context = context;
            this.answers = answers;
            this.listChoice = new List<int>(listChoice);
        }
        public override int ItemCount => answers.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            SelectWordImageRecyclerViewHolder viewHolder = (SelectWordImageRecyclerViewHolder)holder;
            viewHolder.txtAnswer.Text = answers[position].Text;

            var options = new RequestOptions()
                .CenterCrop();
            Glide.With(context).Load(answers[position].Image).Apply(options).Into(viewHolder.imgAnswer);
            viewHolder.imgResult.SetBackgroundColor(answers[position].Value?Color.ParseColor("#8074B825") : Color.ParseColor("#80EE6253"));
            viewHolder.imgResult.SetImageResource(answers[position].Value ? Resource.Drawable.v : Resource.Drawable.x);


            if (listChoice.Count > 0)
            {
                viewHolder.imgResult.Visibility = ViewStates.Visible;
                if (listChoice.Contains(position))
                {
                    viewHolder.txtAnswer.SetTextColor(Color.White);
                    if (!answers[position].Value)
                    {
                        viewHolder.txtAnswer.SetBackgroundColor(Color.ParseColor("#EE6253"));
                    } 
                }
                else
                {
                    if (answers[position].Value)
                    {
                        viewHolder.txtAnswer.SetTextColor(Color.White);
                        viewHolder.txtAnswer.SetBackgroundColor(Color.ParseColor("#74B825"));
                    }
                    else
                    {
                        viewHolder.imgResult.SetBackgroundColor(Color.ParseColor("#ffffff"));
                        viewHolder.imgResult.Alpha = 0.8f;
                        viewHolder.imgResult.SetImageResource(0);
                    }
                    
                }

                
            }
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.select_word_with_image_item, parent, false);
            if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
            {
                view.Elevation = Util.Util.PxFromDp(context, 4);
                view.TranslationZ = Util.Util.PxFromDp(context, 1);
            }
            
            return new SelectWordImageRecyclerViewHolder(view);
        }
    }
    public class SelectWordImageRecyclerViewHolder : RecyclerView.ViewHolder
    {
        public TextView txtAnswer;
        public ImageView imgAnswer;
        public ImageView imgResult;
        public SelectWordImageRecyclerViewHolder(View view) : base(view)
        {
            txtAnswer = view.FindViewById<TextView>(Resource.Id.txtAnswer);
            imgAnswer = view.FindViewById<ImageView>(Resource.Id.imgAnswer);
            imgResult = view.FindViewById<ImageView>(Resource.Id.imgResult);
        }
    }
}