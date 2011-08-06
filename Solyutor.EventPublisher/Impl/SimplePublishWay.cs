namespace Solyutor.EventPublisher.Impl
{
    public class SimplePublishWay : IPublishWay
    {
        public void Publish<TMessage>(TMessage message, IListener<TMessage> listener)
        {
            listener.ListenTo(message);
        }
    }
}