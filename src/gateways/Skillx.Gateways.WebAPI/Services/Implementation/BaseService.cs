using Skillx.Communication.ServiceBus.Abstractions;
using Skillx.Gateways.WebAPI.Options;
using Skillx.Gateways.WebAPI.Services.Abstraction.Common;

namespace Skillx.Gateways.WebAPI.Services.Implementation
{
    public class BaseService
    {
        private readonly IApplicationHttpClient http;
        private readonly ServicesEndpoints endpoints;
        private readonly IMessageBus messageBus;

        public BaseService(IApplicationHttpClient http,
                             ServicesEndpoints endpoints,
                             IMessageBus messageBus)
        {
            this.http = http;
            this.endpoints = endpoints;
            this.messageBus = messageBus;
        }

        protected IApplicationHttpClient Http => this.http;

        protected ServicesEndpoints Endpoints => this.endpoints;

        protected IMessageBus MessageBus => this.messageBus;
    }
}
