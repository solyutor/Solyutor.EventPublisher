namespace Solyutor.EventPublisher.Castle.Tests.Impl
{
    public class TestTransientListener : ITransientListener<Message>
    {
        public Message Message;

        #region ITransientListener<Message> Members

        public void ListenTo(Message message)
        {
            Message = message;
        }

        #endregion
    }
}