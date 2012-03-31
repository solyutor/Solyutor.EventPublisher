namespace Solyutor.EventPublisher.Impl
{
    public interface IDispatcher
    {
        void Invoke<TMessage>(TMessage message, IHandler<TMessage> handler);
    }
}