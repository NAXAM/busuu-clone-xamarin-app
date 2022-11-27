using Foundation;
using System;
using UIKit;
using Naxam.Busuu.Learning.Models;
using ObjCRuntime;
using CoreGraphics;
using FFImageLoading;
using FFImageLoading.Work;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public partial class FillSentenceView : MemoriseBaseView
    {
		public event EventHandler<AnswerModel> AnswerClick;

		public FillSentenceView(IntPtr handle) : base (handle)
        {
		}

		public static FillSentenceView Create(UnitModel item)
		{
			var arr = NSBundle.MainBundle.LoadNib("FillSentence", null, null);
			var v = Runtime.GetNSObject<FillSentenceView>(arr.ValueAt(0));
			v.Item = item;
            v.InitData();
			return v;
		}

        void InitData()
        {
            if (Item != null)
            {
                lblTitle.Text = Item.Title;
                lblQuestion.Text = Item.Inputs[0].Replace("%%", "_____");

                ImageService.Instance.LoadUrl(Item.Images[0]).
                        ErrorPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.ApplicationBundle).
                        LoadingPlaceholder("anime-wallpaper-art-pc (27).jpg", ImageSource.ApplicationBundle).
                            Into(imgFillSentence);
            }

            nfloat x = 0;
            for (int i = 0; i < Item.Answers.Count; i++)
            {
                var button = new UIButton(new CGRect(x, 0, 54, 31));
                x += 54 + 12;
                button.SetTitle(Item.Answers[i].Text, UIControlState.Normal);
                button.TitleLabel.Font = UIFont.SystemFontOfSize(14f);
                button.Layer.ShadowRadius = 2;
                button.Layer.ShadowOffset = new CGSize(0, 2);
                button.Layer.ShadowOpacity = 0.25f;
                button.BackgroundColor = UIColor.White;
                button.SetTitleColor(UIColor.DarkTextColor, UIControlState.Normal);
                button.Tag = i + 100;
                button.TouchUpInside += (sender, e) =>
                {
                    var btn = sender as UIButton;
                    AnswerClick?.Invoke(sender, Item.Answers[(int)btn.Tag - 100]);
                };
                ViewAnswers.AddSubview(button);
            }

            viewAnswersWidthContraint.Constant = x;

			AnswerClick += (sender, e) =>
			{
				string aw = Item.Inputs[0].Replace("%%", "_____");
				int unitAw = aw.IndexOf("_____");

				var greenAttributes = new UIStringAttributes
				{
					Font = UIFont.BoldSystemFontOfSize(15f),
					ForegroundColor = UIColor.FromRGB(116, 184, 39)
				};

				var button = sender as UIButton;
                if (e.Value)
                {
                    button.BackgroundColor = UIColor.FromRGB(207, 234, 252);

                    string TextBoss = lblQuestion.Text.Replace("_____", button.Title(UIControlState.Normal));

                    var prettyString = new NSMutableAttributedString(TextBoss);

                    prettyString.SetAttributes(greenAttributes.Dictionary, new NSRange(unitAw, button.Title(UIControlState.Normal).Length));

                    lblQuestion.AttributedText = prettyString;
					LearnView.myPoint += 1;
				}
                else
                {
                    button.BackgroundColor = UIColor.FromRGB(207, 234, 252);
                                    
                    var redAttributes = new UIStringAttributes
                    {
                        Font = UIFont.BoldSystemFontOfSize(15f),
                        ForegroundColor = UIColor.FromRGB(234, 66, 48),
                        StrikethroughStyle = NSUnderlineStyle.Single,
                    };

                    string TextBoss = "";

					for (int i = 0; i < Item.Answers.Count; i++)
					{
						if (Item.Answers[i].Value)
						{
                            TextBoss = lblQuestion.Text.Replace("_____", button.Title(UIControlState.Normal) + "\n" + (ViewAnswers.ViewWithTag(i + 100) as UIButton).Title(UIControlState.Normal));

							var prettyString = new NSMutableAttributedString(TextBoss);
							prettyString.AddAttribute(UIStringAttributeKey.BaselineOffset,
													  NSNumber.FromNInt(0),
													  new NSRange(0, TextBoss.Length));

							int uLan2 = TextBoss.IndexOf(button.Title(UIControlState.Normal));
							int uLan3 = TextBoss.IndexOf((ViewAnswers.ViewWithTag(i + 100) as UIButton).Title(UIControlState.Normal));
							prettyString.SetAttributes(redAttributes.Dictionary, new NSRange(uLan2, button.Title(UIControlState.Normal).Length));
							prettyString.SetAttributes(greenAttributes.Dictionary, new NSRange(uLan3, (ViewAnswers.ViewWithTag(i + 100) as UIButton).Title(UIControlState.Normal).Length));
                            lblQuestion.AttributedText = prettyString;

                            break;
						}
					}
             
                    /* NSString has its own method to get range of a sub string */
                    //var falseRange = ((NSString)TextBoss).LocalizedStandardRangeOfString((NSString)btnFalse.TitleLabel.Text);
                    //var trueRange = ((NSString)TextBoss).LocalizedStandardRangeOfString((NSString)btnTrue.TitleLabel.Text);
                    //prettyString.SetAttributes(redAttributes.Dictionary, falseRange);
                    //prettyString.SetAttributes(greenAttributes.Dictionary, trueRange);
                }

                ViewAnswers.UserInteractionEnabled = false;
				Answered.DidAnswer(this);
			};
        }
	}
}