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
using Com.Bumptech.Glide;
using Naxam.Busuu.Learning.Models;
using Com.Google.Android.Flexbox;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using System.Text.RegularExpressions;
using System.Net;
using Java.IO;
using Android.Provider;
using Android.Database;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;
using Naxam.Busuu.Learning.ViewModels;
using Naxam.Busuu.Droid.Core;

namespace Naxam.Busuu.Droid.Learning.Control
{
    [NxFragment(BusuuFragmentHosts.Dialogue, false, ViewModelType = typeof(DialogueNormalListSentenceViewModel))]
    public class DialogueNormalListSentenceFragment : MvxFragment<DialogueNormalListSentenceViewModel>
    { 
        public int OrientationScreen;
        private int focusIndex;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.conversation_normal_list_sentence_layout, container, false);
            Init(view);
            return view;
        }

        public void Init(View view)
        {
            //int margin = (int)Util.Util.PxFromDp(Context, 4);
            //ListView lstView = view.FindViewById<ListView>(Resource.Id.lstView);
            //lstView.DividerHeight = 0;
            //lstView.Divider = null;
            //lstView.SetSelector(Resource.Drawable.listview_selector);
            //NXPlayButton btnPlay = view.FindViewById<NXPlayButton>(Resource.Id.btnPlay);
            //lstView.ItemClick += (s, e) =>
            //{
            //    AudioModel audio =  ElementAt(e.Position).Audios.FirstOrDefault();
            //    if (audio == null)
            //        return;
            //    btnPlay.Play(audio.Start, audio.End);
            //};
             
            //List<AudioModel> listAudio = Items.Select(d => d.Audios?.FirstOrDefault()).ToList();
            //DialogueListNormalAdapter adapter = new DialogueListNormalAdapter(Context, Items, -1);
            //btnPlay.PositionChanged += (s, e) =>
            //{
                
            //    AudioModel selectedAudio = listAudio.Where(d => d.Start <= e / 1000 && d.End > e / 1000).FirstOrDefault();
            //    focusIndex = listAudio.IndexOf(selectedAudio);
            //    lstView.SetSelection(focusIndex);
            //    if (adapter.FocusIndex != focusIndex)
            //    {
            //        adapter.FocusIndex = focusIndex;
            //        adapter.NotifyDataSetChanged();
            //    }

            //};
            //FlexboxLayout flexbox = view.FindViewById<FlexboxLayout>(Resource.Id.flexAnswer);
            //if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
            //{
            //    if (Context.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait)
            //    {
            //        ((LinearLayout)flexbox.Parent).Elevation = margin / 2;
            //    }
            //    else
            //    {
            //        flexbox.Elevation = margin / 2;
            //    }
            //    btnPlay.Elevation = margin / 2 + 2;
            //}
            //Button btnNext = view.FindViewById<Button>(Resource.Id.btnNext);
            //btnNext.Click += (s, e) =>
            //{
            //    NextClicked.Invoke(btnNext, 0);
            //};
            //lstView.Adapter = adapter;

            //if (Items[0].Audios?.Count > 0)
            //{
            //    btnPlay.Visibility = ViewStates.Visible;
            //}
            //else
            //{
            //    btnPlay.Visibility = ViewStates.Gone;
            //}
        }

        private Drawable GetBackground(Color color)
        {
            PaintDrawable background = new PaintDrawable(color);
            background.SetCornerRadius((int)Util.Util.PxFromDp(Context, 4));
            background.Shape = new RectShape();
            return background;
        }
        private string Download(string Url)
        {
            string filename = "test.mp3";
            File root = Android.OS.Environment.ExternalStorageDirectory;
            File dir = new File(root.AbsolutePath + "/busuu/media");
            if (dir.Exists() == false)
            {
                dir.Mkdirs();
            }

            File file = new File(dir, filename);

            if (file.Exists())
            {
                return file.AbsolutePath;
            }
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += DownloadProgressChanged;
                wc.DownloadFileAsync(new System.Uri(Url), file.AbsolutePath);
            }
            return file.AbsolutePath;
        }

        void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("---->" + e.ProgressPercentage);
        }
        private int GetIndexByRowPosition(int row, int position)
        {
            return 0;
            //if (row >= Items.Count)
            //    return -1;
            //int index = 0;
            //for (int i = 0; i < row; i++)
            //{
            //    var temp = " " + Items[i].Input[0].Trim() + " ";
            //    int count = Regex.Split(temp, "%%").Length - 1;
            //    if (count > 0)
            //        index += count;
            //}
            //return index;
        }
    }
}