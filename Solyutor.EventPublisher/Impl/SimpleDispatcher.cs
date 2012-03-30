namespace Solyutor.EventPublisher.Impl
{
    public class SimpleDispatcher : IDispatcher
    {
        public virtual void Publish<TMessage>(TMessage message, IHandler<TMessage> handler)
        {
            handler.Handle(message);
        }
    }
}