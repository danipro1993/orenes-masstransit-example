using System;

namespace Messages
{
    public interface IMessage
    {
        string Message { get; }

        Guid MessageId { get; }
    }
}
