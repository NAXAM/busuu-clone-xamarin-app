using Naxam.Busuu.Core.Models;
using System.Threading.Tasks;

namespace Naxam.Busuu.Social.Services
{
	public interface IDataSocial
	{		
        Task<SocialModel[]> GetFriendSocial();
        Task<SocialModel[]> GetDiscoverSocial();
        Task<SocialModel[]> GetFriendSocial(bool speaking,bool writing);
        Task<SocialModel[]> GetDiscoverSocial(bool speaking, bool writing);
        Task<SocialModel> GetSocialById(int id);
        Task<FeedbackModel[]> GetFeedbackById(int id);
	}
}
