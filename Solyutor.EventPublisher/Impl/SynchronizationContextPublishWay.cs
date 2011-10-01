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

        public void Publish<TMessage>(TMessage message, IListener<TMessage> listener)
        {
            _synchronizationContext.Post(x => listener.ListenTo(message), null);
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