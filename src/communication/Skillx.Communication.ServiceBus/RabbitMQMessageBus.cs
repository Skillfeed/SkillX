using EasyNetQ;
using Microsoft.Extensions.Options;
using Skillx.Communication.ServiceBus.Abstractions;
using Skillx.Communication.ServiceBus.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Skillx.Communication.ServiceBus
{
    public class RabbitMQMessageBus : IMessageBus, IDisposable
    {
        private readonly IBus bus;
        private readonly ICollection<ISubscriptionResult> subscriptions;

        public RabbitMQMessageBus(IOptions<RabbitMQOptions> options)
        {
            this.bus = RabbitHutch.CreateBus(options.Value.ConnectionString);
            this.subscriptions = new List<ISubscriptionResult>();
        }

        public void Dispose()
        {
            if (this.bus != null)
            {
                this.bus.SafeDispose();
            }

            foreach (var sub in this.subscriptions)
            {
                sub.SafeDispose();
            }
        }

        public async Task PublishAsync<TMessage>(TMessage message)
            where TMessage : class
        {
            await this.bus.PublishAsync(message);
        }

        public void Subscribe<TMessage>(Action<TMessage> onMessage)
            where TMessage : class
        {
            var subscriptionId = this.GetSubscriptionId(typeof(TMessage));
            var subscription = this.bus.Subscribe(subscriptionId, onMessage);

            this.subscriptions.Add(subscription);
        }

        private string GetSubscriptionId(Type t)
        {
            var guid = Guid.NewGuid().ToString();
            var ticks = DateTime.Now.Ticks.ToString();
            var typeName = t.Name;
            var id = $"{guid}{typeName}{ticks}";

            return id;
        }
    }
}
