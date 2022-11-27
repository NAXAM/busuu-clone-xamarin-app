using System;
using System.Collections.Generic;
using Naxam.Busuu.Core.Models;
using System.Threading.Tasks;

namespace Naxam.Busuu.Notification.Services
{
    public class DataNotification : IDataNotification
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

        NotificationType[] types = {
            NotificationType.Accpect,NotificationType.Correct,NotificationType.Like,NotificationType.Reply,NotificationType.Request
        };

        public Task<NotificationModel[]> GetNotification()
        {
            Random random = new Random();
            List<NotificationModel> list = new List<NotificationModel>();

            for (int i = 0; i < 22; i++)
            {
                list.Add(new NotificationModel
                {
                    Id = i, 
                    User = GetRandomUser(random.Next(0, 100)),
                    Type = types[random.Next(0, 100) % types.Length],
                    Time = DateTime.Now.AddDays(-random.Next(1, 10)).AddHours(-random.Next(1, 23)).AddMinutes(-random.Next(1, 59)),
                    IsRead = random.Next(0, 100) % 2 == 0
                });
            }
            return Task.FromResult(list.ToArray());
        }

        public UserModel GetRandomUser(int id)
        {
            return new UserModel
            {
                Id = id,
                Name = name[id % name.Length],
                Photo = avatar[id % avatar.Length],

                Country = new CountryModel
                {
                    Country = country[id % country.Length],
                },
            };
        }

        public Task<UserModel[]> GetFriendRequest(int userId)
        {
            Random random = new Random();
            List<UserModel> list = new List<UserModel>();

            for (int i = 0; i < 4; i++)
            {
                list.Add(GetRandomUser(random.Next(0, 100)));
            }
            return Task.FromResult(list.ToArray());
        }
    }
}
