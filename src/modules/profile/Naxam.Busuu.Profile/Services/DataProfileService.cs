using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Naxam.Busuu.Core.Models;

namespace Naxam.Busuu.Profile.Services
{
    public class DataProfileService : IDataProfileService
    {
        LanguageLevel[] level = {
            LanguageLevel.Beginner,LanguageLevel.Intermediate,LanguageLevel.Advanced,LanguageLevel.Native
        };
        string[] name = {
            "Jackie Chan","Jet Li","Jame Bond","Lee Jun Ki","Jacson","So Ji Sub","Kwon Sang Woo","Ly Hai","Stenphen Chow","Ngo Kinh"
        };
        string[] avatar = {
            "https://cdn.pixabay.com/photo/2013/12/01/11/38/buddha-221741_960_720.jpg",
            "https://cdn.pixabay.com/photo/2014/02/23/09/17/thinking-272677_960_720.jpg",
            "https://cdn.pixabay.com/photo/2013/11/21/14/15/sad-214977_960_720.jpg",
            "https://cdn.pixabay.com/photo/2013/02/21/19/02/baby-boy-84489_960_720.jpg",
            "https://cdn.pixabay.com/photo/2015/10/04/21/15/old-man-971889_960_720.jpg",
            "https://cdn.pixabay.com/photo/2015/02/13/21/25/kid-635811_960_720.jpg",
            "http://maxpixel.freegreatpicture.com/static/photo/1x/Stress-Man-Stressed-Man-Person-Image-Headache-1557872.jpg",
            "https://cdn.pixabay.com/photo/2016/04/07/21/48/boy-1314845_960_720.jpg",
            "https://upload.wikimedia.org/wikipedia/en/4/47/JimTilley_Image.JPG",
            "https://cdn.pixabay.com/photo/2015/06/17/20/13/caricature-812991_960_720.jpg"
        };
        string[] country = {
            "VietNam","Korean","England","Laos","Thailand","US","Canada","Mexico","Brazil","Japan"
        };
        string[] languages = {
            "Tiếng Việt","English","简体中文","繁體中文","Français","日本語","Español","Português","Deutsch"
        };
        string[] flag = {
            "flag_small_english","flag_small_english","flag_small_english",
            "flag_small_english","flag_small_english","flag_small_english","flag_small_english",
            "flag_small_english","flag_small_english","flag_small_english"
        };
        string[] cornerflag = {
            "english_corner","english_corner","english_corner",
            "english_corner","english_corner","english_corner","english_corner",
            "english_corner","english_corner","english_corner"
        };
        string[] write = {
            "What is your name?","Where are you from?","How old are you?","I am a big fan of jackie chan",
            "i love movie of stephen chow","i am not handsome but i am very tall","i play game very good","today, i feed very hot and i am tired",
            "i love naxam, naxam is a big company, this has 100000 developers"
        };
        string[] reply = {
            "Good","I love you","you are beautiful","are you stupid?",
            "are you kidding me?","thanks god","The Naxam is best company binding in Vietnam",
            "i love naxam, naxam is a big company, this has 100000 developers"
        };

        public Task<CountryModel[]> GetAllCountry()
        {
            List<CountryModel> lst = new List<CountryModel>();
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                lst.Add(new CountryModel
                {
                    Country = "Country" + random.Next(1, 1000),
                    PhoneCode = (i + 1) + ""
                });
            }

            return Task.FromResult(lst.ToArray());
        }

        public Task<LanguageModel[]> GetAllLanguage()
        {
            Random random = new Random();
            List<LanguageModel> lst = new List<LanguageModel>();
            for (int i = 0; i < 10; i++)
            {
                lst.Add(new LanguageModel
                {
                    Flag = flag[random.Next(1, 100) % flag.Length],
                    Language = languages[random.Next(1, 100) % languages.Length]
                });
            }

            return Task.FromResult(lst.ToArray());
        }

        public Task<SocialModel[]> GetCorrections(UserModel user)
        {
            Random random = new Random();
            List<SocialModel> lst = new List<SocialModel>();
            for (int i = 0; i < 100; i++)
            {
                SocialModel social = new SocialModel
                {
                    Id = i + 1,
                    Star = random.NextDouble(),
                    Friends = random.Next(0, 100) % 2 == 0,
                    ImgQuestion = "http://www.conceptcarz.com/images/Jaguar/Jaguar-F-Pace-First-Edition-2015-Image-03.jpg",
                    TextQuestion = "Say hello Naxam!",
                    User = GetRandomUser(random),
                    DatePosted = DateTime.Now.AddDays(random.Next(0, 30)).AddHours(random.Next(0, 23)).AddMinutes(random.Next(0, 59)).AddSeconds(random.Next(0, 59)),
                    Type = random.Next(0, 100) % 2 == 0 ? SocialModel.SocialType.Speaking : SocialModel.SocialType.Writing,
                    Content = write[random.Next(0, 100) % write.Length],
                    Feedbacks = new List<FeedbackModel>{
                        new FeedbackModel
                        {
                            Likes = new List<UserModel>{
                                new UserModel{},new UserModel{}
                            },
                            Rating = random.Next(1,5)
                        }
                    }
                };
                lst.Add(social);
            }
            return Task.FromResult(lst.ToArray());
        }

        public Task<SocialModel[]> GetExercises(UserModel user)
        {
            Random random = new Random();
            List<SocialModel> lst = new List<SocialModel>();
            for (int i = 0; i < 100; i++)
            {
                SocialModel social = new SocialModel
                {
                    Id = i + 1,
                    Star = random.NextDouble(),
                    Friends = random.Next(0, 100) % 2 == 0,
                    ImgQuestion = "http://www.conceptcarz.com/images/Jaguar/Jaguar-F-Pace-First-Edition-2015-Image-03.jpg",
                    TextQuestion = "Say hello Naxam!",
                    User = new UserModel
                    {
                        Name = user.Name,
                        Photo = user.Photo,
                        Country = user.Country,
                        SpeakLanguages = new List<LanguageModel>
                        {
                            new LanguageModel
                            {
                                Language = languages[random.Next(0, 100) % languages.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length]
                            },
                            new LanguageModel
                            {
                                Language = languages[random.Next(0, 100) % languages.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length]
                            }
                        },
                        Languages = new List<LanguageModel>
                        {
                            new LanguageModel
                            {
                                Language = languages[random.Next(0, 100) % languages.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length],
                                HalfFlag =cornerflag[random.Next(0, 100) % cornerflag.Length]
                            }
                        },
                    },
                    DatePosted = DateTime.Now.AddDays(random.Next(0, 30)).AddHours(random.Next(0, 23)).AddMinutes(random.Next(0, 59)).AddSeconds(random.Next(0, 59)),
                    Type = random.Next(0, 100) % 2 == 0 ? SocialModel.SocialType.Speaking : SocialModel.SocialType.Writing,
                    Content = write[random.Next(0, 100) % write.Length]
                };
                lst.Add(social);
            }
            return Task.FromResult(lst.ToArray());
        }

        public UserModel GetRandomUser(Random random)
        {
            return new UserModel
            {
                Id = random.Next(0, 100),
                Name = name[random.Next(0, 100) % name.Length],
                Photo = avatar[random.Next(0, 100) % avatar.Length],
          
                Country = new CountryModel
                {
                    Country = country[random.Next(0, 100) % country.Length],
                },
                SpeakLanguages = new List<LanguageModel>
                        {
                            new LanguageModel
                            {
                                Language = languages[random.Next(0, 100) % languages.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length]
                            },
                            new LanguageModel
                            {
                                Language = languages[random.Next(0, 100) % languages.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length]
                            }
                        },
                Languages = new List<LanguageModel>
                        {
                            new LanguageModel
                            {
                                Language = languages[random.Next(0, 100) % languages.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length],
                                HalfFlag =cornerflag[random.Next(0, 100) % cornerflag.Length]
                            }
                        },
            };
        }

        public List<UserModel> GetRandomListUser()
        {
            Random random = new Random();
            List<UserModel> lst = new List<UserModel>();
            for (int i = 0; i < 20; i++)
            {
                lst.Add(GetRandomUser(random));
            }
            return lst;
        }

        public Task<UserModel> GetUser(int id)
        {
            return Task.FromResult(new UserModel
            {
                Id = id,
                Name = name[id % name.Length],
                Photo = "https://scontent.fhan2-1.fna.fbcdn.net/v/t1.0-9/16508930_1170067123106485_5721616632039463605_n.jpg?oh=4dc4296fe19f805057c4ff9834f10224&oe=5A342AE1",
                Country = new CountryModel
                {
                    Country = country[id % country.Length]
                },
                Gender = Gender.Undisclosed,
                SpeakLanguages = new List<LanguageModel> {
                    new LanguageModel
                    {
                        Language = languages[id%languages.Length]
                    },
                     new LanguageModel
                    {
                        Language = languages[id%languages.Length]
                    },
                },
                Languages = new List<LanguageModel> {
                    new LanguageModel
                    {
                        Language = languages[id%languages.Length],
                        Flag = "flag_small_english"
                    }
                },
                interfaceLanguage = new LanguageModel
                {
                    Language = "English"
                },
                Friends = GetRandomListUser(),
                FriendsRequest = GetRandomListUser()
            });
        }
    }
}
