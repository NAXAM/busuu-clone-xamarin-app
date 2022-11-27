using System;
using System.Collections.Generic;
using Naxam.Busuu.Learning.Models;
using System.Threading.Tasks;

namespace Naxam.Busuu.Learning.Services
{
    public class LearningService : ILearningService
    {
        public Task<LessonModel[]> GetAllLesson()
        {
            Random random = new Random();
            string[] color =
            {
                "#58B0F8","#B02018","#FEAB35"
            };

            string[] titlesLesson = {
                "Hello",
                "How are you?",
                "What's your name?",
                "Review 1-3",
                "What does he look like?",
                "What do you do?",
                "Do you have any brother or sisters?",
                "Where are you from?",
                "Review 4-7",
                "That's mine!",
                "Where do you live?",
                "Is there a supermarket near here?",
                "Review 8-10",
                "Do you want something to eat?",
                "Eat your vegetables!",
                "Review 11-13",
                "What do you do in your free time?",
                "Can you play any sport?",
                "What are you doing?",
                "When do you get up?",
                "Review 14-17"

            };

            string[] icons =
            {
                "http://app4e.net/busuu/lesson1.png",
                "http://app4e.net/busuu/lesson2.png",
                "http://app4e.net/busuu/lesson3.png",
            };

            List<LessonModel> Lessons = new List<LessonModel>();

            for (int i = 0; i < 16; i++)
            {
                var lesson = new LessonModel(GetTopic(color[i % 3]))
                {
                    Id = i,
                    LessonNumber = "Lesson " + (i + 1),
                    LessonName = titlesLesson[i],
                    Color = color[i % 3],
                    Percent = random.Next(1, 100),
                    Icon = icons[i % 3]
                };
                Lessons.Add(lesson);
            }

            return Task.FromResult(Lessons.ToArray());
        }

        public Task<ExerciseModel> GetExerciseByiId(int id)
        {
            ExerciseModel.ExerciseType[] lstType = {
                ExerciseModel.ExerciseType.Memorise,
                ExerciseModel.ExerciseType.Dialogue,
                ExerciseModel.ExerciseType.Vocabulary
            };
            ExerciseModel exercise = new ExerciseModel
            {
                ExerciseId = id,
                Type = lstType[id % lstType.Length]
            };
            return Task.FromResult(exercise);
        }

        public Task<TipModel> GetTipByUnit(UnitModel unit)
        {
            List<string> lstTip = new List<string>() { "mot hai ba", "ba bon ngay" };
            TipModel tip = new TipModel
            {
                Tip = "\"Hi\" is a slightly more casual way of saying \"hello\". However, we use both words and we can say both at any time of day.",
                Samples = lstTip,
                Detail = "\"Hi\" is a slightly more casual way of saying \"hello\". However, we use both words and we can say both at any time of day."
            };
            return Task.FromResult(tip);
        }

        public async Task<UnitModel[]> GetUnitByExercise(ExerciseModel ex)
        {
            Random random = new Random();
            UnitModel.UnitType[] lstType = {
                UnitModel.UnitType.ChooseWord,
                UnitModel.UnitType.CompleteSentence,
                //UnitModel.UnitType.ConversationSentence,
                UnitModel.UnitType.FillSentence,
                UnitModel.UnitType.HearAndRepeat,
                UnitModel.UnitType.MatchingSentence,
                //UnitModel.UnitType.OrderWord,
                UnitModel.UnitType.SelectWord,
                UnitModel.UnitType.SelectWordImage,
                UnitModel.UnitType.TrueFalseQuestion
            };

            ex = ex ?? new ExerciseModel();


            if (ex.Type == ExerciseModel.ExerciseType.Dialogue)
            {
                List<UnitModel> lstUnit = new List<UnitModel>();
                lstUnit.Add(new UnitModel
                {
                    Title = "Jack",
                    Inputs = new List<string> { "hello, %% Jack" },
                    Images = new List<string> { "http://newsen.vn/data/news/2015/3/11/17/So-Ji-Sub-bat-ngo-tai-xuat-man-anh-rong-Newsen-vn-0-1426043851.jpg" },
                    Answers = new List<AnswerModel> { new AnswerModel {
                    Text = "I'm",
                    Value = true,
                } },
                    Audios = new List<AudioModel> {
                    new AudioModel{ Link="" }
                }
                });
                lstUnit.Add(new UnitModel
                {
                    Title = "Martha",
                    Inputs = new List<string> { "%%, I'm Martha" },
                    Images = new List<string> { "http://eva-img.24hstatic.com/upload/2-2017/images/2017-04-20/1492655255-chi-pu.jpg" },
                    Answers = new List<AnswerModel> { new AnswerModel {
                    Text = "Hi",
                    Value = true,
                } },
                    Audios = new List<AudioModel> {
                    new AudioModel{ Link="" }
                }
                });
                lstUnit.Add(new UnitModel
                {
                    Title = "Jack",
                    Inputs = new List<string> { "%%. %%" },
                    Images = new List<string> { "http://newsen.vn/data/news/2015/3/11/17/So-Ji-Sub-bat-ngo-tai-xuat-man-anh-rong-Newsen-vn-0-1426043851.jpg" },
                    Answers = new List<AnswerModel> { new AnswerModel {
                    Text = "Nice to meet you",
                    Value = true,
                },
                    new AnswerModel {
                    Text = "How's it going ?",
                    Value = true,
                    Position = 1
                }},
                    Audios = new List<AudioModel> {
                    new AudioModel{ Link="" }
                }
                });
                lstUnit.Add(new UnitModel
                {
                    Title = "Martha",
                    Inputs = new List<string> { "%%" },
                    Images = new List<string> { "http://eva-img.24hstatic.com/upload/2-2017/images/2017-04-20/1492655255-chi-pu.jpg" },
                    Answers = new List<AnswerModel> { new AnswerModel {
                    Text = "Fine, thanks",
                    Value = true,
                } },
                    Audios = new List<AudioModel> {
                    new AudioModel{ Link="" }
                }
                });
                return lstUnit.ToArray();
            }

            List<UnitModel> listUnit = new List<UnitModel>();

            for (int i = 0; i < 7; i++)
            {
                listUnit.Add(await GetUnitByType(lstType[random.Next(1, 100) % lstType.Length]));
            }
            ex.Units = listUnit;

            return listUnit.ToArray();
        }

        public async Task<UnitModel> GetUnitByType(UnitModel.UnitType type)
        {
            if (type == UnitModel.UnitType.HearAndRepeat)
			{
				return new UnitModel
				{
					Title = "Select the correct option",
					Type = type,
					Images = new List<string>
					{
						"http://assets.nydailynews.com/polopoly_fs/1.1710480.1393962541!/img/httpImage/image.jpg_gen/derivatives/landscape_1200/72964838.jpg"
					},
					Inputs = new List<string> {
						"Select the correct option",
					},
					Answers = new List<AnswerModel>
					{
						new AnswerModel
						{
							Text = "Spain",
							Value = true,
						}
					}
				};
			}

			if (type == UnitModel.UnitType.TrueFalseQuestion)
			{
				return new UnitModel
				{
					Title = "Select the correct option",
					Type = type,
					Images = new List<string>
					{
						"http://assets.nydailynews.com/polopoly_fs/1.1710480.1393962541!/img/httpImage/image.jpg_gen/derivatives/landscape_1200/72964838.jpg"
					},
					Inputs = new List<string> {
						"Select the correct option",
					},
					Answers = new List<AnswerModel>
					{
						new AnswerModel
						{
							Text = "Spain",
							Value = true,
						}
					}
				};
			}

            if (type == UnitModel.UnitType.SelectWordImage)
            {
                return new UnitModel
                {
                    Title = "Select the correct option",
                    Type = type,
                    Audios = new List<AudioModel> {
                        new AudioModel
                        {
                            Link = "https://s-media-cache-ak0.pinimg.com/originals/f2/8e/13/f28e130166dac6b5a0d2cd43f147ab4b.jpg"
                        }
                    },
                    Answers = new List<AnswerModel>
                    {
                        new AnswerModel
                        {
                            Text = "Spain",
                            Value = true,
                            Image = "http://www.planetware.com/photos-large/E/spain-granada-alhambra-4.jpg"
                        },
                        new AnswerModel
                        {
                            Text = "England",
                            Image = "https://www.raileurope.com/cms-images/400/91/britrail-overview-1.jpg"
                        },
                        new AnswerModel
                        {
                            Text = "France",
                            Image = "http://www.telegraph.co.uk/content/dam/Travel/leadAssets/30/20/France-image-858_3020641a.jpg"
                        }
                    }
                };
            }

            if (type == UnitModel.UnitType.MatchingSentence)
            {
                return new UnitModel
                {
                    Title = "Match the pair of words/ phareses",
                    Type = type,
                    Answers = new List<AnswerModel>
                    {
                        new AnswerModel
                        {
                            Text = "già"
                        },
                        new AnswerModel
                        {
                            Text = "cao"
                        },
                        new AnswerModel
                        {
                            Text = "trẻ"
                        }
                    },
                    Inputs = new List<string>
                    {
                        "old","tall","young"
                    }
                };
            }

            if (type == UnitModel.UnitType.OrderWord)
            {
                return new UnitModel
                {
                    Title = "Match the pair of words/ phareses",
                    Type = type,
                    Inputs = new List<string>
                    {
                        "What is your name?"
                    }
                };
            }

            if (type == UnitModel.UnitType.ChooseWord)
            {
                return new UnitModel
                {
                    Title = "Select the opposite of \"tall\"",
                    Type = type,
                    Images = new List<string>
                    {
                        "http://assets.nydailynews.com/polopoly_fs/1.1710480.1393962541!/img/httpImage/image.jpg_gen/derivatives/landscape_1200/72964838.jpg"
                    },
                    Audios = new List<AudioModel> {
                        new AudioModel
                        {
                            Link = "https://s-media-cache-ak0.pinimg.com/originals/f2/8e/13/f28e130166dac6b5a0d2cd43f147ab4b.jpg"
                        }
                    },
                    Answers = new List<AnswerModel>
                    {
                        new AnswerModel
                        {
                            Text = "tall"
                        },
                        new AnswerModel
                        {
                            Text = "old"
                        },
                        new AnswerModel
                        {
                            Text = "short",
                            Value= true
                        }
                    }
                };
            }

            if (type == UnitModel.UnitType.CompleteSentence)
            {
                return new UnitModel
                {
                    Title = "Hoàn thành câu để hỏi chính xác",
                    Type = type,
                    Images = new List<string>
                    {
                        "http://assets.nydailynews.com/polopoly_fs/1.1710480.1393962541!/img/httpImage/image.jpg_gen/derivatives/landscape_1200/72964838.jpg"
                    },
                    Audios = new List<AudioModel> {
                        new AudioModel
                        {
                            Link = "https://s-media-cache-ak0.pinimg.com/originals/f2/8e/13/f28e130166dac6b5a0d2cd43f147ab4b.jpg"
                        }
                    },
                    Answers = new List<AnswerModel>
                    {
                        new AnswerModel
                        {
                            Text = "Where"
                        }
                    },
                    Inputs = new List<string>
                    {
                        "%% are you from?",
                    }
                };
            }

            if (type == UnitModel.UnitType.FillSentence)
            {
                return new UnitModel
                {
                    Title = "Chạm vào (các) từ đúng để hoàn thành câu",
                    Type = type,
                    Images = new List<string>
                    {
                        "http://assets.nydailynews.com/polopoly_fs/1.1710480.1393962541!/img/httpImage/image.jpg_gen/derivatives/landscape_1200/72964838.jpg"
                    },
                    Answers = new List<AnswerModel>
                    {
                        new AnswerModel
                        {
                            Text = "Where",
                            Value = true,
                            Position = 0
                        },
                        new AnswerModel
                        {
                            Text = "What"
                        },
                        new AnswerModel
                        {
                            Text = "How",
                            Value = true,
                            Position = 1
                        },
                    },
                    Inputs = new List<string>
                    {
                        "%% are you %% from? hello",
                    }
                };
            }

            if (type == UnitModel.UnitType.Unknow)
            {
                return new UnitModel
                {
                    Title = "Select the words in the correct order",
                    Type = type,
                    Audios = new List<AudioModel> {
                        new AudioModel
                        {
                            Link = "https://s-media-cache-ak0.pinimg.com/originals/f2/8e/13/f28e130166dac6b5a0d2cd43f147ab4b.jpg"
                        }
                    },
                    Answers = new List<AnswerModel>
                    {
                        new AnswerModel
                        {
                            Text = "Bố tôi là người Tây Ban Nha"
                        }
                    },
                    Inputs = new List<string>
                    {
                        "My dad is Spanish."
                    }
                };
            }


            if (type == UnitModel.UnitType.SelectWord)
            {
                return new UnitModel
                {
                    Title = "Chọn từ có nghĩa là \"đàn bà\"",
                    Type = type,
                    Images = new List<string> {
                        "https://www.codeproject.com/KB/GDI-plus/ImageProcessing2/flip.jpg"
                    },
                    Answers = new List<AnswerModel>
                    {
                        new AnswerModel
                        {
                            Text = "man"
                        },
                        new AnswerModel
                        {
                            Text = "boy"
                        },
                        new AnswerModel
                        {
                            Text = "girl",
                            Value = true
                        },
                        new AnswerModel
                        {
                            Text = "friend"
                        },
                        new AnswerModel
                        {
                            Text = "woman",
                            Value = true
                        },
                    },
                };
            }

            return new UnitModel
            {
                Title = "Chọn từ đúng",
                Type = type,
                Tip = await GetTipByUnit(null),
                Inputs = new List<string>
                {
                    "Tôi là %% người %%  trai nhất naxam"
                },
                Images = new List<string>
                    {
                         "https://s-media-cache-ak0.pinimg.com/originals/f2/8e/13/f28e130166dac6b5a0d2cd43f147ab4b.jpg",
                    },
                Audios = new List<AudioModel> {
                    new AudioModel
                    {
                        Link = "https://s-media-cache-ak0.pinimg.com/originals/f2/8e/13/f28e130166dac6b5a0d2cd43f147ab4b.jpg"
                    }
                },
                Answers = new List<AnswerModel>
                {
                    new AnswerModel
                    {
                        Text = "thảo",
                        Value = true,
                        Image = "https://s-media-cache-ak0.pinimg.com/originals/f2/8e/13/f28e130166dac6b5a0d2cd43f147ab4b.jpg"
                    },
                    new AnswerModel
                    {
                        Text = "xấu",
                        Value  = true,
                        Position = 1,
                        Image = "https://s-media-cache-ak0.pinimg.com/originals/f2/8e/13/f28e130166dac6b5a0d2cd43f147ab4b.jpg"
                    },
                    new AnswerModel
                    {
                        Text = "đẹp",
                        Image = "https://s-media-cache-ak0.pinimg.com/originals/f2/8e/13/f28e130166dac6b5a0d2cd43f147ab4b.jpg"
                    }
                }
            };
        }

        private List<TopicModel> GetTopic(string color)
        {
            Random random = new Random();
            List<TopicModel> Topicsx = new List<TopicModel>();
            for (int i = 0; i < 4; i++)
            {
                Topicsx.Add(new TopicModel
                {
                    Toppic = "Topic " + random.Next(1, 1000),
                    Time = random.Next(1, 50),
                    Color = color,
                    Exercises = new List<ExerciseModel>
                    {
                        new ExerciseModel{
                            ExerciseId = random.Next(1,1000),
                            Type = ExerciseModel.ExerciseType.Dialogue,
                            IsDone = true,
                            Color = color,
                            Name = "Cai Gi Do AI Biet   ",
                        },
                        new ExerciseModel{
                            ExerciseId = random.Next(1,1000),
                            Type = ExerciseModel.ExerciseType.Vocabulary,
                            Color = color,
                            Name = "Cai Gi Do AI Biet Duoc",
                        },
                        new ExerciseModel{
                            ExerciseId = random.Next(1,1000),
                            Type = ExerciseModel.ExerciseType.Memorise,
                            Color = color,
                            Name = "Cai Gi Do AI Biet Duoc",
                        }
                    }
                });
            }
            return Topicsx;
        }
    }
}
