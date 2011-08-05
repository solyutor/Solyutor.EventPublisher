namespace Solyutor.EventPublisher.Impl
{
    public interface IPublishWay
    {
        void Publish<TMessage>(TMessage message, IListener<TMessage> listener);
    }
}