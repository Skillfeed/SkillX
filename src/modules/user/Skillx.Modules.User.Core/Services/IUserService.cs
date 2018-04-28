using System.Threading.Tasks;

namespace Skillx.Modules.User.Core.Services
{
    public interface IUserService
    {
        Task CreateUser(Entities.User user);
    }
}
