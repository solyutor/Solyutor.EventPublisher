namespace Solyutor.EventPublisher.Impl
{
    public interface IDispatcher
    {
        void Publish<TMessage>(TMessage message, IHandler<TMessage> handler);
    }
}