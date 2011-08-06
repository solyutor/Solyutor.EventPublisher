namespace Solyutor.EventPublisher.Impl
{
    public class SimplePublishWay : IPublishWay
    {
        public virtual void Publish<TMessage>(TMessage message, IListener<TMessage> listener)
        {
            listener.ListenTo(message);
        }
    }
}