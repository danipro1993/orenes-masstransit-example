using Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masstransit.Publisher.Messages
{
    public class MessagePublisher : IMessage
    {
        public MessagePublisher(string message)
        {
            MessageId = Guid.NewGuid();
            Message = message;
        }

        public string Message { get; set; }

        public Guid MessageId { get; set; }
    }
}
