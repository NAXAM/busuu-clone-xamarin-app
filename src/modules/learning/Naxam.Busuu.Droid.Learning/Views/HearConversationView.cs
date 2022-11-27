using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Android.Views.Animations;
using Naxam.Busuu.Droid.Learning.Models;
using Naxam.Busuu.Droid.Learning.Adapters;
using Android.Graphics;
using System.Threading.Tasks;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Naxam.Busuu.Droid.Learning.Views
{
    [Activity]
    public class HearConversationView : MvxAppCompatActivity
    {
        private FrameLayout btPLay;
        private ImageView imBtPlay;
        //private ListView lvConversation;
        private bool isPlay = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hear_conversation_page);

            InitInterface();
        }

        public void InitInterface()
        {
            btPLay = FindViewById<FrameLayout>(Resource.Id.bt_play);
            imBtPlay = FindViewById<ImageView>(Resource.Id.im_bt_play);
            //lvConversation = FindViewById<ListView>(Resource.Id.list_conversation);

            //Conversation conversation = new Conversation();
            //HearConversationAdapter adapter = new HearConversationAdapter(this, Resource.Layout.conversation_sentence_list_item, conversation.Conversations);
            //lvConversation.Adapter = adapter;
            //lvConversation.ChoiceMode = ChoiceMode.Single;

            btPLay.Click += BtPLay_Click;
        }

        

        private void BtPLay_Click(object sender, EventArgs e)
        {
            if (isPlay)
            {
                RotateAnimation rotate = new RotateAnimation(0, 90, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
                rotate.Duration = 250;
                rotate.FillAfter = true;
                imBtPlay.StartAnimation(rotate);             

                rotate.SetAnimationListener(new AnimationListener
                {
                    AnimationStart = (a) =>
                    {
                        ScaleAnimation scale = new ScaleAnimation(1f, 1.1f, 1f, 1.1f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
                        scale.Duration = 250;
                        btPLay.StartAnimation(scale);
                    }
                    ,
                    AnimationEnd = (a) =>
                    {
                        imBtPlay.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.ic_pause));
                        RotateAnimation rotateBackward = new RotateAnimation(180, 0, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
                        rotateBackward.Duration = 250;
                        rotateBackward.FillAfter = true;
                        imBtPlay.StartAnimation(rotateBackward);
                    }
                });                
            }
            else
            {
                RotateAnimation rotate = new RotateAnimation(0, 180, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
                rotate.Duration = 250;
                rotate.FillAfter = true;
                imBtPlay.StartAnimation(rotate);
                rotate.SetAnimationListener(new AnimationListener
                {
                    AnimationStart = (a) =>
                    {
                        ScaleAnimation scale = new ScaleAnimation(1f, 1.1f, 1f, 1.1f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
                        scale.Duration = 250;
                        btPLay.StartAnimation(scale);
                    }
                    ,
                    AnimationEnd = (a) =>
                    {
                        imBtPlay.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.ic_play_arrow));
                        RotateAnimation rotateBackward = new RotateAnimation(180, 0, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
                        rotateBackward.Duration = 250;
                        rotateBackward.FillAfter = true;
                        imBtPlay.StartAnimation(rotateBackward);
                    }
                });
            }
            isPlay = !isPlay;
        }
    }
}