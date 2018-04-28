using System;
using System.Threading.Tasks;
using Skillx.Modules.User.Core.Repositories;
using Skillx.Modules.User.Core.Services;

namespace Skillx.Modules.User.Impl.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task CreateUser(Core.Entities.User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await this.userRepository.AddUserAsync(user);
        }
    }
}
