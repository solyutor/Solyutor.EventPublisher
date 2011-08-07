namespace Solyutor.EventPublisher.Castle.Tests.Impl
{
    public class TestTransientListener : ITransientListener<Message>
    {
        public Message Message;
        public static Message StaticMessage;

        #region ITransientListener<Message> Members

        public void ListenTo(Message message)
        {
            Message = message;
            StaticMessage = message;
        }

        #endregion
    }
}