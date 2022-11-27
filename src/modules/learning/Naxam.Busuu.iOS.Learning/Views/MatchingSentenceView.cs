using Foundation;
using System;
using UIKit;
using Naxam.Busuu.Learning.Models;
using ObjCRuntime;
using CoreAnimation;
using CoreGraphics;

namespace Naxam.Busuu.iOS.Learning.Views
{
    public partial class MatchingSentenceView : MemoriseBaseView
    {
        bool viewAnswerIsMoving;
        bool viewAnswerIsMoving1;
        bool viewAnswerIsMoving2;

        bool isFullBox3;
        bool isFullBox4;
        bool isFullBox5;

        NSLayoutConstraint viewAnswerCenterYviewBox3;
        NSLayoutConstraint viewAnswerCenterYviewBox4;
        NSLayoutConstraint viewAnswerCenterYviewBox5;

        NSLayoutConstraint viewAnswer1CenterYviewBox3;
        NSLayoutConstraint viewAnswer1CenterYviewBox4;
        NSLayoutConstraint viewAnswer1CenterYviewBox5;

		NSLayoutConstraint viewAnswer2CenterYviewBox3;
		NSLayoutConstraint viewAnswer2CenterYviewBox4;
		NSLayoutConstraint viewAnswer2CenterYviewBox5;

        NSTimer myTimer;
        byte i;

		public MatchingSentenceView(IntPtr handle) : base(handle)
		{
		}

		public static MatchingSentenceView Create(UnitModel item)
		{
			var arr = NSBundle.MainBundle.LoadNib("MatchingSentence", null, null);
			var v = Runtime.GetNSObject<MatchingSentenceView>(arr.ValueAt(0));
			v.Item = item;
			v.InitData();
			return v;
		}

        void InitData()
        {
            textBox.Text = textAnswer.Text = Item.Answers[0].Text;
            textBox1.Text = textAnswer1.Text = Item.Answers[1].Text;
            textBox2.Text = textAnswer2.Text = Item.Answers[2].Text;

            textBox3.Text = Item.Inputs[0];
            textBox4.Text = Item.Inputs[1];
            textBox5.Text = Item.Inputs[2];
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            var dottedLayer = new CAShapeLayer();
            dottedLayer.StrokeColor = UIColor.FromRGB(170, 179, 186).CGColor;
            dottedLayer.FillColor = null;
            dottedLayer.LineDashPattern = new[] { new NSNumber(4), new NSNumber(2) };
            dottedLayer.Path = UIBezierPath.FromRect(viewBox.Bounds).CGPath; //for square
            dottedLayer.Path = UIBezierPath.FromRoundedRect(viewBox.Bounds, 0).CGPath; //for rounded corners
            dottedLayer.Frame = viewBox.Bounds;

            var dottedLayer1 = new CAShapeLayer();
            dottedLayer1.StrokeColor = UIColor.FromRGB(170, 179, 186).CGColor;
            dottedLayer1.FillColor = null;
            dottedLayer1.LineDashPattern = new[] { new NSNumber(4), new NSNumber(2) };
            dottedLayer1.Path = UIBezierPath.FromRect(viewBox.Bounds).CGPath; //for square
            dottedLayer1.Path = UIBezierPath.FromRoundedRect(viewBox.Bounds, 0).CGPath; //for rounded corners
            dottedLayer1.Frame = viewBox1.Bounds;

            var dottedLayer2 = new CAShapeLayer();
            dottedLayer2.StrokeColor = UIColor.FromRGB(170, 179, 186).CGColor;
            dottedLayer2.FillColor = null;
            dottedLayer2.LineDashPattern = new[] { new NSNumber(4), new NSNumber(2) };
            dottedLayer2.Path = UIBezierPath.FromRect(viewBox.Bounds).CGPath; //for square
            dottedLayer2.Path = UIBezierPath.FromRoundedRect(viewBox.Bounds, 0).CGPath; //for rounded corners
            dottedLayer2.Frame = viewBox2.Bounds;

            viewBox.Layer.AddSublayer(dottedLayer);
            viewBox1.Layer.AddSublayer(dottedLayer1);
            viewBox2.Layer.AddSublayer(dottedLayer2);

            viewAnswer.Layer.ShadowRadius = 2;
            viewAnswer.Layer.ShadowOpacity = 0.25f;
            viewAnswer.Layer.ShadowOffset = new CGSize(0, 2);

            viewAnswer1.Layer.ShadowRadius = 2;
            viewAnswer1.Layer.ShadowOpacity = 0.25f;
            viewAnswer1.Layer.ShadowOffset = new CGSize(0, 2);

            viewAnswer2.Layer.ShadowRadius = 2;
            viewAnswer2.Layer.ShadowOpacity = 0.25f;
            viewAnswer2.Layer.ShadowOffset = new CGSize(0, 2);

            viewAnswerCenterYviewBox3 = NSLayoutConstraint.Create(viewAnswer, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, viewBox3, NSLayoutAttribute.CenterY, 1, 0);
            viewAnswerCenterYviewBox4 = NSLayoutConstraint.Create(viewAnswer, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, viewBox4, NSLayoutAttribute.CenterY, 1, 0);
            viewAnswerCenterYviewBox5 = NSLayoutConstraint.Create(viewAnswer, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, viewBox5, NSLayoutAttribute.CenterY, 1, 0);

            viewAnswer1CenterYviewBox3 = NSLayoutConstraint.Create(viewAnswer1, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, viewBox3, NSLayoutAttribute.CenterY, 1, 0);
            viewAnswer1CenterYviewBox4 = NSLayoutConstraint.Create(viewAnswer1, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, viewBox4, NSLayoutAttribute.CenterY, 1, 0);
            viewAnswer1CenterYviewBox5 = NSLayoutConstraint.Create(viewAnswer1, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, viewBox5, NSLayoutAttribute.CenterY, 1, 0);

            viewAnswer2CenterYviewBox3 = NSLayoutConstraint.Create(viewAnswer2, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, viewBox3, NSLayoutAttribute.CenterY, 1, 0);
            viewAnswer2CenterYviewBox4 = NSLayoutConstraint.Create(viewAnswer2, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, viewBox4, NSLayoutAttribute.CenterY, 1, 0);
            viewAnswer2CenterYviewBox5 = NSLayoutConstraint.Create(viewAnswer2, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, viewBox5, NSLayoutAttribute.CenterY, 1, 0);

            if (myTimer == null)
            {
                InvokeOnMainThread(() =>
                {
                    myTimer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(0.5), HandleAction);
                });
            }

            void HandleAction(NSTimer obj)
            {
                if (isFullBox3 && isFullBox4 && isFullBox5)
                {
                    textAnswer.TextColor = UIColor.White;
                    textAnswer1.TextColor = UIColor.White;
                    textAnswer2.TextColor = UIColor.White;

                    if (viewAnswer.Center == viewBox3.Center)
                    {
                        viewAnswer.BackgroundColor = UIColor.FromRGB(116, 184, 39);
                        i += 1;
                    }
                    else
                    {
                        viewAnswer.BackgroundColor = UIColor.FromRGB(234, 67, 50);
                    }

					if (viewAnswer1.Center == viewBox4.Center)
					{
						viewAnswer1.BackgroundColor = UIColor.FromRGB(116, 184, 39);
						i += 1;
					}
					else
					{
						viewAnswer1.BackgroundColor = UIColor.FromRGB(234, 67, 50);
					}

					if (viewAnswer2.Center == viewBox5.Center)
					{
						viewAnswer2.BackgroundColor = UIColor.FromRGB(116, 184, 39);
						i += 1;
					}
					else
					{
						viewAnswer2.BackgroundColor = UIColor.FromRGB(234, 67, 50);
					}

                    if (i >= 3)
                    {
                        LearnView.myPoint += 1;
                    }
					
					UserInteractionEnabled = false;
					Answered.DidAnswer(this);

					if (myTimer != null)
					{
						myTimer.Invalidate();
						myTimer = null;
					}
                }
            }
        }

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);

			var touch = touches.AnyObject as UITouch;

			if (touch == null)
				return;

            if (viewAnswer.Frame.Contains(touch.LocationInView(this)))
            {
				viewAnswerIsMoving = true;
                viewAnswer.Layer.ShadowOffset = new CGSize(0, 0);
                viewAnswer.Alpha = 0.75f;
                viewBoss.BringSubviewToFront(viewAnswer);
				
                if (viewBox3.Frame.Contains(viewAnswer.Center) == true)
                {
					isFullBox3 = false;
                }
                else if (viewBox4.Frame.Contains(viewAnswer.Center) == true)
				{
					isFullBox4 = false;
				}
                else if (viewBox5.Frame.Contains(viewAnswer.Center) == true)
				{
					isFullBox5 = false;
				}
            }
            else if (viewAnswer1.Frame.Contains(touch.LocationInView(this)))
			{
				viewAnswerIsMoving1 = true;
				viewAnswer1.Layer.ShadowOffset = new CGSize(0, 0);
				viewAnswer1.Alpha = 0.75f;
				viewBoss.BringSubviewToFront(viewAnswer1);

				if (viewBox3.Frame.Contains(viewAnswer1.Center) == true)
				{
					isFullBox3 = false;
				}
				else if (viewBox4.Frame.Contains(viewAnswer1.Center) == true)
				{
					isFullBox4 = false;
				}
				else if (viewBox5.Frame.Contains(viewAnswer1.Center) == true)
				{
					isFullBox5 = false;
				}
			}
			else if (viewAnswer2.Frame.Contains(touch.LocationInView(this)))
			{
				viewAnswerIsMoving2 = true;
				viewAnswer2.Layer.ShadowOffset = new CGSize(0, 0);
				viewAnswer2.Alpha = 0.75f;
				viewBoss.BringSubviewToFront(viewAnswer2);

				if (viewBox3.Frame.Contains(viewAnswer2.Center) == true)
				{
					isFullBox3 = false;
				}
				else if (viewBox4.Frame.Contains(viewAnswer2.Center) == true)
				{
					isFullBox4 = false;
				}
				else if (viewBox5.Frame.Contains(viewAnswer2.Center) == true)
				{
					isFullBox5 = false;
				}
			}
		}

		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);

			var touch = touches.AnyObject as UITouch;

            if (touch != null || viewAnswerIsMoving || viewAnswerIsMoving1 || viewAnswerIsMoving2)
            {
                nfloat deltaX = touch.PreviousLocationInView(this).X - touch.LocationInView(this).X;
                nfloat deltaY = touch.PreviousLocationInView(this).Y - touch.LocationInView(this).Y;
                CGPoint newPoint;

                if (viewAnswerIsMoving)
                {
					newPoint = new CGPoint(viewAnswer.Frame.X - deltaX, viewAnswer.Frame.Y - deltaY);

                    if (viewBox3.Frame.Contains(viewAnswer.Center) == true)
                    {
                        viewBox3.BackgroundColor = UIColor.FromRGB(182, 183, 189);
                    }
                    else
                    {
                        viewBox3.BackgroundColor = UIColor.FromRGB(214, 222, 230);
                    }

					if (viewBox4.Frame.Contains(viewAnswer.Center) == true)
					{
						viewBox4.BackgroundColor = UIColor.FromRGB(182, 183, 189);
					}
					else
					{
						viewBox4.BackgroundColor = UIColor.FromRGB(214, 222, 230);
					}

					if (viewBox5.Frame.Contains(viewAnswer.Center) == true)
					{
						viewBox5.BackgroundColor = UIColor.FromRGB(182, 183, 189);
					}
					else
					{
						viewBox5.BackgroundColor = UIColor.FromRGB(214, 222, 230);
					}

                    viewAnswer.Frame = new CGRect(newPoint, viewAnswer.Frame.Size);
                }
                else if (viewAnswerIsMoving1)
                {
                    newPoint = new CGPoint(viewAnswer1.Frame.X - deltaX, viewAnswer1.Frame.Y - deltaY);

                    if (viewBox3.Frame.Contains(viewAnswer1.Center) == true)
                    {
                        viewBox3.BackgroundColor = UIColor.FromRGB(182, 183, 189);
                    }
                    else
                    {
                        viewBox3.BackgroundColor = UIColor.FromRGB(214, 222, 230);
                    }

					if (viewBox4.Frame.Contains(viewAnswer1.Center) == true)
					{
						viewBox4.BackgroundColor = UIColor.FromRGB(182, 183, 189);
					}
					else
					{
						viewBox4.BackgroundColor = UIColor.FromRGB(214, 222, 230);
					}

					if (viewBox5.Frame.Contains(viewAnswer1.Center) == true)
					{
						viewBox5.BackgroundColor = UIColor.FromRGB(182, 183, 189);
					}
					else
					{
						viewBox5.BackgroundColor = UIColor.FromRGB(214, 222, 230);
					}

                    viewAnswer1.Frame = new CGRect(newPoint, viewAnswer1.Frame.Size);
                }
                else if (viewAnswerIsMoving2)
                {
                    newPoint = new CGPoint(viewAnswer2.Frame.X - deltaX, viewAnswer2.Frame.Y - deltaY);

                    if (viewBox3.Frame.Contains(viewAnswer2.Center) == true)
                    {
                        viewBox3.BackgroundColor = UIColor.FromRGB(182, 183, 189);
                    }
                    else
                    {
                        viewBox3.BackgroundColor = UIColor.FromRGB(214, 222, 230);
                    }

					if (viewBox4.Frame.Contains(viewAnswer2.Center) == true)
					{
						viewBox4.BackgroundColor = UIColor.FromRGB(182, 183, 189);
					}
					else
					{
						viewBox4.BackgroundColor = UIColor.FromRGB(214, 222, 230);
					}

					if (viewBox5.Frame.Contains(viewAnswer2.Center) == true)
					{
						viewBox5.BackgroundColor = UIColor.FromRGB(182, 183, 189);
					}
					else
					{
						viewBox5.BackgroundColor = UIColor.FromRGB(214, 222, 230);
					}

                    viewAnswer2.Frame = new CGRect(newPoint, viewAnswer2.Frame.Size);
                }
            }
		}

		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);

			var touch = touches.AnyObject as UITouch;

            if (touch != null || viewAnswerIsMoving || viewAnswerIsMoving1 || viewAnswerIsMoving2)
            {
				if (viewAnswerIsMoving)
				{
                    if (!isFullBox3)
                    {
						if (viewBox3.Frame.Contains(viewAnswer.Center) == true)
						{
							viewAnswersCenterYviewBoxContraint.Active = false;
                           
                            viewAnswerCenterYviewBox4.Active = false;
							viewAnswerCenterYviewBox5.Active = false;
							viewAnswerCenterYviewBox3.Active = true;

							viewAnswer.Center = viewBox3.Center;
                            isFullBox3 = true;
						}
						else
						{
                            viewAnswerCenterYviewBox3.Active = false;
							isFullBox3 = false;
							if (!isFullBox4)
							{
								if (viewBox4.Frame.Contains(viewAnswer.Center) == true)
								{
									viewAnswersCenterYviewBoxContraint.Active = false;

                                    viewAnswerCenterYviewBox3.Active = false;
                                    viewAnswerCenterYviewBox5.Active = false;
									viewAnswerCenterYviewBox4.Active = true;

									viewAnswer.Center = viewBox4.Center;
									isFullBox4 = true;
								}
								else
								{
                                    viewAnswerCenterYviewBox4.Active = false;
									isFullBox4 = false;
									if (!isFullBox5)
									{
										if (viewBox5.Frame.Contains(viewAnswer.Center) == true)
										{
											viewAnswersCenterYviewBoxContraint.Active = false;

                                            viewAnswerCenterYviewBox3.Active = false;
                                            viewAnswerCenterYviewBox4.Active = false;
											viewAnswerCenterYviewBox5.Active = true;

											viewAnswer.Center = viewBox5.Center;
											isFullBox5 = true;
										}
										else
										{
                                            viewAnswerCenterYviewBox5.Active = false;
											viewAnswersCenterYviewBoxContraint.Active = true;
											isFullBox5 = false;
											viewAnswer.Center = viewBox.Center;
										}
									}
                                    else
                                    {
                                        viewAnswerCenterYviewBox5.Active = false;
										viewAnswersCenterYviewBoxContraint.Active = true;
										isFullBox5 = true;
										viewAnswer.Center = viewBox.Center;
									}
								}
							}
                            else
                            {
                                viewAnswerCenterYviewBox4.Active = false;
                                isFullBox4 = true;
								if (!isFullBox5)
								{
									if (viewBox5.Frame.Contains(viewAnswer.Center) == true)
									{
										viewAnswersCenterYviewBoxContraint.Active = false;

                                        viewAnswerCenterYviewBox3.Active = false;
                                        viewAnswerCenterYviewBox4.Active = false;
										viewAnswerCenterYviewBox5.Active = true;

										viewAnswer.Center = viewBox5.Center;
										isFullBox5 = true;
									}
									else
									{
                                        viewAnswerCenterYviewBox5.Active = false;
										viewAnswersCenterYviewBoxContraint.Active = true;
										isFullBox5 = false;
										viewAnswer.Center = viewBox.Center;
									}
								}
                                else
                                {
                                    viewAnswerCenterYviewBox5.Active = false;
                                    viewAnswersCenterYviewBoxContraint.Active = true;
									isFullBox5 = true;
									viewAnswer.Center = viewBox.Center;
								}
                            }
						}
                    }
                    else
                    {
                        isFullBox3 = true;
						if (!isFullBox4)
						{
							if (viewBox4.Frame.Contains(viewAnswer.Center) == true)
							{
								viewAnswersCenterYviewBoxContraint.Active = false;

                                viewAnswerCenterYviewBox3.Active = false;
                                viewAnswerCenterYviewBox5.Active = false;
								viewAnswerCenterYviewBox4.Active = true;

								viewAnswer.Center = viewBox4.Center;
								isFullBox4 = true;
							}
							else
							{
                                viewAnswerCenterYviewBox4.Active = false;
                                isFullBox4 = false;
								if (!isFullBox5)
								{
									if (viewBox5.Frame.Contains(viewAnswer.Center) == true)
									{
										viewAnswersCenterYviewBoxContraint.Active = false;

                                        viewAnswerCenterYviewBox3.Active = false;
                                        viewAnswerCenterYviewBox4.Active = false;
										viewAnswerCenterYviewBox5.Active = true;

										viewAnswer.Center = viewBox5.Center;
										isFullBox5 = true;
									}
									else
									{
                                        viewAnswerCenterYviewBox5.Active = false;
                                        viewAnswersCenterYviewBoxContraint.Active = true;
                                        isFullBox5 = false;
										viewAnswer.Center = viewBox.Center;
									}
								}
                                else
                                {
                                    viewAnswerCenterYviewBox5.Active = false;
                                    viewAnswersCenterYviewBoxContraint.Active = true;
									isFullBox5 = true;
									viewAnswer.Center = viewBox.Center;
                                }
							}
						}
                        else
                        {
                            isFullBox4 = true;
							if (!isFullBox5)
							{
								if (viewBox5.Frame.Contains(viewAnswer.Center) == true)
								{
									viewAnswersCenterYviewBoxContraint.Active = false;

                                    viewAnswerCenterYviewBox3.Active = false;
                                    viewAnswerCenterYviewBox4.Active = false;
									viewAnswerCenterYviewBox5.Active = true;
                                    
									viewAnswer.Center = viewBox5.Center;
									isFullBox5 = true;
								}
								else
								{
                                    viewAnswerCenterYviewBox5.Active = false;
                                    viewAnswersCenterYviewBoxContraint.Active = true;
                                    isFullBox5 = false;
									viewAnswer.Center = viewBox.Center;
								}
							}
                            else
                            {
                                viewAnswerCenterYviewBox5.Active = false;
                                viewAnswersCenterYviewBoxContraint.Active = true;
								isFullBox5 = true;
								viewAnswer.Center = viewBox.Center;
                            }
                        }
					}

					viewAnswerIsMoving = false;
					viewAnswer.Alpha = 1;
					viewAnswer.Layer.ShadowOffset = new CGSize(0, 2);
				}

				if (viewAnswerIsMoving1)
				{
					if (!isFullBox3)
					{
						if (viewBox3.Frame.Contains(viewAnswer1.Center) == true)
						{
							viewAnswers1CenterYviewBox1Contraint.Active = false;

                            viewAnswer1CenterYviewBox4.Active = false;
                            viewAnswer1CenterYviewBox5.Active = false;
							viewAnswer1CenterYviewBox3.Active = true;

							viewAnswer1.Center = viewBox3.Center;
							isFullBox3 = true;
						}
						else
						{
                            viewAnswer1CenterYviewBox3.Active = false;
							isFullBox3 = false;
							if (!isFullBox4)
							{
								if (viewBox4.Frame.Contains(viewAnswer1.Center) == true)
								{
									viewAnswers1CenterYviewBox1Contraint.Active = false;

                                    viewAnswer1CenterYviewBox3.Active = false;
                                    viewAnswer1CenterYviewBox5.Active = false;
									viewAnswer1CenterYviewBox4.Active = true;

									viewAnswer1.Center = viewBox4.Center;
									isFullBox4 = true;
								}
								else
								{
                                    viewAnswer1CenterYviewBox4.Active = false;
									isFullBox4 = false;
									if (!isFullBox5)
									{
										if (viewBox5.Frame.Contains(viewAnswer1.Center) == true)
										{
											viewAnswers1CenterYviewBox1Contraint.Active = false;

                                            viewAnswer1CenterYviewBox3.Active = false;
                                            viewAnswer1CenterYviewBox4.Active = false;
											viewAnswer1CenterYviewBox5.Active = true;

											viewAnswer1.Center = viewBox5.Center;
											isFullBox5 = true;
										}
										else
										{
                                            viewAnswer1CenterYviewBox5.Active = false;
                                            viewAnswers1CenterYviewBox1Contraint.Active = true;
											isFullBox5 = false;
											viewAnswer1.Center = viewBox1.Center;
										}
									}
									else
									{
                                        viewAnswer1CenterYviewBox5.Active = false;
										viewAnswers1CenterYviewBox1Contraint.Active = true;
										isFullBox5 = true;
										viewAnswer1.Center = viewBox1.Center;
									}
								}
							}
							else
							{
								isFullBox4 = true;
								if (!isFullBox5)
								{
									if (viewBox5.Frame.Contains(viewAnswer1.Center) == true)
									{
										viewAnswers1CenterYviewBox1Contraint.Active = false;

                                        viewAnswer1CenterYviewBox3.Active = false;
                                        viewAnswer1CenterYviewBox4.Active = false;
										viewAnswer1CenterYviewBox5.Active = true;

										viewAnswer1.Center = viewBox5.Center;
										isFullBox5 = true;
									}
									else
									{
                                        viewAnswer1CenterYviewBox5.Active = false;
										viewAnswers1CenterYviewBox1Contraint.Active = true;
										isFullBox5 = false;
										viewAnswer1.Center = viewBox1.Center;
									}
								}
								else
								{
                                    viewAnswer1CenterYviewBox5.Active = false;
                                    viewAnswers1CenterYviewBox1Contraint.Active = true;
									isFullBox5 = true;
									viewAnswer1.Center = viewBox1.Center;
								}
							}
						}
					}
					else
					{
						isFullBox3 = true;
						if (!isFullBox4)
						{
							if (viewBox4.Frame.Contains(viewAnswer1.Center) == true)
							{
								viewAnswers1CenterYviewBox1Contraint.Active = false;

                                viewAnswer1CenterYviewBox3.Active = false;
                                viewAnswer1CenterYviewBox5.Active = false;
								viewAnswer1CenterYviewBox4.Active = true;

								viewAnswer1.Center = viewBox4.Center;
								isFullBox4 = true;
							}
							else
							{
                                viewAnswer1CenterYviewBox4.Active = false;
								isFullBox4 = false;
								if (!isFullBox5)
								{
									if (viewBox5.Frame.Contains(viewAnswer1.Center) == true)
									{
										viewAnswers1CenterYviewBox1Contraint.Active = false;

                                        viewAnswer1CenterYviewBox3.Active = false;
                                        viewAnswer1CenterYviewBox4.Active = false;
										viewAnswer1CenterYviewBox5.Active = true;

										viewAnswer1.Center = viewBox5.Center;
										isFullBox5 = true;
									}
									else
									{
                                        viewAnswer1CenterYviewBox5.Active = false;
                                        viewAnswers1CenterYviewBox1Contraint.Active = true;
										isFullBox5 = false;
										viewAnswer1.Center = viewBox1.Center;
									}
								}
								else
								{
                                    viewAnswer1CenterYviewBox5.Active = false;
                                    viewAnswers1CenterYviewBox1Contraint.Active = true;
									isFullBox5 = true;
									viewAnswer1.Center = viewBox1.Center;
								}
							}
						}
						else
						{
							isFullBox4 = true;
							if (!isFullBox5)
							{
								if (viewBox5.Frame.Contains(viewAnswer1.Center) == true)
								{
									viewAnswers1CenterYviewBox1Contraint.Active = false;

                                    viewAnswer1CenterYviewBox3.Active = false;
                                    viewAnswer1CenterYviewBox4.Active = false;
									viewAnswer1CenterYviewBox5.Active = true;

									viewAnswer1.Center = viewBox5.Center;
									isFullBox5 = true;
								}
								else
								{
                                    viewAnswer1CenterYviewBox5.Active = false;
                                    viewAnswers1CenterYviewBox1Contraint.Active = true;
									isFullBox5 = false;
									viewAnswer1.Center = viewBox1.Center;
								}
							}
							else
							{
                                viewAnswer1CenterYviewBox5.Active = false;
                                viewAnswers1CenterYviewBox1Contraint.Active = true;
								isFullBox5 = true;
								viewAnswer1.Center = viewBox1.Center;
							}
						}
					}

					viewAnswerIsMoving1 = false;
					viewAnswer1.Alpha = 1;
					viewAnswer1.Layer.ShadowOffset = new CGSize(0, 2);
				}

				if (viewAnswerIsMoving2)
				{
					if (!isFullBox3)
					{
						if (viewBox3.Frame.Contains(viewAnswer2.Center) == true)
						{
							viewAnswers2CenterYviewBox2Contraint.Active = false;
							
							viewAnswer2CenterYviewBox4.Active = false;
							viewAnswer2CenterYviewBox5.Active = false;
							viewAnswer2CenterYviewBox3.Active = true;

							viewAnswer2.Center = viewBox3.Center;
							isFullBox3 = true;
						}
						else
						{
                            viewAnswer2CenterYviewBox3.Active = false;
							isFullBox3 = false;
							if (!isFullBox4)
							{
								if (viewBox4.Frame.Contains(viewAnswer2.Center) == true)
								{
									viewAnswers2CenterYviewBox2Contraint.Active = false;

									viewAnswer2CenterYviewBox3.Active = false;
									viewAnswer2CenterYviewBox5.Active = false;
									viewAnswer2CenterYviewBox4.Active = true;

									viewAnswer2.Center = viewBox4.Center;
									isFullBox4 = true;
								}
								else
								{
									viewAnswer2CenterYviewBox4.Active = false;
									isFullBox4 = false;
									if (!isFullBox5)
									{
										if (viewBox5.Frame.Contains(viewAnswer2.Center) == true)
										{
											viewAnswers2CenterYviewBox2Contraint.Active = false;

                                            viewAnswer2CenterYviewBox3.Active = false;
                                            viewAnswer2CenterYviewBox4.Active = false;
											viewAnswer2CenterYviewBox5.Active = true;

											viewAnswer2.Center = viewBox5.Center;
											isFullBox5 = true;
										}
										else
										{
											viewAnswer2CenterYviewBox5.Active = false;
                                            viewAnswers2CenterYviewBox2Contraint.Active = true;
											isFullBox5 = false;
											viewAnswer2.Center = viewBox2.Center;
										}
									}
									else
									{
										viewAnswer2CenterYviewBox5.Active = false;
                                        viewAnswers2CenterYviewBox2Contraint.Active = true;
										isFullBox5 = true;
										viewAnswer2.Center = viewBox2.Center;
									}
								}
							}
							else
							{
								isFullBox4 = true;
								if (!isFullBox5)
								{
									if (viewBox5.Frame.Contains(viewAnswer2.Center) == true)
									{
										viewAnswers2CenterYviewBox2Contraint.Active = false;

                                        viewAnswer2CenterYviewBox3.Active = false;
                                        viewAnswer2CenterYviewBox4.Active = false;
										viewAnswer2CenterYviewBox5.Active = true;

										viewAnswer2.Center = viewBox5.Center;
										isFullBox5 = true;
									}
									else
									{
										viewAnswer2CenterYviewBox5.Active = false;
                                        viewAnswers2CenterYviewBox2Contraint.Active = true;
										isFullBox5 = false;
										viewAnswer2.Center = viewBox2.Center;
									}
								}
								else
								{
									viewAnswer2CenterYviewBox5.Active = false;
                                    viewAnswers2CenterYviewBox2Contraint.Active = true;
									isFullBox5 = true;
									viewAnswer2.Center = viewBox2.Center;
								}
							}
						}
					}
					else
					{
						isFullBox3 = true;
						if (!isFullBox4)
						{
							if (viewBox4.Frame.Contains(viewAnswer2.Center) == true)
							{
								viewAnswers2CenterYviewBox2Contraint.Active = false;

								viewAnswer2CenterYviewBox3.Active = false;
								viewAnswer2CenterYviewBox5.Active = false;
								viewAnswer2CenterYviewBox4.Active = true;

								viewAnswer2.Center = viewBox4.Center;
								isFullBox4 = true;
							}
							else
							{
                                viewAnswer2CenterYviewBox4.Active = false;
								isFullBox4 = false;
								if (!isFullBox5)
								{
									if (viewBox5.Frame.Contains(viewAnswer2.Center) == true)
									{
										viewAnswers2CenterYviewBox2Contraint.Active = false;

										viewAnswer2CenterYviewBox3.Active = false;
										viewAnswer2CenterYviewBox4.Active = false;
										viewAnswer2CenterYviewBox5.Active = true;

										viewAnswer2.Center = viewBox5.Center;
										isFullBox5 = true;
									}
									else
									{
                                        viewAnswer2CenterYviewBox5.Active = false;
                                        viewAnswers2CenterYviewBox2Contraint.Active = true;
										isFullBox5 = false;
										viewAnswer2.Center = viewBox2.Center;
									}
								}
								else
								{
                                    viewAnswer2CenterYviewBox5.Active = false;
                                    viewAnswers2CenterYviewBox2Contraint.Active = true;
									isFullBox5 = true;
									viewAnswer2.Center = viewBox2.Center;
								}
							}
						}
						else
						{
							isFullBox4 = true;
							if (!isFullBox5)
							{
								if (viewBox5.Frame.Contains(viewAnswer2.Center) == true)
								{
									viewAnswers2CenterYviewBox2Contraint.Active = false;

									viewAnswer2CenterYviewBox3.Active = false;
									viewAnswer2CenterYviewBox4.Active = false;
									viewAnswer2CenterYviewBox5.Active = true;

									viewAnswer2.Center = viewBox5.Center;
									isFullBox5 = true;
								}
								else
								{
                                    viewAnswer2CenterYviewBox5.Active = false;
                                    viewAnswers2CenterYviewBox2Contraint.Active = true;
									isFullBox5 = false;
									viewAnswer2.Center = viewBox2.Center;
								}
							}
							else
							{
                                viewAnswer2CenterYviewBox5.Active = false;
                                viewAnswers2CenterYviewBox2Contraint.Active = true;
								isFullBox5 = true;
								viewAnswer2.Center = viewBox2.Center;
							}
						}
					}

					viewAnswerIsMoving2 = false;
					viewAnswer2.Alpha = 1;
					viewAnswer2.Layer.ShadowOffset = new CGSize(0, 2);
				}
            }

			viewBox3.BackgroundColor = UIColor.FromRGB(214, 222, 230);
			viewBox4.BackgroundColor = UIColor.FromRGB(214, 222, 230);
			viewBox5.BackgroundColor = UIColor.FromRGB(214, 222, 230);
		}

		public override void TouchesCancelled(NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);

			//viewAnswerIsMoving = false;
			//viewAnswer.Alpha = 1;
			//viewAnswer.Layer.ShadowOffset = new CGSize(0, 2);

			//viewAnswerIsMoving1 = false;
			//viewAnswer1.Alpha = 1;
			//viewAnswer1.Layer.ShadowOffset = new CGSize(0, 2);

			//viewAnswerIsMoving2 = false;
			//viewAnswer2.Alpha = 1;
			//viewAnswer2.Layer.ShadowOffset = new CGSize(0, 2);

			//viewBox3.BackgroundColor = UIColor.FromRGB(214, 222, 230);
			//viewBox4.BackgroundColor = UIColor.FromRGB(214, 222, 230);
			//viewBox5.BackgroundColor = UIColor.FromRGB(214, 222, 230);
		}
    }
}