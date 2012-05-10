using System;

namespace Solyutor.EventPublisher.Castle.Tests
{
    [Obsolete("Use TestHandler<TMessage> instead")]
    public class TestTransientHandler : IHandler<Message>
    {
        public Message Message;
        public static Message StaticMessage;

        #region ITransientHandler<Message> Members

        public void Handle(Message message)
        {
            Message = message;
            StaticMessage = message;
        }

        #endregion
    }
}