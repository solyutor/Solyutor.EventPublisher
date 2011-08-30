using System.Collections.Generic;
using Castle.MicroKernel;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Windsor.Facility
{
    public class TransientSource : IListenerSource
    {
        private readonly IKernel _kernel;

        public TransientSource(IKernel kernel)
        {
            _kernel = kernel;
        }

        public virtual IEnumerable<IListener<TMessage>> ResolveListenersFor<TMessage>()
        {
            return _kernel.ResolveAll<ITransientListener<TMessage>>();
        }
    }
}