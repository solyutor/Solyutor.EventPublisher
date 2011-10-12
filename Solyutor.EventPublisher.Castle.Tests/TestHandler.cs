namespace Solyutor.EventPublisher.Castle.Tests
{
    public class TestHandler : IHandler<Message>
    {
        public void Handle(Message message)
        {
            Message = message;
        }

        public Message Message;
    }
}