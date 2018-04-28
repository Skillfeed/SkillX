using Skillx.Gateways.WebAPI.Models;
using Skillx.Gateways.WebAPI.Models.User;
using System.Threading.Tasks;

namespace Skillx.Gateways.WebAPI.Services.Abstraction
{
    public interface IAuthService
    {
        Task<DefaultResponse> Register(RegisterUserRequest user);

        Task<DefaultResponse> Login(LoginUserRequest user);
    }
}
