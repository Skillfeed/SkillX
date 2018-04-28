using System.Threading.Tasks;

namespace Skillx.Modules.User.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(Entities.User user);
    }
}
