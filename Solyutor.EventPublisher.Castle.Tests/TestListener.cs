using Solyutor.EventPublisher.Castle.Tests.Impl;

namespace Solyutor.EventPublisher.Castle.Tests
{
    public class TestListener : IListener<Message>
    {
        public void ListenTo(Message message)
        {
            Message = message;
        }

        public Message Message;
    }
}