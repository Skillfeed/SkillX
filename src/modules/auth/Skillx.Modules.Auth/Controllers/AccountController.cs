using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skillx.Modules.Auth.Models;
using System.Threading.Tasks;

namespace Skillx.Modules.Auth.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMapper mapper;
        private readonly UserManager<Core.Entities.User> userManager;

        public AccountController(IMapper mapper, UserManager<Core.Entities.User> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var userEntity = this.mapper.Map<Core.Entities.User>(user);
            var creationResult = await this.userManager.CreateAsync(userEntity, user.Password);

            if (!creationResult.Succeeded)
            {
                var creationFailedResponse = this.CreateDefaultResponse(success: false, data: creationResult.Errors);
                return this.BadRequest(creationFailedResponse);
            }

            var successResponse = this.CreateDefaultResponse(success: true, data: new { userEntity.Id });
            return this.Ok(successResponse);
        }
    }
}
