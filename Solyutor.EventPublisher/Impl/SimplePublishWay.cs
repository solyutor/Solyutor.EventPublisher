namespace Solyutor.EventPublisher.Impl
{
    public class SimplePublishWay : IPublishWay
    {
        public virtual void Publish<TMessage>(TMessage message, IHandler<TMessage> handler)
        {
            handler.Handle(message);
        }
    }
}