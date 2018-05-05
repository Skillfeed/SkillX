using System;
using Newtonsoft.Json;
using Skillx.Communication.ServiceBus.Abstractions;
using Skillx.Communication.ServiceBus.Messages.UserMessages;
using Skillx.Modules.User.Core.Services;

namespace Skillx.Modules.User.Config
{
    internal class MessageRegistrations
    {
        internal static void RegisterMessageHandlers(IServiceProvider serviceProvider)
        {
            var messageBus = serviceProvider.GetService(typeof(IMessageBus)) as IMessageBus;

            messageBus.Subscribe<UserCreatedMessage>(async message =>
            {
                var userService = serviceProvider.GetService(typeof(IUserService)) as IUserService;
                var userData = JsonConvert.DeserializeObject<Core.Entities.User>(message.Data);

                await userService.CreateUser(userData);
            });
        }
    }
}
