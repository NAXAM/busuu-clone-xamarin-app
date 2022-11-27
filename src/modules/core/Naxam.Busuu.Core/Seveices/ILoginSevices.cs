using System;
using System.Threading.Tasks;

namespace Naxam.Busuu.Core.Seveices
{
    public interface ILoginSevices
    {
        Task Login();
        Task Logout();
        Task Register();
	}
}
