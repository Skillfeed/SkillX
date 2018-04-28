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
    public class AuthService : IAuthService
    {
        private readonly IApplicationHttpClient http;
        private readonly ServicesEndpoints endpoints;
        private readonly IMessageBus messageBus;

        public AuthService(IOptions<ServicesEndpoints> endpoints,
                           IMessageBus messageBus,
                           IApplicationHttpClient http)
        {
            this.endpoints = endpoints.Value;
            this.messageBus = messageBus;
            this.http = http;
        }

        public async Task<DefaultResponse> Register(RegisterUserRequest user)
        {
            var response = await this.http.PostAsync($"{this.endpoints.Auth}/account/register", user);

            if (response.Success)
            {
                var message = new UserCreatedMessage { Data = JsonConvert.SerializeObject(user) };

                await this.messageBus.PublishAsync(message);
            }

            return response;
        }

        public async Task<DefaultResponse> Login(LoginUserRequest user)
        {
            var response = await this.http.PostAsync($"{this.endpoints.Auth}/auth/login", user);

            return response;
        }
    }
}
