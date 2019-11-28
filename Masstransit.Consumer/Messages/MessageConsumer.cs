using Messages;
using System;

namespace Masstransit.Consumer.Messages
{
    public class MessageConsumer : IMessage
    {
        public string Message { get; set; }

        public Guid MessageId { get; set; }
    }
}
