using Microsoft.AspNetCore.Mvc;
using Skillx.Gateways.WebAPI.Models;
using Skillx.Gateways.WebAPI.Models.User;
using Skillx.Gateways.WebAPI.Services.Abstraction;
using System.Threading.Tasks;

namespace Skillx.Gateways.WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthService auth;

        public AccountController(IAuthService auth)
        {
            this.auth = auth;
        }

        public async Task<IActionResult> Register([FromBody] RegisterUserRequest user)
        {
            var response = await this.auth.Register(user);

            return this.GetActionResult(response);
        }

        public async Task<IActionResult> Login([FromBody] LoginUserRequest user)
        {
            var response = await this.auth.Login(user);

            return this.GetActionResult(response);
        }
    }
}
