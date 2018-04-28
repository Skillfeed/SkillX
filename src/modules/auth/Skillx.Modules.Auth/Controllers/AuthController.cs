using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Skillx.Modules.Auth.Core.Jwt;
using Skillx.Modules.Auth.Core.Options;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skillx.Modules.Auth.Controllers
{
    public class AuthController : BaseController
    {
        private readonly UserManager<Core.Entities.User> userManager;
        private readonly IJwtFactory jwtFactory;
        private readonly JwtIssuerOptions jwtOptions;
        private readonly ITokenGenerator tokenGenerator;

        public AuthController(UserManager<Core.Entities.User> userManager,
                              IJwtFactory jwtFactory,      
                              IOptions<JwtIssuerOptions> jwtOptions,
                              ITokenGenerator tokenGenerator)
        {
            this.userManager = userManager;
            this.jwtFactory = jwtFactory;
            this.jwtOptions = jwtOptions.Value;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<IActionResult> Login([FromBody] Models.User user)
        {
            var identity = await GetClaimsIdentity(user.UserName, user.Password);

            if (identity == null)
            {
                var invalidLoginResponse = this.CreateDefaultResponse(success: false, message: "Invalid username or password.");
                return this.BadRequest(invalidLoginResponse);
            }

            var token = await this.tokenGenerator.GenerateJwt(identity, this.jwtFactory, user.UserName, this.jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            var loginResponse = this.CreateDefaultResponse(success: true, data: new { Token = token });

            return this.Ok(loginResponse);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Validate()
        {
            return this.Ok();
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            var userToVerify = await this.userManager.FindByNameAsync(userName);

            if (userToVerify == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            if (await this.userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(this.jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
