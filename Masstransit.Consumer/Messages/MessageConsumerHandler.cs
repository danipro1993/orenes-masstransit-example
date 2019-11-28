using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Masstransit.Consumer.Messages
{
    public class MessageConsumerHandler : IConsumer<MessageConsumer>
    {
        private readonly ILogger<MessageConsumerHandler> _logger;

        public MessageConsumerHandler(ILogger<MessageConsumerHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Consume(ConsumeContext<MessageConsumer> context)
        {
            _logger.LogInformation($"context message:  {context.Message.Message}");

            return Task.CompletedTask;
        }
    }
}
