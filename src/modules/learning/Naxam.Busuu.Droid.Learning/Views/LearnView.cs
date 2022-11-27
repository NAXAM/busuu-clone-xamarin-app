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
using MvvmCross.Droid.Support.V7.AppCompat;
using Com.Ittianyu.Bottomnavigationviewex;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Binding.Droid.Views;
using Naxam.Busuu.Droid.Learning.Control;
using MvvmCross.Binding.Droid.BindingContext;
using Naxam.Busuu.Learning.Models;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using Naxam.Busuu.Droid.Core; 
using MvvmCross.Droid.Support.V4;
using Naxam.Busuu.Droid.Learning.Dialogs;
using Naxam.Busuu.Learning.ViewModels;

namespace Naxam.Busuu.Droid.Learning.Views
{

    [NxFragment(BusuuFragmentHosts.MainView, true, ViewModelType = typeof(LearnViewModel))]
    [Register("naxam.busuu.droid.Learning.views.LearnView")]
    public class LearnView : MvxFragment<LearnViewModel>, View.IOnTouchListener
    {
        DownloadDialog DownloadDialog;
        NXMvxExpandableListView expLessons; 

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater,container,savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.LearnActivity, container, false);
            view.SetOnTouchListener(this);
            OnGroupClickListener GroupClick = new OnGroupClickListener(Activity);
            expLessons = view.FindViewById<NXMvxExpandableListView>(Resource.Id.expLessons);
            NXMvxExpandableListAdapter adapter = new NXMvxExpandableListAdapter(Context, (IMvxAndroidBindingContext)BindingContext)
            {
                ItemsSource = expLessons.ItemsSource,
                GroupTemplateId = expLessons.GroupTemplateId,
                ItemTemplateId = expLessons.ItemTemplateId
            };
            adapter.ExerciseClick += (s, e) =>
            {
                if (expLessons.ExerciseClickCommand == null)
                    return;
                if (expLessons.ExerciseClickCommand.CanExecute(e))
                {
                    expLessons.ExerciseClickCommand.Execute(e);
                }
            };
            adapter.DownloadClick += (s, e) =>
            {
                DownloadDialog = new DownloadDialog(view.Context, "https://media.ngoisao.vn/resize_580/news/2016/12/07/ngoc-trinh-vong-1-xuong-cap-ngoisaovn-26-ngoisao.vn-w550-h787.stamp2.jpg");
                DownloadDialog.Show();
                if (expLessons.DownloadCommand == null)
                    return;
                if (expLessons.DownloadCommand.CanExecute(e))
                {
                    expLessons.DownloadCommand.Execute(e);
                }
            };
            adapter.DoneAnim += (s, e) =>
            {
                if (!e)
                {
                    expLessons.ExpandGroup((int)s);
                }
                else
                {
                    expLessons.CollapseGroup((int)s);
                }
            };
            expLessons.SetAdapter(adapter);
            expLessons.SetOnGroupClickListener(GroupClick);
            expLessons.SetOnTouchListener(GroupClick);
            expLessons.GroupExpand += ExpLessons_GroupExpand;
            expLessons.GroupCollapse += ExpLessons_GroupCollapse;
            expLessons.OffsetTopAndBottom(0);
            return view;
        }
       
 
        private void ExpLessons_GroupCollapse(object sender, ExpandableListView.GroupCollapseEventArgs e)
        { 
            expLessons.SmoothScrollToPositionFromTop(e.GroupPosition, 0, 500);
        }



        private void ExpLessons_GroupExpand(object sender, ExpandableListView.GroupExpandEventArgs e)
        {
            expLessons.SmoothScrollToPositionFromTop(e.GroupPosition, 0, 500);
        }

        float x;
        float y;
     
        public bool OnTouch(View v, MotionEvent e)
        {
            // maybe change this method later
            x = e.GetX();
            y = e.GetY();
            return true;
        }
    }
    class OnGroupClickListener : Java.Lang.Object, ExpandableListView.IOnGroupClickListener, View.IOnTouchListener
    {
        public float x { set; get; }
        public float y { set; get; }
        Activity context;
        public OnGroupClickListener(Activity context)
        {
            this.context = context;
        }

        public bool OnGroupClick(ExpandableListView parent, View clickedView, int groupPosition, long id)
        {
            // parent.SetSelection(groupPosition);
            // parent.SmoothScrollToPosition(groupPosition);
            var view = (LessonHeaderBackground)clickedView;
            //  view.InitAnim(x, y);
            //view.IsExpand = parent.IsGroupExpanded(groupPosition);
            return false;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            x = e.GetX();
            y = e.GetY();
            return false;
        }
    }
}