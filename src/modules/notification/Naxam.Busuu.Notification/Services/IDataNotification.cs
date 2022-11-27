using System;
using Naxam.Busuu.Core.Models;
using System.Threading.Tasks;

namespace Naxam.Busuu.Notification.Services
{
    public interface IDataNotification
    {
        Task<NotificationModel[]> GetNotification();
        Task<UserModel[]> GetFriendRequest(int userId);
    }
}
