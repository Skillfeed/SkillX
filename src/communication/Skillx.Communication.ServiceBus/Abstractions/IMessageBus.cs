using System;
using System.Threading.Tasks;

namespace Skillx.Communication.ServiceBus.Abstractions
{
    public interface IMessageBus
    {
        Task PublishAsync<TMessage>(TMessage message)
            where TMessage : class;

        void Subscribe<TMessage>(Action<TMessage> onMessage)
            where TMessage : class;
    }
}
