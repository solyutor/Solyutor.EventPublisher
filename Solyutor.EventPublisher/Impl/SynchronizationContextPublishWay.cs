using System.Threading;

namespace Solyutor.EventPublisher.Impl
{
    public class SynchronizationContextPublishWay : IPublishWay
    {
        private readonly SynchronizationContext _synchronizationContext;

        public SynchronizationContextPublishWay(SynchronizationContext synchronizationContext)
        {
            _synchronizationContext = synchronizationContext;
        }

        public void Publish<TMessage>(TMessage message, IHandler<TMessage> handler)
        {
            _synchronizationContext.Post(x => handler.Handle(message), null);
        }
    }

    public class SynchronizationContextPublishWay<TSynchornizationContext> : SynchronizationContextPublishWay
        where TSynchornizationContext : SynchronizationContext, new()
    {
        public SynchronizationContextPublishWay()
            : base(new TSynchornizationContext())
        {
        }
    }
}