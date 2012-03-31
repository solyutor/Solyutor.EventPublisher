using System.Threading;

namespace Solyutor.EventPublisher.Impl
{
    public class SynchronizationContextDispatcher : IDispatcher
    {
        private readonly SynchronizationContext _synchronizationContext;

        public SynchronizationContextDispatcher(SynchronizationContext synchronizationContext)
        {
            _synchronizationContext = synchronizationContext;
        }

        public void Invoke<TMessage>(TMessage message, IHandler<TMessage> handler)
        {
            _synchronizationContext.Post(x => handler.Handle(message), null);
        }
    }

    public class SynchronizationContextDispatcher<TSynchornizationContext> : SynchronizationContextDispatcher
        where TSynchornizationContext : SynchronizationContext, new()
    {
        public SynchronizationContextDispatcher()
            : base(new TSynchornizationContext())
        {
        }
    }
}