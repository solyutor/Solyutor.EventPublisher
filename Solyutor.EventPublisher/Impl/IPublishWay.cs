namespace Solyutor.EventPublisher.Impl
{
    public interface IPublishWay
    {
        void Publish<TMessage>(TMessage message, IHandler<TMessage> handler);
    }
}