using System.Linq;
using System.Collections.Generic;
using System;
using Naxam.Busuu.Core.Models;
using System.Threading.Tasks;

namespace Naxam.Busuu.Social.Services
{
    public class DataSocial : IDataSocial
    {
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
        public Task<SocialModel[]> GetAllSocial()
        {
            Random random = new Random();

            List<SocialModel> lst = new List<SocialModel>();
            for (int i = 0; i < 100; i++)
            {
                SocialModel social = new SocialModel
                {
                    Id = i + 1,
                    ImageSpeakLanguage = "flag_small_english.png",
                    ImageLearn = "flag_small_english.png",
                    TextLearn = "ENGLISH",
                    Star = random.NextDouble(),
                    Friends = random.Next(0, 100) % 2 == 0,
                    ImgQuestion = "http://znews-photo-td.zadn.vn/w1024/Uploaded/rik_rdcvcvwt_wc/2016_03_30/a3.jpg",
                    TextQuestion = "Say hello Naxam!",
                    User = GetRandomUser(random),
                    DatePosted = DateTime.Now.AddDays(random.Next(0, 30)).AddHours(random.Next(0, 23)).AddMinutes(random.Next(0, 59)).AddSeconds(random.Next(0, 59)),
                    Type = random.Next(0, 100) % 2 == 0 ? SocialModel.SocialType.Speaking : SocialModel.SocialType.Writing,
                    Content = write[random.Next(0, 100) % write.Length]
                };
                lst.Add(social);
            }

            return Task.FromResult(lst.ToArray());
        }

        public async Task<SocialModel[]> GetDiscoverSocial()
        {
            return (await GetAllSocial()).Where(d => !d.Friends).ToArray();
        }

        public async Task<SocialModel[]> GetFriendSocial()
        {
            return (await GetAllSocial()).Where(d => d.Friends).ToArray();
        }

        public async Task<SocialModel> GetSocialById(int id)
        {
            return(await GetAllSocial()).Where(d => d.Id == id).FirstOrDefault();
        }

        public UserModel GetRandomUser(Random random)
        {
            return new UserModel
            {
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
                                Language = country[random.Next(0, 100) % country.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length]
                            },
                            new LanguageModel
                            {
                                Language = country[random.Next(0, 100) % country.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length]
                            }
                        },
                Languages = new List<LanguageModel>
                        {
                            new LanguageModel
                            {
                                Language = country[random.Next(0, 100) % country.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length],
                                HalfFlag =cornerflag[random.Next(0, 100) % cornerflag.Length]
                            }
                        },
            };
        }

        public Task<FeedbackModel[]> GetFeedbackById(int id)
        {
            Random random = new Random();

            List<UserModel> lstUser = new List<UserModel>();
            for (int i = 0; i < 10; i++)
            {
                lstUser.Add(new UserModel
                {
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
                                Language = country[random.Next(0, 100) % country.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length]
                            },
                            new LanguageModel
                            {
                                Language = country[random.Next(0, 100) % country.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length]
                            }
                        },
                    Languages = new List<LanguageModel>
                        {
                            new LanguageModel
                            {
                                Language = country[random.Next(0, 100) % country.Length],
                                Flag = flag[random.Next(0, 100) % flag.Length],
                                HalfFlag =cornerflag[random.Next(0, 100) % cornerflag.Length]
                            }
                        },
                });
            }

            var ListReply = new List<FeedbackModel>();
            for (int i = 0; i < 10; i++)
            {
                ListReply.Add(new FeedbackModel
                {
                    Likes = lstUser,
                    Unlikes = lstUser,
                    User = GetRandomUser(random),
                    Feedback = reply[random.Next(0, 100) % reply.Length],
                    PostedDate = DateTime.Now.AddDays(random.Next(0, 30)).AddHours(random.Next(0, 23)).AddMinutes(random.Next(0, 59)).AddSeconds(random.Next(0, 59))
                });
            }

            var lst = new List<FeedbackModel>();
            for (int i = 0; i < 10; i++)
            {
                lst.Add(new FeedbackModel
                {
                    Rating = random.Next(1, 5),
                    User = GetRandomUser(random),
                    Feedback = write[random.Next(1, 100) % write.Length],
                    Replies = ListReply,
                    PostedDate = DateTime.Now.AddDays(random.Next(0, 30)).AddHours(random.Next(0, 23)).AddMinutes(random.Next(0, 59)).AddSeconds(random.Next(0, 59))
                });
            }

            return Task.FromResult(lst.ToArray());
        }

        public async Task<SocialModel[]> GetFriendSocial(bool speaking, bool writing)
        {
            if (speaking && !writing)
            {
                return (await GetAllSocial()).Where(d => d.Type == SocialModel.SocialType.Speaking && d.Friends).ToArray();
            }
            if (!speaking && writing)
            {
                return (await GetAllSocial()).Where(d => d.Type == SocialModel.SocialType.Writing && d.Friends).ToArray();
            }
            return await GetFriendSocial();
        }

        public async Task<SocialModel[]> GetDiscoverSocial(bool speaking, bool writing)
        {
            if (speaking && !writing)
            {
                return (await GetAllSocial()).Where(d => d.Type == SocialModel.SocialType.Speaking && !d.Friends).ToArray();
            }

            if (!speaking && writing)
            {
                return (await GetAllSocial()).Where(d => d.Type == SocialModel.SocialType.Writing && !d.Friends).ToArray();
            }

            return await GetDiscoverSocial();
        }
    }
}
