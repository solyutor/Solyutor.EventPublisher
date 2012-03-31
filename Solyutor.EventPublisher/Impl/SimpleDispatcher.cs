namespace Solyutor.EventPublisher.Impl
{
    public class SimpleDispatcher : IDispatcher
    {
        public virtual void Invoke<TMessage>(TMessage message, IHandler<TMessage> handler)
        {
            handler.Handle(message);
        }
    }
}