using Solyutor.EventPublisher.Windsor;

namespace Solyutor.EventPublisher.Castle.Tests
{
    public class TestTransientHandler : ITransientHandler<Message>
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