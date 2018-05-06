using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Skillx.Communication.ServiceBus.Abstractions;
using Skillx.Communication.ServiceBus.Messages.UserMessages;
using Skillx.Gateways.WebAPI.Models;
using Skillx.Gateways.WebAPI.Models.User;
using Skillx.Gateways.WebAPI.Options;
using Skillx.Gateways.WebAPI.Services.Abstraction;
using Skillx.Gateways.WebAPI.Services.Abstraction.Common;
using Newtonsoft.Json;

namespace Skillx.Gateways.WebAPI.Services.Implementation
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(IOptions<ServicesEndpoints> endpoints,
                           IMessageBus messageBus,
                           IApplicationHttpClient http)
            : base(http, endpoints.Value, messageBus)
        {
        }

        public async Task<DefaultResponse> Register(RegisterUserRequest user)
        {
            var response = await this.Http.PostAsync($"{this.Endpoints.Auth}/account/register", user);

            if (response.Success)
            {
                var message = new UserCreatedMessage { Data = JsonConvert.SerializeObject(user) };

                await this.MessageBus.PublishAsync(message);
            }

            return response;
        }

        public async Task<DefaultResponse> Login(LoginUserRequest user)
        {
            var response = await this.Http.PostAsync($"{this.Endpoints.Auth}/auth/login", user);

            return response;
        }
    }
}
