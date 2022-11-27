using Naxam.Busuu.Core.Models;
using System.Threading.Tasks;

namespace Naxam.Busuu.Profile.Services
{
    public interface IDataProfileService
    {
        Task<SocialModel[]> GetCorrections(UserModel user);
        Task<SocialModel[]> GetExercises(UserModel user);
        Task<UserModel> GetUser(int id);
        Task<CountryModel[]> GetAllCountry();
        Task<LanguageModel[]> GetAllLanguage(); 
    }
}
